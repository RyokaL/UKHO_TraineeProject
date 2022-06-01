using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using TraineeProject.Models;
using System.Net.Http;

namespace TraineProject.FunctionApp
{
    public class LogLines
    {
        public const string FINISH = "0";
        public const string DISCOVERY = "5";
        public const string INSTANCE = "6";
        public const string DAMAGEREC = "10";
        public const string DEATH = "11";
        public const string ATTACK = "15";
        public const string AOEATTACK = "16";
        public const string HEAL = "18";
        public const string AOEHEAL = "19";
        public const string SHIELD = "20";
        public const string DOT = "21";
        public const string HOT = "22";
    }

    public class Function1
    {
        [FunctionName("Function1")]
        public void Run([BlobTrigger("traineeprojectblobstorage/{name}", Connection = "StorageAccountConnectionFunctionApp")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
            ProcessLog(myBlob, log);
        }

        public void LogLineDiscovery(string characterName,
                             string worldServer,
                             string jobClass,
                             DateTime startTime,
                             IDictionary<string, CharacterStats> list)
        {
            list.Add(characterName, new CharacterStats(characterName, worldServer, jobClass, startTime));
        }

        public void LogLineDamageReceived(string characterName, long damage, IDictionary<string, CharacterStats> list)
        {
            list.TryGetValue(characterName, out CharacterStats stats);
            stats.TotalDamageReceived += damage;

            if(stats.CurrentShield > 0)
            {
                long diff = stats.CurrentShield - damage;
                if(diff < 0)
                {
                    stats.CurrentShield = 0;
                    damage = -diff;
                }
                else
                {
                    stats.CurrentShield -= damage;
                    damage = diff;
                }
            }
            stats.CurrentHealthLost += damage;
        }

        public bool LogLineDeath(string characterName, IDictionary<string, CharacterStats> list)
        {
            list.TryGetValue(characterName, out CharacterStats stats);
            stats.CurrentHealthLost = 0;
            stats.NumberOfDeaths += 1;
            stats.Dead = true;

            if(list.Values.All(c => c.Dead))
            {
                return true;
            }
            return false;
        }

        public void LogLineAttack(string characterName, long damage, IDictionary<string, CharacterStats> list)
        {
            list.TryGetValue(characterName, out CharacterStats stats);
            stats.TotalDamageDealt += damage;
        }

        public void LogLineHeal(string characterName, string targetName, long heal, IDictionary<string, CharacterStats> list)
        {
            list.TryGetValue(characterName, out CharacterStats originStats);
            list.TryGetValue(targetName, out CharacterStats targetStats);

            originStats.TotalHealing += heal;
            targetStats.CurrentHealthLost -= heal;
            if(targetStats.CurrentHealthLost < 0)
            {
                targetStats.CurrentHealthLost = 0;
            }
        }

        public void LogLineShield(string targetName, long shield, IDictionary<string, CharacterStats> list)
        {
            list.TryGetValue(targetName, out CharacterStats target);
            target.CurrentShield += shield;
        }

        public void LogLineRaise(string targetName, IDictionary<string, CharacterStats> list)
        {
            list.TryGetValue(targetName, out CharacterStats target);
            target.Dead = false;
        }

        public void LogLineFinish(DateTime end, IDictionary<string, CharacterStats> list)
        {
            foreach(CharacterStats c in list.Values)
            {
                c.LastAction = end;
            }
        }

        public async void FinaliseLog(IDictionary<string, CharacterStats> list,
                                bool success,
                                LogParse logParse,
                                DateTime start,
                                DateTime finish)
        {
            logParse.TimeTaken = (int)((finish.Subtract(start)).TotalSeconds);
            logParse.Succeeded = success;
            logParse.DateUploaded = DateTime.Now;
            logParse.CharacterLogs = new List<CharacterLog>();

            foreach(CharacterStats c in list.Values)
            {
                double activeTime = (c.LastAction.Subtract(c.FirstAction)).TotalSeconds;

                CharacterLog cLog = new CharacterLog();
                cLog.Character = new Character { CharacterName = c.CharacterName, WorldServer = c.WorldServer };
                cLog.JobClass = c.JobClass;
                cLog.ActualDPS = (c.TotalDamageDealt) / activeTime;
                cLog.DamageTaken = c.TotalDamageReceived;
                cLog.HPS = (c.TotalHealing) / activeTime;
                //Hardcoded for now due to extra complexities
                cLog.PercentActive = 1;
                cLog.OverhealPercent = 0;
                cLog.RaidDPS = cLog.ActualDPS;

                logParse.CharacterLogs.Add(cLog);
            }

            using(HttpClient http = new HttpClient())
            {
                var response = await http.PostAsJsonAsync("https://calumdbtraineeproject.azurewebsites.net/api/parse", logParse);
                var respContent = await response.Content.ReadAsStringAsync();
                //Could be used for logging
            }
        }

        public void ProcessLog(Stream logFile, ILogger log)
        {
            var logCharacters = new Dictionary<string, CharacterStats>();

            //Obviously missing a fair amount of input validation but eh
            using(var reader = new StreamReader(logFile)) {
                string line = "";
                bool finished = false;
                LogParse parse = new();
                DateTime logStart = DateTime.Now;
                DateTime logFinish = DateTime.Now;

                //First line must be Instance
                line = reader.ReadLine();
                if(line != null)
                {
                    string[] strings = line.Split('|');
                    if (strings[0] == LogLines.INSTANCE)
                    {
                        logStart = DateTime.Parse(strings[1]);
                        parse.InstanceName = strings[2];
                    }
                    else
                    {
                        log.LogError("Invalid log file");
                    }
                }

                while((line = reader.ReadLine()) != null)
                {
                    if(finished)
                    {
                        break;
                    }

                    string[] strings = line.Split('|');

                    switch(strings[0]) {
                        case LogLines.DISCOVERY:
                            LogLineDiscovery(strings[2], strings[3], strings[4], DateTime.Parse(strings[1]), logCharacters);
                            break;
                        case LogLines.DAMAGEREC:
                            LogLineDamageReceived(strings[4], long.Parse(strings[5]), logCharacters);
                            break;
                        case LogLines.DEATH:
                            if(LogLineDeath(strings[2], logCharacters))
                            {
                                //Finalise log failure
                                logFinish = DateTime.Parse(strings[1]);
                                FinaliseLog(logCharacters, false, parse, logStart, logFinish);
                                return;
                            }
                            break;
                        case LogLines.AOEATTACK:
                        case LogLines.ATTACK:
                            LogLineAttack(strings[2], long.Parse(strings[5]), logCharacters);
                            break;
                        case LogLines.AOEHEAL:
                        case LogLines.HEAL:
                            LogLineHeal(strings[2], strings[4], long.Parse(strings[5]), logCharacters);
                            break;
                        case LogLines.SHIELD:
                            LogLineShield(strings[4], long.Parse(strings[5]), logCharacters);
                            break;
                        case LogLines.FINISH:
                            logFinish = DateTime.Parse(strings[1]);
                            LogLineFinish(logFinish, logCharacters);
                            break;
                        default:
                            log.LogError("Unknown line type, cannot process file!");
                            return;
                    }
                }
                //Finalise Log success
                FinaliseLog(logCharacters, true, parse, logStart, logFinish);
                return;
          }  
        }
    }
}

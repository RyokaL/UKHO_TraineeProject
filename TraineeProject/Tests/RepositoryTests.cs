using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TraineeProject.Database;
using TraineeProject.Models;
using TraineeProject.Models.Views;
using TraineeProject.Repository;

namespace TraineeProject.Tests
{
    [TestFixture]
    public class RepositoryTests
    {
        private LogContext _logContext;
        private List<Character> characterSeed;
        private List<CharacterLog> logSeed;
        private List<LogParse> parseSeed;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<LogContext>()
                .UseInMemoryDatabase("logTable").Options;
            _logContext = new LogContext(options);

            SeedDb();
        }

        private void SeedDb()
        {
            _logContext.Database.EnsureDeleted();
            _logContext.Database.EnsureCreated();

            characterSeed = new List<Character>
            {
                new Character()
                {
                    CharacterName = "Test",
                    WorldServer = "Test",
                    Id = 1,
                    Private = true
                },
                
                new Character()
                {
                    CharacterName = "Test",
                    WorldServer = "Test",
                    Id = 2,
                    Private = false
                }
            };

            logSeed = new List<CharacterLog>()
            {
                new CharacterLog()
                {
                    Character = characterSeed[0],
                    CharacterId = 1
                },

                new CharacterLog()
                {
                    Character = characterSeed[1],
                    CharacterId = 2
                }
            };

            parseSeed = new List<LogParse>()
            {
                new LogParse()
                {
                    Id = 1,
                    InstanceName = "Test Fight",
                    Private = false,
                    DateUploaded = new DateTime(2021, 11, 23),
                    Succeeded = true,
                    TimeTaken = 100,
                    CharacterLogs = new List<CharacterLog>()
                    {
                        logSeed[0],
                        logSeed[1]
                    }
                },

                new LogParse()
                {
                    Id = 2,
                    Private = true,
                    DateUploaded = DateTime.MinValue
                }
            };

            logSeed[0].LogParse = parseSeed[0];
            logSeed[0].LogParseId = 1;
            logSeed[1].LogParse = parseSeed[0];
            logSeed[1].LogParseId = 1;

            _logContext.Character.AddRange(characterSeed);
            _logContext.CharacterLog.AddRange(logSeed);
            _logContext.LogParse.AddRange(parseSeed);
            _logContext.SaveChanges();
        }

        [Test]
        public async Task Private_Characters_Not_Returned()
        {
            var repository = new CharacterRepository(_logContext);

            var result = await repository.GetAllCharacters();

            Assert.AreNotEqual(characterSeed.Count, result.Count());
        }

        [Test]
        public async Task Private_Characters_In_Log_Are_Returned_Anonymised()
        {
            var repository = new ParseRepository(_logContext);

            var result = await repository.GetAllParses();

            Assert.AreNotEqual(characterSeed[0].CharacterName, result.ToList()[0].CharacterLogs[0].Character.CharacterName);
        }

        [Test]
        public async Task Private_Parses_Not_Returned()
        {
            var repository = new ParseRepository(_logContext);

            var result = await repository.GetAllParses();

            Assert.AreNotEqual(logSeed.Count, result.Count());
        }

        [Test]
        public async Task Get_Logs_By_Date()
        {
            var repository = new ParseRepository(_logContext);
            var fromDate = new DateTime(2021, 11, 20);
            var untilDate = new DateTime(2021, 11, 23);

            var result = await repository.GetAllParses(fromDateTime: fromDate, untilDateTime: untilDate);
            var logParseApiViews = result.ToList();

            Assert.AreNotEqual(logSeed.Count, logParseApiViews.Count());
            Assert.True(logParseApiViews.Any(p => p.DateUploaded >= fromDate && p.DateUploaded <= untilDate));
        }
    }
}

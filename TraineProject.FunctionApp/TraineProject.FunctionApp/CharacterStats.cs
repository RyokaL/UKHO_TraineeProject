using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineProject.FunctionApp
{
    public class CharacterStats
    {
        public CharacterStats(string name, string server, string jobClass, DateTime start)
        {
            CharacterName = name;
            WorldServer = server;
            JobClass = jobClass;
            FirstAction = start;
            TotalDamageDealt = 0;
            TotalHealing = 0;
            TotalDamageReceived = 0;
            CurrentHealthLost = 0;
            NumberOfDeaths = 0;
            CurrentShield = 0;
            Dead = false;
        }

        public string CharacterName { get; set; }
        public string WorldServer { get; set; }
        public string JobClass { get; set; }
        public DateTime FirstAction { get; set; }
        public DateTime LastAction { get; set; }
        public long TotalDamageDealt { get; set; }
        public long TotalHealing { get; set; }
        public long TotalDamageReceived { get; set; }
        public long CurrentHealthLost { get; set; }
        public int NumberOfDeaths { get; set; }
        public long CurrentShield { get; set; }
        public bool Dead { get; set; }
    }
}

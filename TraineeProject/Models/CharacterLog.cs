using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TraineeProject.Models
{
    public class CharacterLog
    {
        public int Id { get; set; }
        public string JobClass { get; set; }
        public float RaidDPS { get; set; }
        public float ActualDPS { get; set; }
        public float TotalDamage { get; set; }
        public int TimeActive { get; set; }
        public float HPS { get; set; }
        public float OverhealPercent { get; set; }
        public float DamageTaken { get; set; }
        public int LogId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TraineeProject.Models
{
    public class CharacterLog
    {
        public string JobClass { get; set; }
        public double RaidDPS { get; set; }
        public double ActualDPS { get; set; }
        public double TotalDamage { get; set; }
        public double PercentActive { get; set; }
        public double HPS { get; set; }
        public double OverhealPercent { get; set; }
        public double DamageTaken { get; set; }
        public Character Character { get; set; }
    }
}

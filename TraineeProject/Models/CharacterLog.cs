using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TraineeProject.Models
{
    public class CharacterLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string JobClass { get; set; }
        public double RaidDPS { get; set; }
        public double ActualDPS { get; set; }
        public double TotalDamage { get; set; }
        public double PercentActive { get; set; }
        public double HPS { get; set; }
        public double OverhealPercent { get; set; }
        public double DamageTaken { get; set; }
        [ForeignKey("LogParseId")]
        public int LogParseId { get; set; }
        [ForeignKey("CharacterId")]
        public int CharacterId { get; set; }
        public virtual Character Character { get; set; }
        public virtual LogParse LogParse { get; set; }
    }
}

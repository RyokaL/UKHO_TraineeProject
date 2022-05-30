namespace TraineeProject.Models.Request
{
    public class CharacterLogRequest
    {
        public string JobClass { get; set; }
        public double RaidDPS { get; set; }
        public double ActualDPS { get; set; }
        public double TotalDamage { get; set; }
        public double PercentActive { get; set; }
        public double HPS { get; set; }
        public double OverhealPercent { get; set; }
        public double DamageTaken { get; set; }
        public virtual CharacterRequest Character { get; set; }

        public static CharacterLog convertToDbModel(CharacterLogRequest charLog, LogParse parentLog, Character dbChar)
        {
            return new CharacterLog
            {
                JobClass = charLog.JobClass,
                RaidDPS = charLog.RaidDPS,
                ActualDPS = charLog.ActualDPS,
                TotalDamage = charLog.TotalDamage,
                PercentActive = charLog.PercentActive,
                OverhealPercent = charLog.OverhealPercent,
                DamageTaken = charLog.DamageTaken,
                Character = dbChar ?? CharacterRequest.convertToDbModel(charLog.Character),
                LogParse = parentLog
            };
        }
    }
}

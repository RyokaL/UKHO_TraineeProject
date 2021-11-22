namespace TraineeProject.Models.Views
{
    public class CharacterLogParseApiView
    {
        public CharacterLogParseApiView(CharacterLog log)
        {
            JobClass = log.JobClass;
            RaidDPS = log.RaidDPS;
            ActualDPS = log.ActualDPS;
            TotalDamage = log.TotalDamage;
            PercentActive = log.PercentActive;
            HPS = log.HPS;
            OverhealPercent = log.OverhealPercent;
            DamageTaken = log.DamageTaken;
            Character = new CharacterApiView(log.Character);
        }

        public string JobClass { get; set; }
        public double RaidDPS { get; set; }
        public double ActualDPS { get; set; }
        public double TotalDamage { get; set; }
        public double PercentActive { get; set; }
        public double HPS { get; set; }
        public double OverhealPercent { get; set; }
        public double DamageTaken { get; set; }
        public CharacterApiView Character { get; set; }
    }
}

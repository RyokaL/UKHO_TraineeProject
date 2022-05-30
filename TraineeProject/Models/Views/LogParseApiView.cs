using System;
using System.Collections.Generic;

namespace TraineeProject.Models.Views
{
    public class LogParseApiView
    {
        public LogParseApiView(LogParse parse)
        {
            Id = parse.Id;
            InstanceName = parse.InstanceName;
            TimeTaken = parse.TimeTaken;
            Succeeded = parse.Succeeded;
            DateUploaded = parse.DateUploaded;
            CharacterLogs = new List<CharacterLogParseApiView>();
            foreach(CharacterLog c in parse.CharacterLogs)
            {
                CharacterLogs.Add(new CharacterLogParseApiView(c));
            }
        }
        public int Id { get; set; }
        public string InstanceName { get; set; }
        public int TimeTaken { get; set; }
        public bool Succeeded { get; set; }
        public DateTime DateUploaded { get; set; }
        public List<CharacterLogParseApiView> CharacterLogs { get; set; }
    }
}

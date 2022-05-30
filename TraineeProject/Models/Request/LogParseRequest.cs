using System;
using System.Collections.Generic;

namespace TraineeProject.Models.Request
{
    public class LogParseRequest
    {
        public string InstanceName { get; set; }
        public int TimeTaken { get; set; }
        public bool Succeeded { get; set; }
        public bool Private { get; set; }
        public DateTime DateUploaded { get; set; }

        public virtual List<CharacterLogRequest> CharacterLogs { get; set; }

        public static LogParse convertToDbModel(LogParseRequest parse)
        {
            return new LogParse
            {
                InstanceName = parse.InstanceName,
                TimeTaken = parse.TimeTaken,
                Succeeded = parse.Succeeded,
                Private = parse.Private,
                DateUploaded = parse.DateUploaded,
                CharacterLogs = new List<CharacterLog>()
            };
        }
    }
}

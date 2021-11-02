using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TraineeProject.Models
{
    public class ParseLog
    {
        public int Id { get; set; }
        public string InstanceName { get; set; }
        public int TimeTaken { get; set; }
        public DateTime DateUploaded { get; set; }
        public bool Succeeded { get; set; }

    }
}

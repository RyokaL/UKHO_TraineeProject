using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TraineeProject.Models
{
    public class LogParse
    {
        public int Id { get; set; }
        public string InstanceName { get; set; }
        public int TimeTaken { get; set; }
        public bool Succeeded { get; set; }
        public DateTime DateUploaded { get; set; }
    }
}

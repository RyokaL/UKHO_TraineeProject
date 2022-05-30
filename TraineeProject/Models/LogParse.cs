using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TraineeProject.Models
{
    public class LogParse
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string InstanceName { get; set; }
        public int TimeTaken { get; set; }
        public bool Succeeded { get; set; }
        public bool Private { get; set; }
        public DateTime DateUploaded { get; set; }

        public virtual List<CharacterLog> CharacterLogs { get; set; }
    }
}

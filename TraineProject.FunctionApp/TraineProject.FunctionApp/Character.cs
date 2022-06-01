using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TraineeProject.Models
{
    public class Character
    {
        public string CharacterName { get; set; }
        public string WorldServer { get; set; }
    }
}

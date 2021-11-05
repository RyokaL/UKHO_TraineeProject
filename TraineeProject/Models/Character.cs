﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TraineeProject.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string CharacterName { get; set; }
        public string WorldServer { get; set; }
        public int? UserId { get; set; }
        public bool Private { get; set; }

        public virtual List<CharacterLog> CharacterLogs { get; set; }
    }
}

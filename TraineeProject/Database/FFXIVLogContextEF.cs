using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TraineeProject.Database.Interfaces;
using TraineeProject.Models;

namespace TraineeProject.Controllers
{
    public class FFXIVLogContextEF : DbContext, IFFXIVLogContext
    {
        public FFXIVLogContextEF(DbContextOptions<FFXIVLogContextEF> options) : base(options)
        {
            
        }

        public DbSet<CharacterFFXIV> FFXIVCharacters { get; set; }
        public DbSet<CharacterLog> CharacterLogs { get; set; }
        public DbSet<ParseLog> ParseLogs { get; set; }

    }
}

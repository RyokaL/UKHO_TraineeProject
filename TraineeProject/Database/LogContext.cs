using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TraineeProject.Models;

namespace TraineeProject.Database
{
    public class LogContext : DbContext
    {
        public LogContext(DbContextOptions<LogContext> options) : base(options)
        {

        }

        public DbSet<Character> Character { get; set; }
        public DbSet<CharacterLog> CharacterLog { get; set; }
        public DbSet<LogParse> LogParse { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CharacterLog>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Character>()
                .HasMany(x => x.CharacterLogs)
                .WithOne()
                .HasPrincipalKey(p => p.Id)
                .HasForeignKey(p => p.Id);

            modelBuilder.Entity<LogParse>()
                .HasMany(x => x.CharacterLogs)
                .WithOne()
                .HasPrincipalKey(p => p.Id)
                .HasForeignKey(p => p.Id);
        }
    }
}

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
        public LogContext()
        {

        }

        public LogContext(DbContextOptions<LogContext> options) : base(options)
        {

        }

        public virtual DbSet<Character> Character { get; set; }
        public virtual DbSet<CharacterLog> CharacterLog { get; set; }
        public virtual DbSet<LogParse> LogParse { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CharacterLog>()
                .HasOne(p => p.Character)
                .WithMany(p => p.CharacterLogs)
                .HasForeignKey(p => p.CharacterId);

            modelBuilder.Entity<CharacterLog>()
                .HasOne(p => p.LogParse)
                .WithMany(p => p.CharacterLogs)
                .HasForeignKey(p => p.LogParseId);

            //modelBuilder.Entity<Character>()
            //    .HasMany(x => x.CharacterLogs)
            //    .WithOne()
            //    .HasPrincipalKey(p => p.Id)
            //    .HasForeignKey(p => p.Id)
            //    .HasConstraintName("FK_CharacterLog_CharacterId");

            //modelBuilder.Entity<LogParse>()
            //    .HasMany(x => x.CharacterLogs)
            //    .WithOne()
            //    .HasPrincipalKey(p => p.Id)
            //    .HasForeignKey(p => p.Id)
            //    .HasConstraintName("FK_CharacterLog_LogId");
        }
    }
}

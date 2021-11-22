using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TraineeProject.Database;
using TraineeProject.Models;
using TraineeProject.Models.Views;
using TraineeProject.Repository;

namespace TraineeProject.Tests
{
    [TestFixture]
    public class RepositoryTests
    {
        private LogContext _logContext;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<LogContext>()
                .UseInMemoryDatabase("logTable").Options;
            _logContext = new LogContext(options);

            SeedDb();
        }

        private void SeedDb()
        {
            _logContext.Database.EnsureDeleted();
            _logContext.Database.EnsureCreated();

            var characterSeed = new List<Character>
            {
                new Character()
                {
                    CharacterName = "Test",
                    WorldServer = "Test",
                    Id = 1,
                    Private = true
                },
                
                new Character()
                {
                    CharacterName = "Test",
                    WorldServer = "Test",
                    Id = 2,
                    Private = false
                }
            };

            _logContext.Character.AddRange(characterSeed);
            _logContext.SaveChanges();
        }

        [Test]
        public async Task Private_Characters_Not_Returned()
        {
            var repository = new CharacterRepository(_logContext);

            var result = await repository.GetAllCharacters();

            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public async Task Private_Characters_In_Log_Are_Returned_Anonymised()
        {
            //TODO:
        }

        [Test]
        public async Task Get_Parse_Log_Includes_Character_Logs_And_Characters_Info()
        {
            //TODO:
        }
    }
}

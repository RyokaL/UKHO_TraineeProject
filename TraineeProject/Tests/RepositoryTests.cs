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
        }

        [Test]
        public async Task GetPrivateCharacterReturnsNothing()
        {
            var repository = A.Fake<ICharacterRepository<CharacterApiView>>();

            var result = await repository.GetAllCharacters();

            Assert.AreEqual(1, result.Count());
        }
    }
}

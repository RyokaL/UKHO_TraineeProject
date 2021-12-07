using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TraineeProject.Database;
using TraineeProject.Models;
using TraineeProject.Repository;

namespace TraineeProject.Tests
{
    [TestFixture]
    public class RepositoryTests
    {
        private LogContext _logContext;
        private List<Character> _characterSeed;
        private List<CharacterLog> _logSeed;
        private List<LogParse> _parseSeed;

        private Character _privateCharacter;
        private LogParse _privateParse;

        [OneTimeSetUp]
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

            _privateCharacter = new Character()
            {
                CharacterName = "Test",
                WorldServer = "Test",
                Id = 1,
                Private = true
            };

            _characterSeed = new List<Character>
            {
                _privateCharacter,
                
                new Character()
                {
                    CharacterName = "Smudge",
                    WorldServer = "Feline",
                    Id = 2,
                    Private = false
                }
            };

            _logSeed = new List<CharacterLog>()
            {
                new CharacterLog()
                {
                    Character = _characterSeed[0],
                    CharacterId = 1
                },

                new CharacterLog()
                {
                    Character = _characterSeed[1],
                    CharacterId = 2
                }
            };

            _privateParse = new LogParse()
            {
                Id = 1,
                InstanceName = "Test Fight",
                Private = false,
                DateUploaded = new DateTime(2021, 11, 23),
                Succeeded = true,
                TimeTaken = 100,
                CharacterLogs = new List<CharacterLog>()
                {
                    _logSeed[0],
                    _logSeed[1]
                }
            };

            _parseSeed = new List<LogParse>()
            {
                _privateParse,

                new LogParse()
                {
                    Id = 2,
                    Private = true,
                    DateUploaded = DateTime.MinValue
                }
            };

            _logSeed[0].LogParse = _parseSeed[0];
            _logSeed[0].LogParseId = 1;
            _logSeed[1].LogParse = _parseSeed[0];
            _logSeed[1].LogParseId = 1;

            _logContext.Character.AddRange(_characterSeed);
            _logContext.CharacterLog.AddRange(_logSeed);
            _logContext.LogParse.AddRange(_parseSeed);
            _logContext.SaveChanges();
        }

        [Test]
        public async Task Private_Characters_Not_Returned()
        {
            var repository = new CharacterRepository(_logContext);

            var result = await repository.GetAllCharacters();

            Assert.AreNotEqual(_characterSeed.Count, result.Count());
        }

        [Test]
        public async Task Get_Character_By_Id_With_Correct_Id()
        {
            var repository = new CharacterRepository(_logContext);

            var result = await repository.GetCharacterById(_characterSeed[1].Id);

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Get_Character_By_Id_With_Incorrect_Id()
        {
            var repository = new CharacterRepository(_logContext);

            var result = await repository.GetCharacterById(-1);

            Assert.IsNull(result);
        }

        [Test]
        public async Task Get_Character_By_Id_Private_Character_Returns_Null()
        {
            var repository = new CharacterRepository(_logContext);

            var result = await repository.GetCharacterById(_privateCharacter.Id);

            Assert.IsNull(result);
        }

        [Test]
        public async Task Private_Characters_In_Log_Are_Returned_Anonymised()
        {
            var repository = new ParseRepository(_logContext);

            var result = await repository.GetAllParses();

            Assert.AreNotEqual(_privateCharacter.CharacterName, result.ToList()[0].CharacterLogs[0].Character.CharacterName);
        }

        [Test]
        public async Task Private_Parses_Not_Returned()
        {
            var repository = new ParseRepository(_logContext);

            var result = await repository.GetAllParses();

            Assert.AreNotEqual(_logSeed.Count, result.Count());
        }

        [Test]
        public async Task Get_Logs_By_Date()
        {
            var repository = new ParseRepository(_logContext);
            var fromDate = new DateTime(2021, 11, 20);
            var untilDate = new DateTime(2021, 11, 23);

            var result = await repository.GetAllParses(fromDateTime: fromDate, untilDateTime: untilDate);
            var logParseApiViews = result.ToList();

            //Assert.AreNotEqual(_logSeed.Count, logParseApiViews.Count);
            Assert.True(logParseApiViews.All(p => p.DateUploaded >= fromDate && p.DateUploaded <= untilDate));
        }

        [Test]
        public async Task Get_Logs_By_Character_Name()
        {
            var repository = new ParseRepository(_logContext);

            var result = await repository.GetAllParses(characterName: _characterSeed[1].CharacterName);
            var logParseApiViews = result.ToList();

            //Assert.AreNotEqual(_logSeed.Count, logParseApiViews.Count());
            Assert.True(logParseApiViews.Any(pl => pl.CharacterLogs.Any(cl => cl.Character.CharacterName.Contains(_characterSeed[1].CharacterName))));
        }

        [Test]
        public async Task Get_Logs_By_World_Server()
        {
            var repository = new ParseRepository(_logContext);

            var result = await repository.GetAllParses(worldServer: _characterSeed[1].WorldServer);
            var logParseApiViews = result.ToList();

            //Assert.AreNotEqual(_logSeed.Count, logParseApiViews.Count());
            Assert.True(logParseApiViews.Any(pl => pl.CharacterLogs.Any(cl => cl.Character.WorldServer.Contains(_characterSeed[1].WorldServer))));
        }

        //Don't want to accidentally expose private data by proxy
        [Test]
        public async Task Getting_Logs_By_Character_Name_Does_Not_Include_Private_Characters()
        {
            var repository = new ParseRepository(_logContext);

            var result = await repository.GetAllParses(characterName: _privateCharacter.CharacterName);
            var logParseApiViews = result.ToList();

            //Assert.AreNotEqual(_logSeed.Count, logParseApiViews.Count());
            Assert.False(logParseApiViews.Any(pl => pl.CharacterLogs.Any(cl => cl.Character.CharacterName.Equals(_privateCharacter.CharacterName))));
        }

        [Test]
        public async Task Getting_Logs_By_World_Server_Does_Not_Include_Private_Characters()
        {
            var repository = new ParseRepository(_logContext);

            var result = await repository.GetAllParses(worldServer: _privateCharacter.WorldServer);
            var logParseApiViews = result.ToList();

            Assert.AreNotEqual(_logSeed.Count, logParseApiViews.Count());
        }
    }
}

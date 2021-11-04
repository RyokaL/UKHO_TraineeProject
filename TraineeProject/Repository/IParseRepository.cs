using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraineeProject.Models;

namespace TraineeProject.Repository
{
    public interface IParseRepository
    {
        //This could be a lot so may be removed later
        public Task<IEnumerable<LogParse>> GetAllParses();
        public Task<IEnumerable<LogParse>> GetAllParsesBetweenDates(DateTime fromDateTime, DateTime untilDateTime);
        public Task<IEnumerable<LogParse>> GetAllParsesByCharacterName(string characterName, string worldServer);
        public Task<IEnumerable<LogParse>> GetAllParsesByCharacterBetweenDates(string characterName, string worldServer,
            DateTime fromDateTime, DateTime untilDateTime);

        public Task<IEnumerable<LogParse>> GetAllParsesByCharacterId(int characterId);
        public Task<LogParse> GetParseById(int id);
    }
}

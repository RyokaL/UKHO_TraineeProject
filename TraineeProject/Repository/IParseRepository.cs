using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraineeProject.Models;

namespace TraineeProject.Repository
{
    public interface IParseRepository<T>
    {
        //This could be a lot so may be removed later
        public Task<IEnumerable<T>> GetAllParses();
        public Task<IEnumerable<T>> GetAllParsesBetweenDates(DateTime fromDateTime, DateTime untilDateTime);
        public Task<IEnumerable<T>> GetAllParsesByCharacterName(string characterName, string worldServer);
        public Task<IEnumerable<T>> GetAllParsesByCharacterBetweenDates(string characterName, string worldServer,
            DateTime fromDateTime, DateTime untilDateTime);

        public Task<IEnumerable<T>> GetAllParsesByCharacterId(int characterId);
        public Task<T> GetParseById(int id);
    }
}

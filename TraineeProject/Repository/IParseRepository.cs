using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraineeProject.Models;

namespace TraineeProject.Repository
{
    public interface IParseRepository<T>
    {
        public Task<IEnumerable<T>> GetAllParses(string instanceName = "",
            string characterName = "", string worldServer = "", DateTime? fromDateTime = null,
            DateTime? untilDateTime = null);

        public Task<IEnumerable<T>> GetAllParsesByCharacterId(int characterId);
        public Task<T> GetParseById(int id);
    }
}

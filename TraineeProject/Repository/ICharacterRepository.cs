using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraineeProject.Database;
using TraineeProject.Models;

namespace TraineeProject.Repository
{
    public interface ICharacterRepository
    {
        public Task<IEnumerable<Character>> GetAllCharacters();
        public Task<Character> GetCharacterById(int id);

    }
}

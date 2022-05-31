using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraineeProject.Database;
using TraineeProject.Models;
using TraineeProject.Models.Request;
using TraineeProject.Models.Views;

namespace TraineeProject.Repository
{
    public interface ICharacterRepository<T>
    {
        public Task<IEnumerable<T>> GetAllCharacters();
        public Task<T> GetCharacterById(int id);

        public Task<T> AddCharacter(CharacterRequest character);

        public Task<T> AddUserIdToCharacter(CharacterRequest character);
        public Task<T> UpdateCharacterPrivacy(CharacterRequest character);

        public Task<IEnumerable<T>> GetCharactersForUserId(string userId);

    }
}

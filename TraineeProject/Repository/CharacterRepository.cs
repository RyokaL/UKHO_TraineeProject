using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TraineeProject.Database;
using TraineeProject.Models;
using TraineeProject.Models.Request;
using TraineeProject.Models.Views;

namespace TraineeProject.Repository
{
    public class CharacterRepository : ICharacterRepository<CharacterApiView>
    {
        private readonly LogContext _logContext;

        public CharacterRepository(LogContext _logContext)
        {
            this._logContext = _logContext;
        }

        public async Task<CharacterApiView> AddCharacter(CharacterRequest character)
        {
            Character exists = await _logContext.Character.FirstOrDefaultAsync(c => c.CharacterName == character.CharacterName && c.WorldServer == character.WorldServer);
            if(exists == null)
            {
                var dbCharacter = CharacterRequest.convertToDbModel(character);
                _logContext.Character.Add(dbCharacter);
                await _logContext.SaveChangesAsync();
                return new CharacterApiView(dbCharacter);
            }

            return null;
        }

        public async Task<IEnumerable<CharacterApiView>> GetCharactersForUserId(string userId)
        {
            return await _logContext.Character.Where(c => c.UserId == userId).Select(c => new CharacterApiView(c, userId)).ToListAsync();
        }

        public async Task<CharacterApiView> UpdateCharacterPrivacy(CharacterRequest character)
        {
            Character exists = await _logContext.Character.FirstOrDefaultAsync(c => c.CharacterName == character.CharacterName && c.WorldServer == character.WorldServer);
            if (exists != null)
            {

                    exists.Private = character.Private ?? false;
                    await _logContext.SaveChangesAsync();

                return new CharacterApiView(exists);
            }

            return null;
        }

        public async Task<CharacterApiView> AddUserIdToCharacter(CharacterRequest character)
        {
            Character exists = await _logContext.Character.FirstOrDefaultAsync(c => c.CharacterName == character.CharacterName && c.WorldServer == character.WorldServer);
            if(exists != null)
            {
                if(exists.UserId == null)
                {
                    return null;
                }
                await _logContext.SaveChangesAsync();
                return new CharacterApiView(exists);
            }

            return null;
        }

        public async Task<IEnumerable<CharacterApiView>> GetAllCharacters()
        {
            var characters = await _logContext.Character.ToListAsync();
            return characters.Where(c => !c.Private).Select(c => new CharacterApiView(c)).ToList();
        }

        public async Task<CharacterApiView> GetCharacterById(int id)
        {
            var character = await _logContext.Character.FindAsync(id);
            return character == null ? null : (character.Private) ? null : new CharacterApiView(character);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TraineeProject.Database;
using TraineeProject.Models;
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

        public async Task<IEnumerable<CharacterApiView>> GetAllCharacters()
        {
            var characters = await _logContext.Character.ToListAsync();
            return characters.Where(c => !c.Private).Select(c => new CharacterApiView(c)).ToList();
        }

        public async Task<CharacterApiView> GetCharacterById(int id)
        {
            var character = await _logContext.Character.FindAsync(id);
            return character == null ? null : new CharacterApiView(character);
        }
    }
}
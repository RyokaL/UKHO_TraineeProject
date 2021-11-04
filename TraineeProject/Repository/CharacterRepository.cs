using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TraineeProject.Database;
using TraineeProject.Models;

namespace TraineeProject.Repository
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly LogContext _logContext;

        public CharacterRepository(LogContext _logContext)
        {
            this._logContext = _logContext;
        }
        public async Task<IEnumerable<Character>> GetAllCharacters()
        {
            return await _logContext.Character.ToListAsync();
        }

        public async Task<Character> GetCharacterById(int id)
        {
            return await _logContext.Character.FindAsync(id);
        }
    }
}
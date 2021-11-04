using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TraineeProject.Database;
using TraineeProject.Models;

namespace TraineeProject.Repository
{
    class ParseRepository : IParseRepository
    {
        private readonly LogContext _logContext;

        public ParseRepository(LogContext logContext)
        {
            _logContext = logContext;
        }

        public async Task<IEnumerable<LogParse>> GetAllParses()
        {
            return await _logContext.LogParse.ToListAsync();
        }

        public async Task<IEnumerable<LogParse>> GetAllParsesBetweenDates(DateTime fromDateTime, DateTime untilDateTime)
        {
            return await _logContext.LogParse.Where(p =>
                p.DateUploaded >= fromDateTime && p.DateUploaded <= untilDateTime).ToListAsync();
        }

        public Task<IEnumerable<LogParse>> GetAllParsesByCharacterName(string characterName, string worldServer)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LogParse>> GetAllParsesByCharacterBetweenDates(string characterName, string worldServer, DateTime fromDateTime,
            DateTime untilDateTime)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LogParse>> GetAllParsesByCharacterId(int characterId)
        {
            throw new NotImplementedException();
        }

        public async Task<LogParse> GetParseById(int id)
        {
            return await _logContext.LogParse.FindAsync(id);
        }
    }
}
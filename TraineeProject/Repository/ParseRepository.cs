using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TraineeProject.Database;
using TraineeProject.Models;
using TraineeProject.Models.Views;

namespace TraineeProject.Repository
{
    class ParseRepository : IParseRepository<LogParseApiView>
    {
        private readonly LogContext _logContext;

        public ParseRepository(LogContext logContext)
        {
            _logContext = logContext;
        }

        //public async Task<IEnumerable<LogParseApiView>> GetAllParses()
        //{
        //    IEnumerable<LogParse> parses = await _logContext.LogParse.Include(p => p.CharacterLogs).ThenInclude(p => p.Character).Where(p => !p.Private).ToListAsync();
        //    List<LogParseApiView> viewParses = new List<LogParseApiView>();
        //    foreach(var parse in parses)
        //    {
        //        viewParses.Add(new LogParseApiView(parse));
        //    }
        //    return viewParses;
        //}

        public async Task<IEnumerable<LogParseApiView>> GetAllParses(string instanceName = "", string characterName = "", string worldServer = "", DateTime? fromDateTime = null, DateTime? untilDateTime = null)
        {
            fromDateTime ??= DateTime.MinValue;
            untilDateTime ??= DateTime.UtcNow;

            IEnumerable<LogParse> parses = await _logContext.LogParse.Include(p => p.CharacterLogs)
                .ThenInclude(p => p.Character)
                .Where(p => !p.Private)
                .Where(p => p.InstanceName.Contains(instanceName) 
                            && p.DateUploaded >= fromDateTime 
                            && p.DateUploaded <= untilDateTime 
                            && p.CharacterLogs.Any(cl => !cl.Character.Private 
                                                         && cl.Character.CharacterName.Contains(characterName) 
                                                         && cl.Character.WorldServer.Contains(worldServer)))
                .ToListAsync();

            return parses.Select(parse => new LogParseApiView(parse)).ToList();
        }

        public async Task<IEnumerable<LogParseApiView>> GetAllParsesBetweenDates(DateTime fromDateTime, DateTime untilDateTime)
        {
            throw new NotImplementedException();
            //return await _logContext.LogParse.Where(p =>
            //    p.DateUploaded >= fromDateTime && p.DateUploaded <= untilDateTime).ToListAsync();
        }

        public Task<IEnumerable<LogParseApiView>> GetAllParsesByCharacterName(string characterName, string worldServer)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LogParseApiView>> GetAllParsesByCharacterBetweenDates(string characterName, string worldServer, DateTime fromDateTime,
            DateTime untilDateTime)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<LogParseApiView>> GetAllParsesByCharacterId(int characterId)
        {
            var parses = await _logContext.LogParse.Include(p => p.CharacterLogs).ThenInclude(p => p.Character).Where(p => !p.Private && p.CharacterLogs.Any(cl => cl.CharacterId == characterId))
                .ToListAsync();
            List<LogParseApiView> viewParses = new List<LogParseApiView>();

            foreach(var p in parses)
            {
                viewParses.Add(new LogParseApiView(p));
            }
            return viewParses;
        }

        public async Task<LogParseApiView> GetParseById(int id)
        {
            var parse = await _logContext.LogParse.FindAsync(id);
            return parse == null ? null : new LogParseApiView(parse);
        }
    }
}
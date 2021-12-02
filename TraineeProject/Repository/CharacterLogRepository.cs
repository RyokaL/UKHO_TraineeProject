using System.Collections.Generic;
using System.Threading.Tasks;
using TraineeProject.Database;
using TraineeProject.Models.Views;

namespace TraineeProject.Repository;

class CharacterLogRepository : ICharacterLogRepository<CharacterLogParseApiView>
{
    private readonly LogContext _logContext;

    public CharacterLogRepository(LogContext logContext)
    {
        _logContext = logContext;
    }

    public async Task<CharacterLogParseApiView> GetCharacterLogById(int id)
    {
        var log = await _logContext.CharacterLog.FindAsync(id);
        return log == null ? null : new CharacterLogParseApiView(log);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TraineeProject.Repository
{
    public interface ICharacterLogRepository<T>
    {
        public Task<T> GetCharacterLogById(int id);
    }
}

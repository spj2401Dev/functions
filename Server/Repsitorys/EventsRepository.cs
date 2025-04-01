using Functions.Server.Interfaces;
using Functions.Server.Model;
using Functions.Server.Services;

namespace Functions.Server.Repsitorys
{
    public class EventsRepository(FunctionsDbContext context) : IRepository<Events>
    {
        public async Task<IEnumerable<Events>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Events> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(Events entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Events entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

using Functions.Server.Interfaces.Messages;
using Functions.Server.Model;
using Functions.Server.Services;
using Functions.Shared.DTOs.Messages;
using Microsoft.EntityFrameworkCore;

namespace Functions.Server.Repsitorys
{
    public class GetMessagesByEventIdQuery(FunctionsDbContext context) : IGetMessagesByEventIdQuery
    {

        public async Task<List<MessageDTO>> GetMessagesByEventId(Guid Id)
        {
            return await context.Messages
                .Where(x => x.EventId == Id)
                .AsNoTracking()
                .Select(x => new MessageDTO
                {
                    Text = x.Text,
                    MessageDate = x.MessageDate,
                    ParentId = x.ParentId
                })
                .ToListAsync();
        }
    }
}

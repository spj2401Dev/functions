using Functions.Server.Interfaces.Messages;
using Functions.Server.Services;
using Functions.Shared.DTOs.Messages;
using Functions.Shared.DTOs.Users;
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
                    Id = x.Id, // Add this property
                    Text = x.Text,
                    MessageDate = x.MessageDate,
                    ParentId = x.ParentId,
                    Type = x.Type,
                    Creator = context.Users
                        .Where(u => u.Id == x.CreatorId)
                        .Select(u => new SafeUserResponseDTO
                        {
                            UserId = u.Id,
                            FirstName = u.Firstname,
                            LastName = u.Lastname,
                            UserName = u.Username
                        })
                        .FirstOrDefault()
                })
                .ToListAsync();
        }
    }
}

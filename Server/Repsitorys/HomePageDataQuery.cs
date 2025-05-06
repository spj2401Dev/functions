using Functions.Server.Interfaces.Event;
using Functions.Server.Services;
using Functions.Shared.DTOs.Event;
using Functions.Shared.DTOs.Messages;
using Functions.Shared.DTOs.Users;

namespace Functions.Server.Repsitorys
{
    public class HomePageDataQuery(FunctionsDbContext context) : IHomePageDataQuery
    {
        public async Task<HomePageResponseDTO> GetHomePageData(Guid userId)
        {
            var homePageData = new HomePageResponseDTO();

            homePageData.Events = context.EventVisitors
                .Where(ev => ev.UserId == userId)
                .Join(
                    context.Events,
                    visitor => visitor.EventId,
                    evt => evt.Id,
                    (visitor, evt) => new EventMasterPageDTO
                    {
                        Id = evt.Id,
                        Name = evt.Name,
                        Description = evt.Description,
                        StartDate = evt.StartDateTime,
                        EndDate = evt.EndDateTime,
                        ImageId = evt.PictureId,
                        Location = evt.Location
                    })
                .ToList();

            homePageData.Announcements = context.EventVisitors
                .Where(ev => ev.UserId == userId)
                .Join(
                    context.Messages.Where(m => m.Type == Shared.Enum.MessageTypes.Announcement)
                                    .OrderBy(m => m.MessageDate),
                    visitor => visitor.EventId,
                    message => message.EventId,
                    (visitor, message) => new HomePageAnnouncementResponseDTO
                    {
                        Text = message.Text,
                        MessageDate = message.MessageDate,
                        Creator = context.Users
                            .Where(u => u.Id == message.CreatorId)
                            .Select(u => new SafeUserResponseDTO
                            {
                                UserId = u.Id,
                                FirstName = u.Firstname,
                                LastName = u.Lastname,
                                UserName = u.Username
                            })
                            .FirstOrDefault(),
                        Event = context.Events
                            .Where(e => e.Id == message.EventId)
                            .Select(e => new EventMasterPageDTO
                            {
                                Id = e.Id,
                                Name = e.Name,
                                Description = e.Description,
                                StartDate = e.StartDateTime,
                                EndDate = e.EndDateTime,
                                ImageId = e.PictureId,
                                Location = e.Location
                            })
                            .FirstOrDefault()
                    })
                .ToList();

            return homePageData;
        }
    }
}

using Functions.Server.Interfaces;
using Functions.Server.Interfaces.Event;
using Functions.Server.Interfaces.Participation;
using Functions.Server.Model;
using Functions.Shared.DTOs.Event;

namespace Functions.Server.UseCases.Event
{
    public class GetAllEventsByUserUseCase(IRepository<Events> eventsRepository, IEventVisitorQuery eventVisitorQuery) : IGetAllEventsByUserUseCase
    {
        public async Task<List<EventMasterPageDTO>> Handle(Guid userId)
        {
            var eventsByUser = await eventVisitorQuery.GetAllEventsByUserId(userId);

            var eventIds = eventsByUser.Select(ev => ev.EventId).ToList();

            var events = await eventsRepository.GetAllAsync();
            var eventsByUserList = events
                .Where(e => eventIds.Contains(e.Id) && e.EndDateTime > DateTime.Now)
                .Select(e => new EventMasterPageDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Location = e.Location,
                    Description = e.Description,
                    StartDate = e.StartDateTime,
                    EndDate = e.EndDateTime,
                    ImageId = e.PictureId,
                })
                .ToList();

            return eventsByUserList;
        }
    }
}

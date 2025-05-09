using Functions.Server.Interfaces;
using Functions.Server.Interfaces.Event;
using Functions.Server.Model;
using Functions.Shared.DTOs.Event;

namespace Functions.Server.UseCases.Event
{
    public class UpdateEventUseCase(IRepository<Events> eventRepository) : IUpdateEventUseCase
    {
        public async Task Handle(EventsDTO request, Guid userId)
        {
            var Event = new Events
            {
                Id = request.Id,
                Host = userId,
                Name = request.Name,
                Location = request.Location,
                Description = request.Description,
                StartDateTime = request.StartDateTime,
                EndDateTime = request.EndDateTime,
                IsPublic = request.isPublic,
            };
            await eventRepository.UpdateAsync(Event);
        }
    }
}

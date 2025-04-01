using Functions.Server.Interfaces;
using Functions.Server.Model;
using Functions.Shared.DTOs;
using Functions.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

[Route("api/[controller]")]
[ApiController]
public class EventsController(IRepository<Events> eventRepository) : ControllerBase, IEventsProxy
{
    [HttpGet]
    public async Task<List<EventsDTO>> GetEventsAsync()
    {
        var events = await eventRepository.GetAllAsync();
        return events.Select(e => new EventsDTO
        (
            e.Id,
            e.Host,
            e.Name,
            e.Location,
            e.Description,
            e.StartDateTime,
            e.EndDateTime,
            null // TODO mf!
        )).ToList();
    }

    [HttpPost]
    public async Task<HttpResponseMessage> PostEventAsync([FromBody] EventsDTO request)
    {
        Events events = new()
        {
            Host = Guid.Empty, // TODO via User Claims
            Name = request.Name,
            Location = request.Location,
            Description = request.Description,
            StartDateTime = request.StartDateTime,
            EndDateTime = request.EndDateTime,
            PictureId = null // TODO via Robust Upload Sytem
        };

        await eventRepository.AddAsync(events);

        return new HttpResponseMessage(HttpStatusCode.Created);
    }
}

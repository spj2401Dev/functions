using Functions.Server.Interfaces;
using Functions.Server.Model;
using Functions.Shared.DTOs;
using Functions.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

[Route("api/[controller]")]
[ApiController]
public class EventsController(
    IRepository<Events> eventRepository,
    IRepository<Files> fileRepository) : ControllerBase, IEventsProxy
{
    [HttpGet("getEvents")]
    public async Task<List<EventsDTO>> GetEventsAsync()
    {
        var events = await eventRepository.GetAllAsync();
        var result = new List<EventsDTO>();

        foreach (var e in events)
        {
            string? base64Image = null;
            if (e.PictureId.HasValue)
            {
                var file = await fileRepository.GetByIdAsync(e.PictureId.Value);
                if (file != null && file.FileContent != null)
                {
                    base64Image = file.FileContent.Base64Content;
                }
            }

            result.Add(new EventsDTO(
                e.Id,
                e.Host,
                e.Name,
                e.Location,
                e.Description,
                e.StartDateTime,
                e.EndDateTime,
                base64Image
            ));
        }

        return result;
    }

    [HttpGet("getEventbyID")]
    public async Task<EventsDTO?> GetEventsbyIdAsync([FromQuery] Guid Id)
    {
        var @event = await eventRepository.GetByIdAsync(Id);
        if (@event == null)
        {
            return null;
        }
        return new EventsDTO(
            @event.Id,
            @event.Host,
            @event.Name,
            @event.Location,
            @event.Description,
            @event.StartDateTime,
            @event.EndDateTime,
            null);

    }

    [HttpPost]
    public async Task<HttpResponseMessage> PostEventAsync([FromBody] EventsDTO request)
    {
        var newEvent = new Events
        {
            Id = Guid.NewGuid(),
            Host = Guid.Empty, // TODO via User Claims
            Name = request.Name,
            Location = request.Location,
            Description = request.Description,
            StartDateTime = request.StartDateTime,
            EndDateTime = request.EndDateTime
        };

        if (!string.IsNullOrEmpty(request.ProfilePictureBase64) &&
            !string.IsNullOrEmpty(request.FileName) &&
            !string.IsNullOrEmpty(request.FileType))
        {
            try
            {
                var fileContent = new FileContent
                {
                    Id = Guid.NewGuid(), // think about it
                    Base64Content = request.ProfilePictureBase64
                };

                var fileRecord = new Files
                {
                    Id = Guid.NewGuid(),
                    FileName = request.FileName,
                    FileType = request.FileType,
                    FileContentId = fileContent.Id,
                    FileContent = fileContent
                };

                newEvent.PictureId = fileRecord.Id;

                await fileRepository.AddAsync(fileRecord);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving file: {ex.Message}"); // logger implementation?
                newEvent.PictureId = null;
            }
        }

        await eventRepository.AddAsync(newEvent);

        return new HttpResponseMessage(HttpStatusCode.Created);
    }
}
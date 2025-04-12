﻿using Functions.Server.Interfaces.Event;
using Functions.Server.Services;
using Functions.Shared.DTOs.Event;
using Functions.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

[Route("api/[controller]")]
[ApiController]
public class EventsController(IConfiguration configuration,
                              IGetEventsUseCase getEventUseCase,
                              IGetFilteredEventsUseCase getFilteredEventsUseCase,
                              IGetEventByIdUseCase getEventByIdUseCase,
                              ICreateEventUseCase createEventUseCase,
                              IGetAllEventsByUserUseCase getAllEventsByUserUseCase) : FunctionsControllerBase(configuration), IEventsProxy
{
    [HttpGet("getEvents")]
    public async Task<List<EventMasterPageDTO>> GetEventsAsync()
    {
        return await getEventUseCase.Handle();
    }

    [HttpGet("getEventbyId")]
    public async Task<EventsDTO?> GetEventsbyIdAsync([FromQuery] Guid Id)
    {

        return await getEventByIdUseCase.Handle(Id);
    }

    [HttpGet("getfilteredevents")]
    public async Task<List<EventsDTO>> GetFilteredEventsAsync([FromQuery] bool isPublic)
    {
        return await getFilteredEventsUseCase.Handle(isPublic);
    }

    [HttpPost]
    public async Task<HttpResponseMessage> PostEventAsync([FromBody] EventsDTO request)
    {
        var userId = await GetUserIdFromTokenAsync();
        if (userId == null)
        {
            throw new ArgumentNullException(nameof(userId));
        }

        await createEventUseCase.Handle(request, userId.Value);

        return new HttpResponseMessage(HttpStatusCode.Created);
    }

    [HttpGet("getalleventsbyuser")]
    public async Task<List<EventMasterPageDTO>> GetAllEventsByUserAsync()
    {
        var userId = await GetUserIdFromTokenAsync();
        if (userId == null)
        {
            throw new ArgumentNullException(nameof(userId));
        }

        return await getAllEventsByUserUseCase.Handle(userId.Value);
    }
}
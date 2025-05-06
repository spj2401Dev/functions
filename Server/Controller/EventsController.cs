using Functions.Server.Interfaces.Event;
using Functions.Server.Services;
using Functions.Shared.DTOs.Event;
using Functions.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

[Route("api/[controller]")]
[ApiController]
public class EventsController(IConfiguration configuration,
                              IGetEventsUseCase getEventUseCase,
                              IGetEventByIdUseCase getEventByIdUseCase,
                              ICreateEventUseCase createEventUseCase,
                              IHomePageDataQuery homePageDataQuery) : FunctionsControllerBase(configuration), IEventsProxy
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

    [HttpPost]
    public async Task<HttpResponseMessage> PostEventAsync([FromBody] EventsDTO request)
    {
        var userId = await GetUserIdFromTokenAsync() ?? throw new UnauthorizedAccessException();

        await createEventUseCase.Handle(request, userId);

        return new HttpResponseMessage(HttpStatusCode.Created);
    }

    [HttpGet("getalleventsbyuser")]
    public async Task<HomePageResponseDTO> GetAllEventsByUserAsync()
    {
        var userId = await GetUserIdFromTokenAsync() ?? throw new UnauthorizedAccessException();

        return await homePageDataQuery.GetHomePageData(userId);
    }
}
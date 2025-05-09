using Functions.Shared.DTOs.Event;

namespace Functions.Server.Interfaces.Event
{
    public interface IHomePageDataQuery
    {
        Task<HomePageResponseDTO> GetHomePageData(Guid userId);
    }
}

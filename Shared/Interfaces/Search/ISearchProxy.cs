using Functions.Shared.DTOs.Event;

namespace Functions.Shared.Interfaces.Search
{
    public interface ISearchProxy
    {
        Task<IEnumerable<EventMasterPageDTO>> Search(string query);
    }
}

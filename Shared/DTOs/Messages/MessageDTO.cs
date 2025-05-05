using Functions.Shared.DTOs.Users;
using Functions.Shared.Enum;

namespace Functions.Shared.DTOs.Messages
{
    public class MessageDTO
    {
        public Guid Id { get; set; } // Add this property
        public string Text { get; set; } = string.Empty;
        public DateTime MessageDate { get; set; }
        public SafeUserResponseDTO? Creator { get; set; }
        public Guid? ParentId { get; set; }
        public MessageTypes Type { get; set; }
    }
}

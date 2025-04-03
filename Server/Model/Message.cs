using Functions.Shared.Enum;

namespace Functions.Server.Model;

public partial class Message
{
    public Guid Id { get; set; }

    public Guid CreatorId { get; set; }

    public Guid EventId { get; set; }

    public int Likes { get; set; }

    public Guid? ParentId { get; set; }

    public DateTime MessageDate { get; set; }

    public MessageTypes Type { get; set; }

    public string Text { get; set; } = null!;

    public virtual User Creator { get; set; } = null!;

    public virtual Events Event { get; set; } = null!;

    public virtual ICollection<Message> InverseParent { get; set; } = new List<Message>();

    public virtual Message? Parent { get; set; }
}

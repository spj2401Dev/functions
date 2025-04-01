namespace Functions.Server.Model;

public partial class Events
{
    public Guid Id { get; set; }

    public Guid Host { get; set; }

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime StartDateTime { get; set; }

    public DateTime EndDateTime { get; set; }

    public Guid? PictureId { get; set; }

    public virtual ICollection<EventVisitor> EventVisitors { get; set; } = new List<EventVisitor>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual Files? Picture { get; set; }
}

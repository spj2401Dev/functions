namespace Functions.Server.Model;

public partial class Files
{
    public Guid Id { get; set; }

    public string? FileName { get; set; }

    public string? FileType { get; set; }

    public Guid? FileContentId { get; set; }

    public virtual ICollection<Events> Events { get; set; } = new List<Events>();

    public virtual FileContent? FileContent { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}

using System;
using System.Collections.Generic;

namespace Functions.Server.Model;

public partial class User
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public Guid? ProfilePictureId { get; set; }

    public bool Notifications { get; set; }

    public virtual ICollection<EventVisitor> EventVisitors { get; set; } = new List<EventVisitor>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual Files? ProfilePicture { get; set; }
}

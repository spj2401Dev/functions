using System;
using System.Collections.Generic;

namespace Functions.Server.Model;

public partial class EventVisitor
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public int Type { get; set; }

    public Guid EventId { get; set; }

    public virtual Events Event { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace Functions.Server.Model;

public partial class FileContent
{
    public Guid Id { get; set; }

    public string? Base64Content { get; set; }

    public virtual ICollection<Files> Files { get; set; } = new List<Files>();
}

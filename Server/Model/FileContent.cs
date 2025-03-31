using System.ComponentModel.DataAnnotations;

namespace Functions.Server.Model
{
    public class FileContent
    {
        [Key]
        public Guid Id { get; set; }
        public required string Base64Content { get; set; }
    }
}

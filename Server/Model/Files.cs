using System.ComponentModel.DataAnnotations;

namespace Functions.Server.Model
{
    public class Files
    {
        [Key]
        public Guid Id { get; set; }
        public required string FileName { get; set; }
        public required string FileType { get; set; }
        public Guid FileContentId { get; set; }
        public required FileContent FileContent { get; set; }
    }
}

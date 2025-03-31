using Functions.Shared.Enum;
using System.ComponentModel.DataAnnotations;

namespace Functions.Server.Model
{
    public class Person
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Title { get; set; }
        public Gender Gender { get; set; }
        public string Street { get; set; }
        public string Addition { get; set; }
        public int Zip { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime SchoolGraduation { get; set; }
    }
}

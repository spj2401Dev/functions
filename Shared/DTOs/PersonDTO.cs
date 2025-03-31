using Functions.Shared.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functions.Shared.DTOs
{
    public class PersonDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public DateTime Birthday { get; set; }
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Street { get; set; } = string.Empty;
        public string Addition { get; set; } = string.Empty;
        [Required]
        public int? Zip { get; set; } = null;
        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        public EmploymentStatus? EmploymentStatus { get; set; } = null;
        // Pupils
        public SchoolType? SchoolType { get; set; } = null;
        public int? SchoolYear { get; set; } = null;
        // Students
        public int? Semester { get; set; } = null;
        public string? FieldOfStudy { get; set; } = null;
    }
}

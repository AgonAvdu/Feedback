using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class UpdateUserDto
    {
        [Required]
        public string Id { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]

        public string Surname { get; set; }
        [Required]

        public string Gender { get; set; }
        [Required]

        public string DateOfBirth { get; set; }
        public List<int> SubjectIds { get; set; }
        [Required]

        public int FacultyId { get; set; }
    }
}

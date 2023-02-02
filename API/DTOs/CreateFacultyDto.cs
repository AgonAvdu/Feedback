using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class CreateFacultyDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int CityId { get; set; }

    }
}

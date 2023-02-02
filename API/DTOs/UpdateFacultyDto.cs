using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class UpdateFacultyDto : CreateFacultyDto
    {
        [Required]
        public int Id { get; set; }
    }
}

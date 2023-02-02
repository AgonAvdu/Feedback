using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class UpdateCityDto : CreateCityDto
    {
        [Required]
        public int Id { get; set; }
    }
}

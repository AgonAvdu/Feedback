using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class CreateCityDto
    {
        [Required]
        public string Name { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class UpdateSubjectDto : CreateSubjectDto
    {
        [Required]
        public int Id { get; set; }
    }
}

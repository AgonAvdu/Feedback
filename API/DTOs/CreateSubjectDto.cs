using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class CreateSubjectDto
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        [Range(1, 6)]
        public int SemesterNr { get; set; }
    }
}
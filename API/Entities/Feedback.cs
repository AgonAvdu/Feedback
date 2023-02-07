using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }

        public string StudentId { get; set; }
        [ForeignKey("StudentId")]
        public AppUser Student { get; set; }

        public string TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public AppUser Teacher { get; set; }

        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; }
        public string Message { get; set; }

        public float SentimentScore { get; set; }


    }
}
using API.Entities;

namespace API.DTOs
{
    public class AnonymousFeedbackDto
    {
        public int Id { get; set; }
        public Subject Subject { get; set; }
        public string Message { get; set; }
        public float SentimentScore { get; set; }
    }
}

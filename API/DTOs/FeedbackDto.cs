using API.Entities;

namespace API.DTOs
{
    public class FeedbackDto
    {
        public int Id { get; set; }
        public AppUser Student { get; set; }
        public AppUser Teacher { get; set; }
        public Subject Subject { get; set; }
        public string Message { get; set; }
        public float SentimentScore { get; set; }
    }
}

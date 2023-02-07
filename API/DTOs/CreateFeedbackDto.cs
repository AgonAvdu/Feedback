namespace API.DTOs
{
    public class CreateFeedbackDto
    {
        public string StudentId { get; set; }
        public string TeachertId { get; set; }
        public int SubjectId { get; set; }

        public string Message { get; set; }


    }
}

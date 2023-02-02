namespace API.Entities
{
    public class TeacherSubject
    {
        public string TeacherId { get; set; }
        public AppUser Teacher{ get; set; }
        public int SubjectId { get; set; }
        public Subject Subject{ get; set; }
    }
}

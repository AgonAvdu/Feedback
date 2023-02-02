using API.Entities;

namespace API.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string DateOfBrith { get; set; }
        public string CreatedAt { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public List<SubjectDto> Subjects { get; set; }
        public FacultyDto Faculty { get; set; }

    }
}

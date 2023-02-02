
namespace API.DTOs
{
    public class RegisterTeacherDto : LoginDto
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string Gender { get; set; }
        public string DateOfBirth { get; set; }

    }
}


namespace API.DTOs
{
    public class RegisterUserDto : LoginDto
    {
        public string Username{ get; set; } 

        public string Name { get; set; }
        public string Surname { get; set; }

        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        
        public int FacultyId { get; set; }
        public string Role { get; set; }

        public List<int> SubjectIds{ get; set; }


    }
}

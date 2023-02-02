using API.Entities;

namespace API.DTOs
{
    public class FacultyDto
    {
        public int Id{ get; set; }
        public string Name { get; set; }
        public City City { get; set; }
    }
}

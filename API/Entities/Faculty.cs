namespace API.Entities
{
    public class Faculty
    {
            public int Id { get; set; }
            public string Name { get; set; }
            public int CityId{ get; set; }
            public City City { get; set; }

            public ICollection<AppUser> Users { get; set; }
    }
}

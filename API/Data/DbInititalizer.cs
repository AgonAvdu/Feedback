using API.Entities;
using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public static class DbInititalizer
    {
        public static async Task Initialize(DataContext context, UserManager<AppUser> userManager)
        {
            if (!context.Cities.Any())
            {

                var cities = new List<City>
                {
                    new City
                    {
                        Name = "Prishtin",
                    },
                    new City
                    {
                        Name = "Prizren",
                    },
                    new City
                    {
                        Name = "Lipjan",
                    },
                    new City
                    {
                        Name = "Gjilan",
                    },

                };
                foreach (var city in cities)
                {
                    context.Cities.Add(city);
                }
                context.SaveChanges();

            }

            var faculty1 = new Faculty
            {
                Name = "Objekti 1",
                CityId = 2,
                City = context.Cities.FindAsync(2).Result
            };

            if (!context.Faculties.Any())
            {

                var faculties = new List<Faculty>
                {
                    faculty1,
                    new Faculty
                    {
                        Name = "Objekti 2",
                        CityId = 1,
                        City = context.Cities.FindAsync(1).Result
                    },

                };
                foreach (var faculty in faculties)
                {
                    context.Faculties.Add(faculty);
                }
                context.SaveChanges();

            }


            if (!context.Subjects.Any())
            {

                var subjects = new List<Subject>
                {
                    new Subject
                    {
                        Name = "Matematika 1",
                        SemesterNr = 1,
                    },
                    new Subject
                    {
                        Name = "Matematika 2",
                        SemesterNr = 2,
                    },
                    new Subject
                    {
                        Name = "Struktur Disktrete 1",
                        SemesterNr = 3,
                    },
                    new Subject
                    {
                        Name = "Struktur Diskrete 2",
                        SemesterNr = 4,
                    },
                };
                foreach (var subject in subjects)
                {
                    context.Subjects.Add(subject);
                }
                context.SaveChanges();

            }


            if (!userManager.Users.Any())
            {
                var student = new AppUser
                {
                    UserName = "Agon",
                    Name = "Agon",
                    Surname = "Avdullahu",
                    Email = "ggoni2002@gmail.com",
                    Gender = "Male",
                    DateOfBirth = "18/07/2002",
                    CreatedAt = DateTime.Today.ToString("yyyy-MM-dd"),
                    FacultyId = 1,
                  
                };
                await userManager.CreateAsync(student, "Pa$$w0rd");
                await userManager.AddToRoleAsync(student, "Student");

                var admin = new AppUser
                {
                    UserName = "Admin",
                    Name = "Admin",
                    Surname = "Admin",
                    Email = "admin@gmail.com",
                    Gender = "Male",
                    DateOfBirth = "18/07/2002",
                    CreatedAt = DateTime.Today.ToString("yyyy-MM-dd"),
                    FacultyId= 1,
                };
                await userManager.CreateAsync(admin, "Pa$$w0rd");
                await userManager.AddToRoleAsync(admin, "Admin");

                var teacher = new AppUser
                {
                    UserName = "Teacher",
                    Name = "Teacher",
                    Surname = "Teacher",
                    Email = "teacher@gmail.com",
                    Gender = "Male",
                    DateOfBirth = "18/07/2002",
                    CreatedAt = DateTime.Today.ToString("yyyy-MM-dd"),
                    FacultyId = 1,
                
                };
                await userManager.CreateAsync(teacher, "Pa$$w0rd");
                await userManager.AddToRoleAsync(teacher, "Teacher");
            }


            context.SaveChanges();
        }
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace API.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string CreatedAt { get; set; }
        public virtual ICollection<TeacherSubject> TeacherSubjects { get; set; }
        public int FacultyId { get; set; }
        public Faculty Faculty{ get; set; }

    }

}
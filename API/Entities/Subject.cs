using System.Reflection.Metadata;

namespace API.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SemesterNr { get; set; }

        public virtual ICollection<TeacherSubject> TeacherSubjects { get; set; }

    }

}

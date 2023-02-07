using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Emit;

namespace API.Data
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions<DataContext> opt) : base(opt)
        {

        }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<TeacherSubject> TeacherSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TeacherSubject>()
                .HasKey(bc => new { bc.TeacherId, bc.SubjectId });
            builder.Entity<TeacherSubject>()
                .HasOne(bc => bc.Teacher)
                .WithMany(b => b.TeacherSubjects)
                .HasForeignKey(bc => bc.TeacherId);
            builder.Entity<TeacherSubject>()
                .HasOne(bc => bc.Subject)
                .WithMany(c => c.TeacherSubjects)
                .HasForeignKey(bc => bc.SubjectId);

            builder.Entity<AppUser>()
                .HasOne(u => u.Faculty)
                .WithMany(f => f.Users)
                .HasForeignKey(u => u.FacultyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Feedback>()
                .HasIndex(e => new { e.StudentId, e.SubjectId })
                .IsUnique();

            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().
                HasData(
                    new IdentityRole { Name = "Student", NormalizedName = "STUDENT" },
                    new IdentityRole { Name = "Teacher", NormalizedName = "TEACHER" },
                    new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" }
                );
        }
    }
}
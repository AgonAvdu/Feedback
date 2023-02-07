using API.Data;
using API.DTOs;
using API.Entities;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public readonly TokenService _tokenService;
        public UsersController(UserManager<AppUser> userManager, TokenService tokenService, DataContext context, IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPut]
        public async Task<ActionResult<AppUser>> UpdateUser([FromForm] UpdateUserDto updateUserDto)
        {
            var user = await _userManager.FindByIdAsync(updateUserDto.Id);
            if (user == null)
            {
                return NotFound();
            }

            var faculty = await _context.Faculties.FindAsync(user.FacultyId);
            var city = await _context.Cities.FindAsync(user.Faculty.CityId);
            FacultyDto facultyData = new FacultyDto
            {
                Id = faculty.Id,
                Name = faculty.Name,
                City = city,
            };

            user.Name = updateUserDto.Name;
            user.Surname = updateUserDto.Surname;
            user.Gender = updateUserDto.Gender;
            user.DateOfBirth = updateUserDto.DateOfBirth;
            user.FacultyId = updateUserDto.FacultyId;
            user.Faculty = faculty;

            var role = await _userManager.GetRolesAsync(user);

            if (role.FirstOrDefault() == "Teacher")
            {
                var subjects = _context.Subjects.Where(s => updateUserDto.SubjectIds.Contains(s.Id)).ToList();

                List<TeacherSubject> list = new List<TeacherSubject> { };

                foreach (var subject in subjects)
                {
                    list.Add(new TeacherSubject { Subject = subject });

                }
                if (list != null)
                {
                    var rowsToDelete = _context.TeacherSubjects.Where(x => x.TeacherId == updateUserDto.Id);
                    _context.TeacherSubjects.RemoveRange(rowsToDelete);
                    _context.SaveChanges();
                    user.TeacherSubjects = list;
                }
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromForm] string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }


            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }

            return BadRequest();
        }
    }
}

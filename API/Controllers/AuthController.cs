using API.Data;
using API.DTOs;
using API.Entities;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AuthController : BaseApiController

    {
        private readonly UserManager<AppUser> _userManager;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public readonly TokenService _tokenService;
        public AuthController(UserManager<AppUser> userManager, TokenService tokenService, DataContext context, IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("login")]

        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return Unauthorized();
            };

            var faculty = await _context.Faculties.FindAsync(user.FacultyId);
            var city = await _context.Cities.FindAsync(user.Faculty.CityId);
            FacultyDto facultyData = new FacultyDto
            {
                Id = faculty.Id,
                Name = faculty.Name,
                City = city,
            };


            var role = await _userManager.GetRolesAsync(user);



            if (role[0] == "Teacher")
            {

                var subjectTeachers = _context.TeacherSubjects.Include(st => st.Subject).Where(st => st.TeacherId == user.Id).ToList();
                List<SubjectDto> teacherSubjects = new List<SubjectDto>();
                foreach (var subject in subjectTeachers)
                {
                    teacherSubjects.Add(new SubjectDto
                    {
                        Id = subject.SubjectId,
                        Name = subject.Subject.Name,
                        SemesterNr = subject.Subject.SemesterNr,
                    });
                }


                return new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Gender = user.Gender,
                    DateOfBrith = user.DateOfBirth,
                    CreatedAt = user.CreatedAt,
                    Token = await _tokenService.GenerateToken(user),
                    Subjects = teacherSubjects,
                    Faculty = facultyData

                };
            } else
            {
                return new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Gender = user.Gender,
                    DateOfBrith = user.DateOfBirth,
                    CreatedAt= user.CreatedAt,
                    Token = await _tokenService.GenerateToken(user),
                    Faculty = facultyData
                };
            }

             
           
        }

        [HttpPost("register/user")]

        public async Task<ActionResult> RegisterUser([FromForm] RegisterUserDto registerDto)
        {
            var faculty = await _context.Faculties.FindAsync(registerDto.FacultyId);

            List<TeacherSubject> list = new List<TeacherSubject> { };
            if(registerDto.Role == "Teacher")
            {
                var subjects = _context.Subjects.Where(s => registerDto.SubjectIds.Contains(s.Id)).ToList();
                if(subjects == null) return BadRequest(new ProblemDetails { Title = "Subjects with the given ID not found" });

                foreach (var subject in subjects)
                {
                    list.Add(new TeacherSubject { Subject = subject });

                }
            }

            var user = new AppUser
            {
                UserName = registerDto.Username,
                Name = registerDto.Name,
                Surname = registerDto.Surname,
                Email = registerDto.Email,
                Gender = registerDto.Gender,
                DateOfBirth = registerDto.DateOfBirth,
                CreatedAt = DateTime.Today.ToString("yyyy-MM-dd"),
                FacultyId = registerDto.FacultyId,
                Faculty = faculty,
            };
            if (list != null) user.TeacherSubjects = list;


            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return ValidationProblem();
            }
            await _userManager.AddToRoleAsync(user, registerDto.Role);
            return StatusCode(201);
        }

        [Authorize]
        [HttpGet("currentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var faculty = await _context.Faculties.FindAsync(user.FacultyId);
            var city = await _context.Cities.FindAsync(user.Faculty.CityId);
            FacultyDto facultyData = new FacultyDto
            {
                Id = faculty.Id,
                Name = faculty.Name,
                City = city,
            };

            var role = await _userManager.GetRolesAsync(user);



            if (role[0] == "Teacher")
            {

                var subjectTeachers = _context.TeacherSubjects.Include(st => st.Subject).Where(st => st.TeacherId == user.Id).ToList();
                List<SubjectDto> teacherSubjects = new List<SubjectDto>();
                foreach (var subject in subjectTeachers)
                {
                    teacherSubjects.Add(new SubjectDto
                    {
                        Id = subject.SubjectId,
                        Name = subject.Subject.Name,
                        SemesterNr = subject.Subject.SemesterNr,
                    });
                }


                return new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Gender = user.Gender,
                    DateOfBrith = user.DateOfBirth,
                    CreatedAt = user.CreatedAt,
                    Token = await _tokenService.GenerateToken(user),
                    Subjects = teacherSubjects,
                    Faculty = facultyData

                };
            }
            else
            {
                return new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Gender = user.Gender,
                    DateOfBrith = user.DateOfBirth,
                    CreatedAt = user.CreatedAt,
                    Token = await _tokenService.GenerateToken(user),
                    Faculty = facultyData
                };
            }
        }

    }
}

using API.Data;
using API.Entities;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TeacherController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public readonly TokenService _tokenService;
        public TeacherController(UserManager<AppUser> userManager, TokenService tokenService, DataContext context, IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{facultyId}")]
        public async Task<ActionResult<List<AppUser>>> GetTeachersByFaculty(int facultyId)
        {
            var teachers = await _userManager.GetUsersInRoleAsync("Teacher");

            return teachers.Where(u => u.FacultyId == facultyId).ToList();

        }
    }
    
}

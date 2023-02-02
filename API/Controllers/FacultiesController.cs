using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class FacultiesController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public FacultiesController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Faculty>>> GetFaculties()
        {
            var faculties = await _context.Faculties
                .Include(f => f.City)
                .ToListAsync();

            return faculties;
        }
        // [Authorize(Roles = "Admin")]
        [HttpGet("{id}", Name = "GetFaculty")]
        public async Task<ActionResult<Faculty>> GetFaculty(int id)
        {
            return await _context.Faculties.Include(f => f.City).FirstOrDefaultAsync(f => f.Id == id);
        }

        // [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Faculty>> CreateFaculty([FromForm] CreateFacultyDto facultyDto)
        {
            var city = _context.Cities.Find(facultyDto.CityId);
            if (city == null) return BadRequest(new ProblemDetails { Title = "City with the given ID not found" });

            Faculty faculty = new Faculty
            {
                Name = facultyDto.Name,
                CityId = facultyDto.CityId,
                City = city
            };
            _context.Faculties.Add(faculty);

            var result = await _context.SaveChangesAsync() > 0;
            if (result) return CreatedAtAction("GetFaculty", new { id = faculty.Id }, faculty);
            return BadRequest(new ProblemDetails { Title = "Problem creating new Faculty" });
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult<Faculty>> EditFaculty([FromForm] UpdateFacultyDto facultyDto)
        {
            var faculty = await _context.Faculties.Include(f => f.City).FirstOrDefaultAsync(f => f.Id == facultyDto.Id);

            if (faculty == null)
            {
                return NotFound();
            }

            var city = await _context.Cities.FindAsync(facultyDto.CityId);
            if (city == null)
            {
                return BadRequest(new ProblemDetails { Title = "City with the given ID was not found" });
            }

            faculty.Name = facultyDto.Name;
            faculty.City = city;

            _context.Faculties.Update(faculty);
            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok(faculty);
            return BadRequest(new ProblemDetails { Title = "Problem editing faculty" });
        }

        // [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFaculty(int id)
        {
            var faculty = await _context.Faculties.FindAsync(id);
            if (faculty == null)
            {
                return NotFound();
            }

            _context.Faculties.Remove(faculty);
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}

using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class SubjectsController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public SubjectsController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<ActionResult<List<Subject>>> GetSubjects()
        {
            return await _context.Subjects.ToListAsync();
        }
        // [Authorize(Roles = "Admin")]
        [HttpGet("{id}", Name = "GetSubject")]
        public async Task<ActionResult<Subject>> GetSubject(int id)
        {
            return await _context.Subjects.FindAsync(id);
        }

        // [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Subject>> CreateSubject([FromForm] CreateSubjectDto subjectDto)
        {

            Subject subject = new Subject{};
            _mapper.Map(subjectDto, subject);
            _context.Subjects.Add(subject);

            var saveResult = await _context.SaveChangesAsync();

            if (saveResult > 0)
            {
                return CreatedAtRoute("GetSubject", new { id = subject.Id }, subject);
            }
            return BadRequest(new ProblemDetails { Title = "Problem creating new Subject" });
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult<Subject>> UpdateSubject([FromForm] UpdateSubjectDto updateSubjectDto)
        {
            var subject = await _context.Subjects.FindAsync(updateSubjectDto.Id);
            if (subject == null) return NotFound();
            _mapper.Map(updateSubjectDto, subject);

            var result = await _context.SaveChangesAsync() > 0;
            if (result) return Ok(subject);
            return BadRequest(new ProblemDetails { Title = "Problem updating Subject" });
        }

        // [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSubject(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null) return NotFound();

           
            _context.Subjects.Remove(subject);
            var result = await _context.SaveChangesAsync() > 0;
            if (result) return Ok();
            return BadRequest(new ProblemDetails { Title = "Problem deleting Subject" });

        }

    }
}

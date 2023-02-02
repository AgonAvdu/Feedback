using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class CitiesController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CitiesController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<ActionResult<List<City>>> GetCities()
        {
            return await _context.Cities.ToListAsync();
        }
        // [Authorize(Roles = "Admin")]
        [HttpGet("{id}", Name = "GetCity")]
        public async Task<ActionResult<City>> GetCity(int id)
        {
            return await _context.Cities.FindAsync(id);
        }

        // [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<City>> CreateCity([FromForm] CreateCityDto createCityDto)
        {

            City city= new City{};
            _mapper.Map(createCityDto, city);
            _context.Cities.Add(city);

            var saveResult = await _context.SaveChangesAsync();

            if (saveResult > 0)
            {
                return CreatedAtRoute("GetCity", new { id = city.Id }, city);
            }
            return BadRequest(new ProblemDetails { Title = "Problem creating new City" });
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult<City>> UpdateCity([FromForm] UpdateCityDto updateCityDto)
        {
            var city = await _context.Cities.FindAsync(updateCityDto.Id);
            if (city == null) return NotFound();
            _mapper.Map(updateCityDto, city);

            var result = await _context.SaveChangesAsync() > 0;
            if (result) return Ok(city);
            return BadRequest(new ProblemDetails { Title = "Problem updating City" });
        }

        // [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCity(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city == null) return NotFound();

           
            _context.Cities.Remove(city);
            var result = await _context.SaveChangesAsync() > 0;
            if (result) return Ok();
            return BadRequest(new ProblemDetails { Title = "Problem deleting City" });

        }

    }
}

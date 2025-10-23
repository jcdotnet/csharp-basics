﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CitiesManager.WebAPI.Entities;

namespace CitiesManager.WebAPI.Controllers
{
    /// <summary>
    ///  Represents the Cities Manager API controller class
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class CitiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Cities
        /// <summary>
        /// Gets a list of cities from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
            return await _context.Cities.ToListAsync();
        }

        // GET: api/Cities/5
        /// <summary>
        /// Get a city from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(Guid id)
        {
            var city = await _context.Cities.FindAsync(id);

            if (city == null)
            {
                //return NotFound();
                return Problem(detail: "Invalid City Id", statusCode: 400, title: "Get City");
            }

            return city;
        }

        // PUT: api/Cities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Updates a city in the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="city"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(Guid id, City city)
        {
            if (id != city.Id)
            {
                return BadRequest();
            }

            //_context.Entry(city).State = EntityState.Modified;
            var tempCity = await _context.Cities.FindAsync(id);
            if (tempCity == null) return NotFound();
            tempCity.Name = city.Name;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Creates a city in the database
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<City>> PostCity(City city)
        {
            if (_context.Cities == null)
            {
                //return BadRequest();
                return Problem("Entity set 'ApplicationDbContext.Cities' is null.");
            }
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCity", new { id = city.Id }, city);
        }

        // DELETE: api/Cities/5
        /// <summary>
        /// Deletes a city from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(Guid id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CityExists(Guid id)
        {
            return _context.Cities.Any(e => e.Id == id);
        }
    }
}

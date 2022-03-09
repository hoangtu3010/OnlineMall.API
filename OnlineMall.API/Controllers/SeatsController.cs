using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineMall.API.Models;

namespace OnlineMall.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SeatsController : ControllerBase
    {
        private readonly SystemDbContext _context;

        public SeatsController(SystemDbContext context)
        {
            _context = context;
        }

        // GET: api/Seats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seats>>> GetSeats()
        {
            return await _context.Seats.ToListAsync();
        }

        // GET: api/Seats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Seats>> GetSeats(int id)
        {
            var seats = await _context.Seats.FindAsync(id);

            if (seats == null)
            {
                return NotFound();
            }

            return seats;
        }

        // PUT: api/Seats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeats(int id, Seats seats)
        {
            if (id != seats.Id)
            {
                return BadRequest();
            }

            _context.Entry(seats).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeatsExists(id))
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

        // POST: api/Seats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Seats>> PostSeats(Seats seats)
        {
            var find =_context.Seats.Where(c=>c.Column==seats.Column&&c.Row==seats.Row);
            if (find.Any())
            {
                return BadRequest();
            }
            _context.Seats.Add(seats);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSeats", new { id = seats.Id }, seats);
        }

        // DELETE: api/Seats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeats(int id)
        {
            var seats = await _context.Seats.FindAsync(id);
            if (seats == null)
            {
                return NotFound();
            }

            _context.Seats.Remove(seats);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SeatsExists(int id)
        {
            return _context.Seats.Any(e => e.Id == id);
        }
    }
}

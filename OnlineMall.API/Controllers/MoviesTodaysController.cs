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
    public class MoviesTodaysController : ControllerBase
    {
        private readonly SystemDbContext _context;

        public MoviesTodaysController(SystemDbContext context)
        {
            _context = context;
        }

        // GET: api/MoviesTodays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MoviesToday>>> GetMoviesToday()
        {
            return await _context.MoviesToday.Include(m => m.Movies).ToListAsync();
        }

        // GET: api/MoviesTodays/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MoviesToday>> GetMoviesToday(int id)
        {
            var moviesToday = await _context.MoviesToday.Include(c=>c.Movies).FirstOrDefaultAsync(i => i.Id == id);

            if (moviesToday == null)
            {
                return NotFound();
            }
            
            return moviesToday;
        }

        // PUT: api/MoviesTodays/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMoviesToday(int id, MoviesToday moviesToday)
        {
            if (id != moviesToday.Id)
            {
                return BadRequest();
            }

            _context.Entry(moviesToday).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoviesTodayExists(id))
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

        // POST: api/MoviesTodays
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MoviesToday>> PostMoviesToday(MoviesToday moviesToday)
        {
            _context.MoviesToday.Add(moviesToday);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMoviesToday", new { id = moviesToday.Id }, moviesToday);
        }

        // DELETE: api/MoviesTodays/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMoviesToday(int id)
        {
            var moviesToday = await _context.MoviesToday.FindAsync(id);
            if (moviesToday == null)
            {
                return NotFound();
            }

            _context.MoviesToday.Remove(moviesToday);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MoviesTodayExists(int id)
        {
            return _context.MoviesToday.Any(e => e.Id == id);
        }
    }
}

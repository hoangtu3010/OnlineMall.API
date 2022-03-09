using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineMall.API.Models;
using OnlineMall.API.Services;

namespace OnlineMall.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly SystemDbContext _context;
        private readonly IMailService mailService;

      
        public BookingsController(SystemDbContext context, IMailService mailService)
        {
            _context = context;
            this.mailService = mailService;

        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            return await _context.Bookings.ToListAsync();
        }
        [HttpGet]
        public async Task<IEnumerable<Seats>> GetSeatActive(int movieToday)
        {
            var ListBooking = await _context.Bookings.Include(c=>c.MoviesToday).Include(d=>d.Seats).Where(e=>e.MoviesTodayId==movieToday).ToListAsync();
            var listSeat = new List<Seats>();
            foreach (var item in ListBooking)
            {
                if (!listSeat.Contains(item.Seats))
                {
                    listSeat.Add(item.Seats);

                }
            }
            return listSeat;
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }

        // PUT: api/Bookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, Booking booking)
        {
            if (id != booking.Id)
            {
                return BadRequest();
            }

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
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

        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            var res2 = new WelcomeRequest();
            await _context.SaveChangesAsync();
            var seats = await _context.Seats.FindAsync(booking.SeatsId);
            res2.ToEmail = booking.Email;
            res2.UserName = booking.Name;
            res2.TICKET = "Seat's name: "+ seats.Name +", row: "+ seats.Row + ", column: " + seats.Column;

            await mailService.SendWelcomeEmailAsync(res2);


            return CreatedAtAction("GetBooking", new { id = booking.Id }, booking);
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }
    }
}

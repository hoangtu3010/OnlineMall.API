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
    public class ShopsController : ControllerBase
    {
        private readonly SystemDbContext _context;

        public ShopsController(SystemDbContext context)
        {
            _context = context;
        }

        // GET: api/Shops
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shop>>> GetShops()
        {
            return await _context.Shops.Include(c=>c.Department).Select(x=>new Shop() { 
            Id=x.Id,Name=x.Name,ImageName=x.ImageName,ImageLogoName=x.ImageLogoName,
            Contact=x.Contact,ShowTime=x.ShowTime,Link=x.Link,Department=x.Department,
            DepartmentId=x.DepartmentId,Description=x.Description,
            ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageName),
            ImageLogoSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageLogoName)
            }).ToListAsync();
        }

        // GET: api/Shops/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shop>> GetShop(int id)
        {
            var shop = await _context.Shops.FindAsync(id);
            shop.ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, shop.ImageName);
            shop.ImageLogoSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, shop.ImageLogoName);

            if (shop == null)
            {
                return NotFound();
            }

            return shop;
        }

        // PUT: api/Shops/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShop(int id, Shop shop)
        {
            if (id != shop.Id)
            {
                return BadRequest();
            }

            _context.Entry(shop).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShopExists(id))
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

        // POST: api/Shops
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Shop>> PostShop(Shop shop)
        {
            _context.Shops.Add(shop);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShop", new { id = shop.Id }, shop);
        }

        // DELETE: api/Shops/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShop(int id)
        {
            var shop = await _context.Shops.FindAsync(id);
            if (shop == null)
            {
                return NotFound();
            }

            _context.Shops.Remove(shop);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShopExists(int id)
        {
            return _context.Shops.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThreeSItSolutionsWebApi.Models;

namespace ThreeSItSolutionsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly Db3SItSoultion _context;

        public ContactUsController(Db3SItSoultion context)
        {
            _context = context;
        }

        // GET: api/ContactUs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MContactUs>>> GetMContactUs()
        {
            return await _context.MContactUs.ToListAsync();
        }

        // GET: api/ContactUs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MContactUs>> GetMContactUs(int id)
        {
            var mContactUs = await _context.MContactUs.FindAsync(id);

            if (mContactUs == null)
            {
                return NotFound();
            }

            return mContactUs;
        }

        // PUT: api/ContactUs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMContactUs(int id, MContactUs mContactUs)
        {
            if (id != mContactUs.IID)
            {
                return BadRequest();
            }

            _context.Entry(mContactUs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MContactUsExists(id))
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

        // POST: api/ContactUs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MContactUs>> PostMContactUs(MContactUs mContactUs)
        {
            _context.MContactUs.Add(mContactUs);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMContactUs", new { id = mContactUs.IID }, mContactUs);
        }

        // DELETE: api/ContactUs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MContactUs>> DeleteMContactUs(int id)
        {
            var mContactUs = await _context.MContactUs.FindAsync(id);
            if (mContactUs == null)
            {
                return NotFound();
            }

            _context.MContactUs.Remove(mContactUs);
            await _context.SaveChangesAsync();

            return mContactUs;
        }

        private bool MContactUsExists(int id)
        {
            return _context.MContactUs.Any(e => e.IID == id);
        }
    }
}

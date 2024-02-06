
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LSS.Model;
using LSS.Persistence;

namespace LSS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaterfallController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WaterfallController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Waterfall
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Waterfall>>> GetWaterfalls()
        {
            return await _context.Waterfalls.ToListAsync();
        }

        // GET: api/Waterfall/5
        [HttpGet("{w_name}")]
        public IActionResult GetWaterfall(string w_name)
        {
            var waterfall = _context.GetWaterfallByName(w_name);
            if (waterfall == null)
            {
                return NotFound();
            }
            return Ok(waterfall);
        }

        // PUT: api/Waterfall/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWaterfall(int id, Waterfall waterfall)
        {
            if (id != waterfall.id)
            {
                return BadRequest();
            }

            _context.Entry(waterfall).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WaterfallExists(id))
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

        // POST: api/Waterfall
        [HttpPost]
        public async Task<ActionResult<Waterfall>> PostWaterfall(Waterfall waterfall)
        {
            _context.Waterfalls.Add(waterfall);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWaterfall", new { w_name = waterfall.w_name }, waterfall);
        }

        // DELETE: api/Waterfall/5
        [HttpDelete("{id}")]
        public IActionResult DeleteWaterfall(int id)
        {
            Waterfall waterfall = _context.Waterfalls.Find(id);
            if (waterfall == null)
            {
                return NotFound();
            }
            List <LoanDetails> loans = _context.Loans.ToList();
            for(int i=0 ; i<loans.Count ; i++)
            {
                if(loans[i].waterfall_name == waterfall.w_name)
                {
                    return BadRequest("This waterfall is associated with other loans."); 
                }
            } 
            _context.Waterfalls.Remove(waterfall);
            _context.SaveChanges();
            return NoContent();
        }

        private bool WaterfallExists(int id)
        {
            return _context.Waterfalls.Any(e => e.id == id);
        }
    }
}


using LSS.Model;
using LSS.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LSS.Controllers
{
    [ApiController]
    [Route("api/")]
    public class EscrowMasterController : ControllerBase


    {

        private readonly AppDbContext _context;

        public EscrowMasterController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("emaster/")]
        public async Task<ActionResult<IEnumerable<Escrow>>> GetLoans()
        {
            return await _context.Escrows.ToListAsync();
        }
        [HttpGet("emaster/{id}")]
        public async Task<ActionResult<Escrow>> GetLoanDetails(int id)
        {
            var eDetails = await _context.Escrows.FindAsync(id);

            if (eDetails == null)
            {
                return NotFound("Id not found !!");
            }

            return eDetails;
        }

        [HttpDelete("emaster/{id}")]
        public async Task<IActionResult> DeleteLoanDetails(int id)
        {
            var escrow = await _context.Escrows.FindAsync(id);
            if (escrow == null)
            {
                return NotFound();
            }

            _context.Escrows.Remove(escrow);
            await _context.SaveChangesAsync();

            return Ok("Successfully Deleted " + id + " !!");
        }

        [HttpPut("emaster/{id}")]
        public IActionResult PutLoanDetails(int id, Escrow e)
        {
            if (id != e.id)
            {
                return BadRequest();
            }

            _context.Entry(e).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return Ok(e);
        }
    }
}

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
    }
}
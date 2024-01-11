using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LSS.Model;
using LSS.Persistence;

namespace LSS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LoansController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Loans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoanDetails>>> GetLoans()
        {
            return await _context.Loans.ToListAsync();
        }

        // GET: api/Loans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoanDetails>> GetLoanDetails(Guid id)
        {
            var loanDetails = await _context.Loans.FindAsync(id);

            if (loanDetails == null)
            {
                return NotFound();
            }

            return loanDetails;
        }

        // PUT: api/Loans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoanDetails(Guid id, LoanDetails loanDetails)
        {
            if (id != loanDetails.Id)
            {
                return BadRequest();
            }

            _context.Entry(loanDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoanDetailsExists(id))
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

        // POST: api/Loans
        [HttpPost]
        public async Task<ActionResult<LoanDetails>> PostLoanDetails(LoanDetails loanDetails)
        {
            _context.Loans.Add(loanDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLoanDetails", new { id = loanDetails.Id }, loanDetails);
        }

        // POST: api/Loans/Bulk
        [HttpPost("Bulk")]
        public async Task<ActionResult<IEnumerable<LoanDetails>>> PostLoansBulk(List<LoanDetails> loanDetailsList)
        {
            if (loanDetailsList == null || !loanDetailsList.Any())
            {
                return BadRequest("No loan details provided");
            }

            _context.Loans.AddRange(loanDetailsList);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLoans), loanDetailsList);
        }

        // DELETE: api/Loans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoanDetails(Guid id)
        {
            var loanDetails = await _context.Loans.FindAsync(id);
            if (loanDetails == null)
            {
                return NotFound();
            }

            _context.Loans.Remove(loanDetails);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LoanDetailsExists(Guid id)
        {
            return _context.Loans.Any(e => e.Id == id);
        }
    }
}


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LSS.Model;
using LSS.Persistence;

namespace LSS.Controllers
{
    [Route("api/")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PaymentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Payments
        [HttpGet("history/{id}")]
        public async Task<ActionResult<IEnumerable<PaymentHistory>>> GetAllPaymentHistory(Guid id)
        {
            IQueryable<PaymentHistory> query = _context.PaymentHistory;
            if (id != Guid.Empty)
            {
                query = query.Where(ld => ld.loan_id == id);
            }
            return await query.ToListAsync();
        }

        // // GET: api/Payments/5
        // [HttpGet("/{id}")]
        // public async Task<ActionResult<PaymentHistory>> GetPaymentHistory(Guid id)
        // {
        //     var paymentHistory = await _context.PaymentHistory.FindAsync(id);

        //     if (paymentHistory == null)
        //     {
        //         return NotFound();
        //     }

        //     return paymentHistory;
        // }

        // PUT: api/Payments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("history/{id}")]
        public async Task<IActionResult> PutPaymentHistory(Guid id, PaymentHistory paymentHistory)
        {
            if (id != paymentHistory.Id)
            {
                return BadRequest();
            }

            _context.Entry(paymentHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentHistoryExists(id))
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

        // POST: api/Payments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("history")]
        public async Task<ActionResult<PaymentHistory>> PostPaymentHistory(PaymentHistory paymentHistory)
        {
            _context.PaymentHistory.Add(paymentHistory);
            await _context.SaveChangesAsync();

            return Ok(paymentHistory);
        }

        // DELETE: api/Payments/5
        [HttpDelete("history/{id}")]
        public async Task<IActionResult> DeletePaymentHistory(Guid id)
        {
            var paymentHistory = await _context.PaymentHistory.FindAsync(id);
            if (paymentHistory == null)
            {
                return NotFound();
            }

            _context.PaymentHistory.Remove(paymentHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentHistoryExists(Guid id)
        {
            return _context.PaymentHistory.Any(e => e.Id == id);
        }
    }
}

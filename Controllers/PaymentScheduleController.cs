
using LSS.Model;
using LSS.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace LSS.Controllers
{

    [Route("api/")]
    [ApiController]
    public class PaymentScheduleController: ControllerBase
    {
        private readonly AppDbContext _context;

        public PaymentScheduleController(AppDbContext context)
        {
            _context = context;
        }
        public static readonly string[] Months =
        [
            "January", "February", "March", "April", "May", "June",
            "July", "August", "September", "October", "November", "December"
        ];
        [HttpGet("schedule/{id}")]
        public List<PaymentSchedule> CalculatePaymentSchedules(Guid id)
        {
            var loanDetails =  _context.Loans.Find(id);

            // if (loanDetails == null)
            // {
            //     return NotFound();
            // }

            float monthly_interest_rate = loanDetails.current_rate / 1200;
            double temp_upb = loanDetails.upb_amount;
            // float escrow_by_twelve = escrow / 12;

            List<PaymentSchedule> paymentSchedules = new List<PaymentSchedule>();


            for (int i = 1; i <= 12; i++)
            {
                double monthly_interest_pmt = temp_upb * monthly_interest_rate;
                double monthly_pmt =  (loanDetails.upb_amount* monthly_interest_rate * Math.Pow((double)(1 + monthly_interest_rate), 180)) /((Math.Pow((double)1 + monthly_interest_rate, 180)) - 1);
                double principal = monthly_pmt - monthly_interest_pmt ;
                double curr_upb = temp_upb - principal;
                

                PaymentSchedule schedule = new PaymentSchedule
                {
                    month = Months[i - 1],
                    annual_interest_rate = loanDetails.current_rate,
                    upb_amount = curr_upb,
                    monthly_payment = monthly_pmt,
                    interest_amount = monthly_interest_pmt,
                    principal_amount = principal,
                    // escrow = escrow
                };

                paymentSchedules.Add(schedule);
                temp_upb = curr_upb;
            }

            return paymentSchedules;
        }


    }

}
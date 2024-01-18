
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
            double curr_upb = loanDetails.upb_amount;
            double principal = 0;
            double escrow_by_twelve = loanDetails.escrow_amount/12;

            List<PaymentSchedule> paymentSchedules = new List<PaymentSchedule>();


            for (int i = 1; i <= 12; i++)
            {
                curr_upb = curr_upb - principal;
                double monthly_interest_pmt = curr_upb * monthly_interest_rate;
                double monthly_pmt =  (loanDetails.upb_amount* monthly_interest_rate * Math.Pow((double)(1 + monthly_interest_rate), 180)) /((Math.Pow((double)1 + monthly_interest_rate, 180)) - 1)+ escrow_by_twelve;
                principal = monthly_pmt - monthly_interest_pmt - escrow_by_twelve;
                
                

                PaymentSchedule schedule = new PaymentSchedule
                {
                    month = Months[i - 1],
                    annual_interest_rate = loanDetails.current_rate,
                    upb_amount = curr_upb,
                    monthly_payment = monthly_pmt,
                    interest_amount = monthly_interest_pmt,
                    principal_amount = principal,
                    escrow = escrow_by_twelve
                };

                paymentSchedules.Add(schedule);
            }

            return paymentSchedules;
        }


    }

}
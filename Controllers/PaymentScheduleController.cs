
using LSS.Model;
using LSS.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LSS.Controllers
{

    [Route("api/")]
    [ApiController]
    public class PaymentScheduleController : ControllerBase
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
            var loanDetails = _context.Loans.Find(id);

            // if (loanDetails == null)
            // {
            //     return NotFound();
            // }

            float monthly_interest_rate = loanDetails.current_rate / 1200;
            double curr_upb = loanDetails.upb_amount;
            double principal = 0;
            double escrow_by_twelve = loanDetails.escrow_amount / 12;

            List<PaymentSchedule> paymentSchedules = new List<PaymentSchedule>();

            double monthly_pmt = (loanDetails.upb_amount * monthly_interest_rate * Math.Pow((double)(1 + monthly_interest_rate), 180)) / ((Math.Pow((double)1 + monthly_interest_rate, 180)) - 1) + escrow_by_twelve;

            for (int i = 1; i <= 12; i++)
            {
                double monthly_interest_pmt = curr_upb * monthly_interest_rate;
                principal = monthly_pmt - monthly_interest_pmt - escrow_by_twelve;
                curr_upb = curr_upb - principal;



                PaymentSchedule schedule = new PaymentSchedule
                {
                    month = Months[i - 1] + " - 2024",
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

        [HttpPut("payment/{id}")]
        public PaymentSchedule CalculatePayment(Guid id, LoanDetails loanDetails)
        {
            PaymentSchedule payment = new PaymentSchedule("January", 0, 0, 0, 0, 0, 0, 0, 0);

            float monthly_interest_rate = loanDetails.current_rate / 1200;
            double curr_upb = loanDetails.upb_amount;
            double escrow_by_twelve = loanDetails.escrow_amount / 12;
            double rpmt = loanDetails.last_pmt_amount;

            // Supposed values:
            int late_fee = 50;
            double e_principal = 100;

            double monthly_pmt = (loanDetails.loan_amount * monthly_interest_rate * Math.Pow((double)(1 + monthly_interest_rate), 180)) / ((Math.Pow((double)1 + monthly_interest_rate, 180)) - 1) + escrow_by_twelve;
            double monthly_interest_pmt = loanDetails.upb_amount * monthly_interest_rate;
            payment.monthly_payment = loanDetails.last_pmt_amount;
            //Principal priority hierarchy
            double principal = monthly_pmt - monthly_interest_pmt - escrow_by_twelve;
            if (rpmt >= monthly_interest_pmt)
            {
                payment.interest_amount = monthly_interest_pmt;
                rpmt -= monthly_interest_pmt;
            }
            else
            {
                payment.interest_amount = rpmt;
                rpmt = 0;
                return payment;
            }

            // 2nd priority interest 
            if (rpmt >= principal) {
                payment.principal_amount = principal;
                
                rpmt -= principal;
                loanDetails.upb_amount -= principal;
                payment.upb_amount = loanDetails.upb_amount;
                _context.Entry(loanDetails).State = EntityState.Modified;
                _context.SaveChanges();
            }
            else {
                payment.principal_amount = rpmt;
                loanDetails.upb_amount -= rpmt;
                payment.upb_amount = loanDetails.upb_amount;
                rpmt = 0;
                _context.Entry(loanDetails).State = EntityState.Modified;
                _context.SaveChanges();
                return payment;

            }
            
            // 3rd priority escrow.
            if(rpmt >= escrow_by_twelve) {
                payment.escrow = escrow_by_twelve;
                rpmt -= escrow_by_twelve;
            }
            else {
                payment.escrow = rpmt;
                rpmt = 0;
                return payment;
            }
            // 4th priority late fee
            if(rpmt >= late_fee) {
                rpmt -= late_fee;
                payment.late_fee = late_fee;
            }
            else {
                payment.late_fee = rpmt;
                rpmt = 0;
                return payment;
            }

            // 5th priority extra principal
            if(rpmt >= e_principal) {
                rpmt -= e_principal;
                payment.e_principal = e_principal;
                loanDetails.upb_amount -= e_principal;
                payment.upb_amount = loanDetails.upb_amount;
                _context.Entry(loanDetails).State = EntityState.Modified;
                _context.SaveChanges();

            }
            else {
                payment.e_principal = rpmt;
                loanDetails.upb_amount -= rpmt;
                payment.upb_amount = loanDetails.upb_amount;
                rpmt = 0;
                _context.Entry(loanDetails).State = EntityState.Modified;
                _context.SaveChanges();
                return payment;
            }

            // Last suspense
            payment.suspense = rpmt;
            return payment;
        }
    }

}
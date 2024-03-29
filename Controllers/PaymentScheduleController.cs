
using LSS.Model;
using LSS.Helper;
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

            float monthly_interest_rate = loanDetails.current_rate / 1200;
            double curr_upb = loanDetails.upb_amount;
            double principal = 0;
            double escrow_by_twelve = loanDetails.escrow_amount / 12;

            List<PaymentSchedule> paymentSchedules = new List<PaymentSchedule>();

            double monthly_pmt = (loanDetails.loan_amount * monthly_interest_rate * Math.Pow((double)(1 + monthly_interest_rate), 180)) / ((Math.Pow((double)1 + monthly_interest_rate, 180)) - 1) + escrow_by_twelve;
            if(loanDetails.upb_amount == 0)
                monthly_pmt = 0;

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
        public PaymentSchedule CalculatePayment(Guid id, double pmt)
        {
            var loanDetails = _context.Loans.Find(id);
            loanDetails.last_pmt_amount = pmt;
            CustomWaterfall customWaterfall = new CustomWaterfall(_context);
            WaterfallDateSetter waterfallDateSetter = new WaterfallDateSetter(_context);
            String answer = waterfallDateSetter.DateConverter(loanDetails);
            PaymentSchedule payment = new PaymentSchedule("Jan", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            payment = customWaterfall.CalcCustomPayment(loanDetails);
            payment.month = answer;
            return payment;
        }
    }
}
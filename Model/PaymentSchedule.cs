using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LSS.Model
{
    public class PaymentSchedule
    {
        public String  month { get; set; }
        public double annual_interest_rate { get; set; }
        public double upb_amount { get; set; }
        public double monthly_payment { get; set; }
        public double interest_amount { get; set; }
        public double principal_amount { get; set; }
        public double escrow { get; set; }
    }
}
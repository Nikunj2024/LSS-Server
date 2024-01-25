using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LSS.Model
{
    public class PaymentSchedule
    {
        public PaymentSchedule()
        {
        }

        public PaymentSchedule(string month, double upb_amount, double monthly_payment, double interest_amount, double principal_amount, double escrow,double suspense)
        {
            this.month = month;
            this.upb_amount = upb_amount;
            this.monthly_payment = monthly_payment;
            this.interest_amount = interest_amount;
            this.principal_amount = principal_amount;
            this.escrow = escrow;
            this.suspense = suspense;
        }

        public String  month { get; set; }
        public double upb_amount { get; set; }
        public double monthly_payment { get; set; }
        public double interest_amount { get; set; }
        public double principal_amount { get; set; }
        public double escrow { get; set; }
        public double suspense { get; set; }
    }
}
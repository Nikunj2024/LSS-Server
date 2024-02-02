using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LSS.Model
{
    public class PaymentSchedule
    {
        public PaymentSchedule(){}

        public PaymentSchedule(string month, double upb_amount, double monthly_payment, double interest_amount, double principal_amount, double escrow, double late_fee, double e_principal, double suspense, double other_fee, double extra_escrow, double nsf, double escrow_balance, double interest_balance)
        {
            this.month = month;
            this.upb_amount = upb_amount;
            this.monthly_payment = monthly_payment;
            this.interest_amount = interest_amount;
            this.principal_amount = principal_amount;
            this.escrow = escrow;
            this.late_fee = late_fee;
            this.e_principal = e_principal;
            this.suspense = suspense;
            this.other_fee = other_fee;
            this.extra_escrow = extra_escrow;
            this.nsf = nsf;
            this.escrow_balance = escrow_balance;
            this.interest_balance = interest_balance;
        }

        public String month { get; set; }
        public double upb_amount { get; set; }
        public double monthly_payment { get; set; }
        public double interest_amount { get; set; }
        public double principal_amount { get; set; }
        public double escrow { get; set; }
        public double late_fee { get; set; }
        public double e_principal { get; set; }
        public double suspense { get; set; }
        public double other_fee { get; set; }
        public double extra_escrow { get; set; }
        public double nsf { get; set; }
        public double escrow_balance { get; set; }
        public double interest_balance { get; set; }
    }
}
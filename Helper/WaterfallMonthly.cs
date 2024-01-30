// using LSS.Model;
// using LSS.Persistence;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;

// namespace LSS.Helper
// {
//     public class WaterfallMonthly
//     {
//         private readonly AppDbContext _context;

//         public WaterfallMonthly()
//         {
//         }

//         public WaterfallMonthly(AppDbContext context)
//         {
//             _context = context;
//         }

//         public PaymentSchedule CalcPaymentMonthly(Guid id) {
//             PaymentSchedule payment = new PaymentSchedule("January", 0, 0, 0, 0, 0, 0, 0, 0,0,0,0);
//             var loanDetails = _context.Loans.Find(id);
//             float monthly_interest_rate = loanDetails.current_rate / 1200;
//             double curr_upb = loanDetails.upb_amount;
//             double escrow_by_twelve = loanDetails.escrow_amount / 12;
//             double rpmt = loanDetails.last_pmt_amount;

//             // Supposed values:
//             int late_fee = 50;
//             double e_principal = 100;

//             double monthly_pmt = (loanDetails.loan_amount * monthly_interest_rate * Math.Pow((double)(1 + monthly_interest_rate), 180)) / ((Math.Pow((double)1 + monthly_interest_rate, 180)) - 1) + escrow_by_twelve;
//             double monthly_interest_pmt = loanDetails.upb_amount * monthly_interest_rate;
//             payment.monthly_payment = loanDetails.last_pmt_amount;

//             //Interest priority hierarchy
//             double principal = monthly_pmt - monthly_interest_pmt - escrow_by_twelve;
//             if (rpmt >= monthly_interest_pmt)
//             {
//                 payment.interest_amount = monthly_interest_pmt;
//                 rpmt -= monthly_interest_pmt;
//             }
//             else
//             {
//                 payment.interest_amount = rpmt;
//                 rpmt = 0;
//                 return payment;
//             }

//             // 2nd priority principal 
//             if (rpmt >= principal) {
//                 payment.principal_amount = principal;
                
//                 rpmt -= principal;
//                 loanDetails.upb_amount -= principal;
//                 payment.upb_amount = loanDetails.upb_amount;
//                 _context.Entry(loanDetails).State = EntityState.Modified;
//                 _context.SaveChanges();
//             }
//             else {
//                 payment.principal_amount = rpmt;
//                 loanDetails.upb_amount -= rpmt;
//                 payment.upb_amount = loanDetails.upb_amount;
//                 rpmt = 0;
//                 _context.Entry(loanDetails).State = EntityState.Modified;
//                 _context.SaveChanges();
//                 return payment;

//             }
            
//             // 3rd priority escrow.
//             if(rpmt >= escrow_by_twelve) {
//                 payment.escrow = escrow_by_twelve;
//                 rpmt -= escrow_by_twelve;
//             }
//             else {
//                 payment.escrow = rpmt;
//                 rpmt = 0;
//                 return payment;
//             }
//             // 4th priority late fee
//             if(rpmt >= late_fee) {
//                 rpmt -= late_fee;
//                 payment.late_fee = late_fee;
//             }
//             else {
//                 payment.late_fee = rpmt;
//                 rpmt = 0;
//                 return payment;
//             }

//             // 5th priority extra principal
//             if(rpmt >= e_principal) {
//                 rpmt -= e_principal;
//                 payment.e_principal = e_principal;
//                 loanDetails.upb_amount -= e_principal;
//                 payment.upb_amount = loanDetails.upb_amount;
//                 _context.Entry(loanDetails).State = EntityState.Modified;
//                 _context.SaveChanges();

//             }
//             else {
//                 payment.e_principal = rpmt;
//                 loanDetails.upb_amount -= rpmt;
//                 payment.upb_amount = loanDetails.upb_amount;
//                 rpmt = 0;
//                 _context.Entry(loanDetails).State = EntityState.Modified;
//                 _context.SaveChanges();
//                 return payment;
//             }

//             // Last suspense
//             payment.suspense = rpmt;
//             return payment;
//         }


//         public PaymentSchedule CalcPaymentDSI(Guid id) {
//             PaymentSchedule payment = new PaymentSchedule("January", 0, 0, 0, 0, 0, 0, 0, 0,0,0,0);
//             var loanDetails = _context.Loans.Find(id);
//             float monthly_interest_rate = loanDetails.current_rate / 1200;
//             double curr_upb = loanDetails.upb_amount;
//             double escrow_by_twelve = loanDetails.escrow_amount / 12;
//             double rpmt = loanDetails.last_pmt_amount;

//             // Supposed values:
//             int late_fee = 50;
//             double e_principal = 100;

//             double monthly_pmt = (loanDetails.loan_amount * monthly_interest_rate * Math.Pow((double)(1 + monthly_interest_rate), 180)) / ((Math.Pow((double)1 + monthly_interest_rate, 180)) - 1) + escrow_by_twelve;
//             double monthly_interest_pmt = loanDetails.upb_amount * monthly_interest_rate;
//             payment.monthly_payment = loanDetails.last_pmt_amount;

//             //Interest priority hierarchy
//             double principal = monthly_pmt - monthly_interest_pmt - escrow_by_twelve;
//             if (rpmt >= monthly_interest_pmt)
//             {
//                 payment.interest_amount = monthly_interest_pmt;
//                 rpmt -= monthly_interest_pmt;
//             }
//             else
//             {
//                 payment.interest_amount = rpmt;
//                 rpmt = 0;
//                 return payment;
//             }

//             // 2nd priority escrow.
//             if(rpmt >= escrow_by_twelve) {
//                 payment.escrow = escrow_by_twelve;
//                 rpmt -= escrow_by_twelve;
//             }
//             else {
//                 payment.escrow = rpmt;
//                 rpmt = 0;
//                 return payment;
//             }

//             // 3rd priority Escrow
//             if (rpmt >= principal) {
//                 payment.principal_amount = principal;
                
//                 rpmt -= principal;
//                 loanDetails.upb_amount -= principal;
//                 payment.upb_amount = loanDetails.upb_amount;
//                 _context.Entry(loanDetails).State = EntityState.Modified;
//                 _context.SaveChanges();
//             }
//             else {
//                 payment.principal_amount = rpmt;
//                 loanDetails.upb_amount -= rpmt;
//                 payment.upb_amount = loanDetails.upb_amount;
//                 rpmt = 0;
//                 _context.Entry(loanDetails).State = EntityState.Modified;
//                 _context.SaveChanges();
//                 return payment;

//             }
            
            
//             // 4th priority late fee
//             if(rpmt >= late_fee) {
//                 rpmt -= late_fee;
//                 payment.late_fee = late_fee;
//             }
//             else {
//                 payment.late_fee = rpmt;
//                 rpmt = 0;
//                 return payment;
//             }

//             // 5th priority extra principal
//             if(rpmt >= e_principal) {
//                 rpmt -= e_principal;
//                 payment.e_principal = e_principal;
//                 loanDetails.upb_amount -= e_principal;
//                 payment.upb_amount = loanDetails.upb_amount;
//                 _context.Entry(loanDetails).State = EntityState.Modified;
//                 _context.SaveChanges();

//             }
//             else {
//                 payment.e_principal = rpmt;
//                 loanDetails.upb_amount -= rpmt;
//                 payment.upb_amount = loanDetails.upb_amount;
//                 rpmt = 0;
//                 _context.Entry(loanDetails).State = EntityState.Modified;
//                 _context.SaveChanges();
//                 return payment;
//             }

//             // Last suspense
//             payment.suspense = rpmt;
//             return payment;
//         }

//         public PaymentSchedule CalcPaymentFRM(Guid id) {
//             PaymentSchedule payment = new PaymentSchedule("January", 0, 0, 0, 0, 0, 0, 0, 0,0,0,0);
//             var loanDetails = _context.Loans.Find(id);
//             float monthly_interest_rate = loanDetails.current_rate / 1200;
//             double curr_upb = loanDetails.upb_amount;
//             double escrow_by_twelve = loanDetails.escrow_amount / 12;
//             double rpmt = loanDetails.last_pmt_amount;

//             // Supposed values:
//             int late_fee = 50;
//             double e_principal = 100;

//             double monthly_pmt = (loanDetails.loan_amount * monthly_interest_rate * Math.Pow((double)(1 + monthly_interest_rate), 180)) / ((Math.Pow((double)1 + monthly_interest_rate, 180)) - 1) + escrow_by_twelve;
//             double monthly_interest_pmt = loanDetails.upb_amount * monthly_interest_rate;
//             payment.monthly_payment = loanDetails.last_pmt_amount;

//             //Interest priority hierarchy
//             double principal = monthly_pmt - monthly_interest_pmt - escrow_by_twelve;
//             if (rpmt >= monthly_interest_pmt)
//             {
//                 payment.interest_amount = monthly_interest_pmt;
//                 rpmt -= monthly_interest_pmt;
//             }
//             else
//             {
//                 payment.interest_amount = rpmt;
//                 rpmt = 0;
//                 return payment;
//             }

            

//             // 2nd priority principal
//             if (rpmt >= principal) {
//                 payment.principal_amount = principal;
                
//                 rpmt -= principal;
//                 loanDetails.upb_amount -= principal;
//                 payment.upb_amount = loanDetails.upb_amount;
//                 _context.Entry(loanDetails).State = EntityState.Modified;
//                 _context.SaveChanges();
//             }
//             else {
//                 payment.principal_amount = rpmt;
//                 loanDetails.upb_amount -= rpmt;
//                 payment.upb_amount = loanDetails.upb_amount;
//                 rpmt = 0;
//                 _context.Entry(loanDetails).State = EntityState.Modified;
//                 _context.SaveChanges();
//                 return payment;

//             }
            
//             // 3rd priority escrow.
//             if(rpmt >= escrow_by_twelve) {
//                 payment.escrow = escrow_by_twelve;
//                 rpmt -= escrow_by_twelve;
//             }
//             else {
//                 payment.escrow = rpmt;
//                 rpmt = 0;
//                 return payment;
//             }

//             // 4th priority extra principal
//             if(rpmt >= e_principal) {
//                 rpmt -= e_principal;
//                 payment.e_principal = e_principal;
//                 loanDetails.upb_amount -= e_principal;
//                 payment.upb_amount = loanDetails.upb_amount;
//                 _context.Entry(loanDetails).State = EntityState.Modified;
//                 _context.SaveChanges();

//             }
//             else {
//                 payment.e_principal = rpmt;
//                 loanDetails.upb_amount -= rpmt;
//                 payment.upb_amount = loanDetails.upb_amount;
//                 rpmt = 0;
//                 _context.Entry(loanDetails).State = EntityState.Modified;
//                 _context.SaveChanges();
//                 return payment;
//             }

//             // 5th priority late fee
//             if(rpmt >= late_fee) {
//                 rpmt -= late_fee;
//                 payment.late_fee = late_fee;
//             }
//             else {
//                 payment.late_fee = rpmt;
//                 rpmt = 0;
//                 return payment;
//             }

//             // Last suspense
//             payment.suspense = rpmt;
//             return payment;
//         }
//     }
// }
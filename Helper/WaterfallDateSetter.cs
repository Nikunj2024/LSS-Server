using LSS.Model;
using LSS.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LSS.Helper
{
    public class WaterfallDateSetter
    {
        private readonly AppDbContext _context;
        public WaterfallDateSetter() { }

        public WaterfallDateSetter(AppDbContext context) {
            _context = context;
        }
        public String DateConverter(LoanDetails loanDetails)
        {
            DateOnly Curr_date = loanDetails.pmt_due_date;
            int month = Curr_date.Month;
            month -= 1;
            
            string[] AnsArray =
            [
                "January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December"
            ];
            Curr_date = Curr_date.AddMonths(1);
            loanDetails.pmt_due_date = Curr_date;
            _context.Entry(loanDetails).State = EntityState.Modified;
            _context.SaveChanges();
            return AnsArray[month];
        }
    }
}
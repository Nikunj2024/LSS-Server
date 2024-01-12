using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace LSS.Model
{
    [Index(nameof(loan_number), IsUnique = true)]
    public class LoanDetails
    {
        [Key]
        public Guid Id { get; set; }
        public DateOnly boarding_date { get; set; }
        public float current_rate { get; set; }
        public long loan_number { get; set; }
        public String name { get; set; }
        public DateOnly note_date { get; set; }
        public float note_rate { get; set; }
        public float pmt_amount { get; set; }
        public DateOnly pmt_due_date { get; set; }
        public String ppr { get; set; } = "Current_Loan_Waterfall";
        public float principal_intrest { get; set; }
        public float tax_insurance { get; set; }
        public float upb_amount { get; set; }

    }
}
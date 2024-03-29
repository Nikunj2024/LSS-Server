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
        public double loan_amount { get; set; } = 500000.00;
        public long loan_number { get; set; }
        public string name { get; set; }
        public DateOnly note_date { get; set; }

        public float note_rate { get; set; }
        public DateOnly pmt_due_date { get; set; }
        public string waterfall_name { get; set; } = "Jan_2024_BOA"; // Foreign key
        public double upb_amount { get; set; }
        public double escrow_amount { get; set; } = 0.0;
        public double last_pmt_amount { get; set; } = 0.0;
        public double balance { get; set; } = 0.0;
    }
}
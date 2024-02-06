using System.ComponentModel.DataAnnotations;

namespace LSS.Model
{
    public class PaymentHistory
    {
        [Key]
        public Guid Id { get; set; }
        public string mop { get; set; }
        public string c_name { get; set; }
        public string bank_name { get; set; }
        public long account_number { get; set; }
        public long route_number { get; set; }
        public double pmt_amt { get; set; }
        public DateTime date_time { get; set; }
        public Guid loan_id { get; set; }
    }
}
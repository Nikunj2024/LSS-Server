
using System.ComponentModel.DataAnnotations;

namespace LSS.Model
{
    public class Escrow
    {
        public Escrow()
        {
        }

        public Escrow(int id, string item_name, string freq, string escrow_type, int amt, string beneficiary_name, string bank_name, long account_number, long route_number, string mop)
        {
            this.id = id;
            this.item_name = item_name;
            this.freq = freq;
            this.escrow_type = escrow_type;
            this.amt = amt;
            this.beneficiary_name = beneficiary_name;
            this.bank_name = bank_name;
            this.account_number = account_number;
            this.route_number = route_number;
            this.mop = mop;
        }

        [Key]
        public int id { get; set; }
        public string item_name { get; set; }
        public string freq { get; set; }
        public string escrow_type { get; set; }
        public int amt { get; set; }
        public string beneficiary_name { get; set; }
        public string bank_name { get; set; }
        public long account_number { get; set; }
        public long route_number { get; set; }
        public string mop { get; set; } = "Wire Transfer";
    }
}
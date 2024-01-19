

namespace LSS.Model
{
    public class EscrowDetails
    {

        public string month { get; set; }
        public double escrow { get; set; }
        public double county_tax { get; set; }
        public double special_district_tax { get; set; }
        public double mortgage_insurance { get; set; }
        public double flood_insurance { get; set; }
        public double hazard_insurance { get; set; }
        public double balance { get; set; }
        EscrowDetails()
        {

        }

        public EscrowDetails(string month, double escrow, double county_tax, double special_district_tax, double mortgage_insurance, double flood_insurance, double hazard_insurance, double balance)
        {
            this.month = month;
            this.escrow = escrow;
            this.county_tax = county_tax;
            this.special_district_tax = special_district_tax;
            this.mortgage_insurance = mortgage_insurance;
            this.flood_insurance = flood_insurance;
            this.hazard_insurance = hazard_insurance;
            this.balance = balance;
        }
    }
}
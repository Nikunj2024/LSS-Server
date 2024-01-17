
using System.ComponentModel.DataAnnotations;

namespace LSS.Model
{
    public class Escrow
    {
        [Key]
        public int  id { get; set; }
        public string item_name { get; set; }
        public string  freq { get; set; }
        public string escrow_type { get; set; }
        public int amt { get; set; }
    }
}
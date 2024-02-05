using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace LSS.Model
{
    [Owned]
    public class Waterfall
    {
        [Key]
        public int id { get; set; }
        public string w_name { get; set; }
        public string[] desc { get; set; }

    }

}

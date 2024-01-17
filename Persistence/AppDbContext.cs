using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSS.Model;
using Microsoft.EntityFrameworkCore;

namespace LSS.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<LoanDetails> Loans { get; set; }
        public DbSet<Escrow> Escrows { get; set; }
        public DbSet<Waterfall> Waterfalls { get; set; }





    }

}
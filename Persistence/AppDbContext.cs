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
        public DbSet<AppUser> Users { get; set; }

        public async Task<Waterfall> GetWaterfallByNameAsync(string name)
        {
            return await Waterfalls
                .FirstOrDefaultAsync(waterfall => waterfall.w_name == name);
        }

        public AppUser GetByEmail(string email)
        {
            return Users
                .FirstOrDefault(user => user.email == email);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>(entity => {
                entity.HasIndex(e => e.email).IsUnique();
            });

            modelBuilder.Entity<Waterfall>()
                .HasIndex(w => w.w_name)
                .IsUnique();
        }


    }

}
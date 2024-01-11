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

    //     protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<LoanDetails>()
    //         .HasIndex(e => e.loan_number)
    //         .IsUnique();
    // }
}

}
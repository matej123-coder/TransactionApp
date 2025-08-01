using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entites;
namespace Persistance
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ClientInfo> Clients { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<FeeLog> FeeLogs { get; set; }
        public DbSet<Rule> Rules { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Client)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
        .       HasColumnType("decimal(18,4)");
            modelBuilder.Entity<FeeLog>()
            .Property(t => t.FeeAmount)
            .HasColumnType("decimal(18,4)");
            modelBuilder.Entity<FeeLog>()
                .HasOne(f => f.Transaction)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}

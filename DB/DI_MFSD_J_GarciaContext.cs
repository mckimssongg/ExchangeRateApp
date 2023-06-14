using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace DB
{
    public class DI_MFSD_J_GarciaContext : DbContext
    {
        public DI_MFSD_J_GarciaContext(DbContextOptions<DI_MFSD_J_GarciaContext> options)
            :base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Rol> Rols { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Rol>().ToTable("Rol");
            modelBuilder.Entity<Transaction>().ToTable("Transaction");
            modelBuilder.Entity<Transaction>()
                .Property(e => e.ExchangeRate)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Transaction>()
                .Property(e => e.Amount)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<ExchangeRate>().ToTable("ExchangeRate");
            modelBuilder.Entity<ExchangeRate>()
                .Property(e => e.Rate)
                .HasColumnType("decimal(18,2)");

            // Add a new role
            modelBuilder.Entity<Rol>().HasData(
                new Rol
                {
                    RolID = 1,
                    Name = "Admin"
                }
            );
            // Add a new role
            modelBuilder.Entity<Rol>().HasData(
                new Rol
                {
                    RolID = 2,
                    Name = "General"
                }
            );
        }
    }
}
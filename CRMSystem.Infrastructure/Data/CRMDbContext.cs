using CRMSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Infrastructure.Data
{
    public class CRMDbContext:DbContext
    {
        public CRMDbContext(DbContextOptions<CRMDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Customer> Customers => Set<Customer>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Role = "Admin",
                    CreatedAt = "2023-06-15",
                    UpdatedAt = "2023-09-13"
                });

            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Region = "North America", RegistrationDate = "2023-06-15" },
                new Customer { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", Region = "Europe", RegistrationDate = "2023-05-10"},
                new Customer { Id = 3, FirstName = "Carlos", LastName = "Gomez", Email = "carlos.gomez@example.com", Region = "South America", RegistrationDate = "2023-07-22" }
            );
        }
    }
}

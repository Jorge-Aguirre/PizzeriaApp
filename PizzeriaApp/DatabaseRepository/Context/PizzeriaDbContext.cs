using BusinessDomain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DatabaseRepository.Context
{
    public class PizzeriaDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<Product> Products { get; set; }
        public DbSet<Size> Sizes { get; set; }

        public PizzeriaDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("PizzeriaDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}

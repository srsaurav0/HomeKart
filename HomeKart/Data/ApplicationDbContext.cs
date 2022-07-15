using HomeKart.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeKart.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<RegisterVM> Registers { get; set; }

        public DbSet<AdminVM> Admins { get; set; }

        public DbSet<OwnerVM> Properties { get; set; }
    }
}

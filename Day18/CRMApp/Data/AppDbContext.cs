using Microsoft.EntityFrameworkCore;
using CRMApp.Models;

namespace CRMApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=crm.db");
        }
    }
}
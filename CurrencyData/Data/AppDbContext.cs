using Microsoft.EntityFrameworkCore;
using CurrencyData.Models;

namespace CurrencyData.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Currency> Currencies { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}

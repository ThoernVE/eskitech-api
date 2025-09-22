using Microsoft.EntityFrameworkCore;
using EskitechApi.Models;

namespace EskitechApi.Data
{
    public class EskitechDbContext : DbContext
    {
        public EskitechDbContext(DbContextOptions<EskitechDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
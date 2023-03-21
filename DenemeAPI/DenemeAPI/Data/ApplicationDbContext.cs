using DenemeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DenemeAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base (options) { }

        public DbSet<Product> Product { get; set; }
    }
}

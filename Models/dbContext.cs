using Microsoft.EntityFrameworkCore;
 
namespace MVCforFunzies.Models
{
    public class dbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<ProductList> ProductList { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ListProduct> ListProduct { get; set; }
        // base() calls the parent class' constructor passing the "options" parameter along
        public dbContext(DbContextOptions<dbContext> options) : base(options) { }
    }
}

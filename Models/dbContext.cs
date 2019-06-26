using Microsoft.EntityFrameworkCore;
 
namespace MVCforFunzies.Models
{
    public class dbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ProductList> ProductLists { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ListProduct> ListProducts { get; set; }
        // base() calls the parent class' constructor passing the "options" parameter along
        public dbContext(DbContextOptions<dbContext> options) : base(options) { }
    }
}

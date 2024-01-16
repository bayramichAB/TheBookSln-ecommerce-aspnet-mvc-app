using Microsoft.EntityFrameworkCore;

namespace TheBookshelf.Models
{
    public class StoreDbContext:DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }
        
        public DbSet<Book> Books => Set <Book>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Order> Orders => Set<Order>();
       
    }
}

using FirstAPI.Entity;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using Color = FirstAPI.Entity.Color;


namespace FirstAPI.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Color> Colors { get; set; }
      
    }
}

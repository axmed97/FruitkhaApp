using Microsoft.EntityFrameworkCore;
using WebUI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebUI.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext()
        {
            
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ArticleTag> ArticleTags { get; set; }

    }
}

using Microsoft.EntityFrameworkCore;

namespace ShopBridgeAPI.Models
{
    public class ShopBridgeContext: DbContext
    {
        public ShopBridgeContext(DbContextOptions<ShopBridgeContext> options): base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SetupProductTable(modelBuilder);
            SetupCategoryTable(modelBuilder);
            SeedData(modelBuilder);
        }

        private static void SetupProductTable(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Product>();
            entity.ToTable(nameof(Product));

            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(50);
            entity.Property(p => p.Description).HasMaxLength(255);
            entity.HasOne(p => p.Category)
                .WithMany(entity => entity.Products)
                .HasForeignKey(p => p.CategoryId);
        }

        private static void SetupCategoryTable(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Category>();
            entity.ToTable(nameof(Category));

            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(50);
            entity.Property(p => p.DiscountPercentage);

            modelBuilder.Entity<Category>().HasIndex(u => u.Name).IsUnique();
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(Models.SeedData.GetCategories());
            modelBuilder.Entity<Product>().HasData(Models.SeedData.GetProducts());
        }
    }
}

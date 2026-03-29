using ArtGallery.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Artwork> Artworks => Set<Artwork>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<ArtworkCategory> ArtworkCategories => Set<ArtworkCategory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");
        base.OnModelCreating(modelBuilder);

        // ---------- ARTWORK ----------
        modelBuilder.Entity<Artwork>(entity =>
        {
            entity.ToTable("artworks");

            entity.HasKey(a => a.Id);

            entity.Property(a => a.Id).HasColumnName("id");
            entity.Property(a => a.Title).HasColumnName("title");
            entity.Property(a => a.Description).HasColumnName("description");
            entity.Property(a => a.Price).HasColumnName("price");
            entity.Property(a => a.ImageUrl).HasColumnName("image_url");
            entity.Property(a => a.Type).HasColumnName("type").HasConversion<string>().IsRequired();
            entity.Property(a => a.IsAvailable).HasColumnName("is_available");
            entity.Property(a => a.CreatedAt).HasColumnName("created_at");
        });

        // ---------- CATEGORY ----------
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("categories");

            entity.HasKey(c => c.Id);

            entity.Property(c => c.Id).HasColumnName("id");
            entity.Property(c => c.Name).HasColumnName("name");
            entity.Property(c => c.Slug).HasColumnName("slug");
            entity.Property(c => c.CreatedAt).HasColumnName("created_at");
        });

        // ---------- ARTWORK_CATEGORY ----------
        modelBuilder.Entity<ArtworkCategory>(entity =>
        {
            entity.ToTable("artwork_categories");

            entity.HasKey(x => new { x.ArtworkId, x.CategoryId });

            entity.Property(x => x.ArtworkId).HasColumnName("artwork_id");
            entity.Property(x => x.CategoryId).HasColumnName("category_id");

            entity.HasOne(x => x.Artwork)
                  .WithMany(a => a.ArtworkCategories)
                  .HasForeignKey(x => x.ArtworkId);

            entity.HasOne(x => x.Category)
                  .WithMany(c => c.ArtworkCategories)
                  .HasForeignKey(x => x.CategoryId);
        });
    }
}

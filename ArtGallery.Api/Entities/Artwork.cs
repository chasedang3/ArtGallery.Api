namespace ArtGallery.Api.Entities;

public class Artwork
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public string ImageUrl { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;

    public bool IsAvailable { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<ArtworkCategory> ArtworkCategories { get; set; }
        = new List<ArtworkCategory>();
}

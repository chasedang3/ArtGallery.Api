namespace ArtGallery.Api.Entities
{
    public class ArtworkCategory
    {
        public Guid ArtworkId { get; set; }
        public Artwork Artwork { get; set; } = null!;

        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}

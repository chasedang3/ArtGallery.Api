namespace ArtGallery.Api.Entities
{
    public class Category
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<ArtworkCategory> ArtworkCategories { get; set; }
            = new List<ArtworkCategory>();
    }
}

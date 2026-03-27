namespace ArtGallery.Api.DTOs
{
    public class ArtworkDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public List<CategoryDto> Categories { get; set; } = [];
    }
}

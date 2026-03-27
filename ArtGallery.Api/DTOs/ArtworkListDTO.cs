namespace ArtGallery.Api.DTOs
{
    public class ArtworkListDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public List<CategoryDto> Categories { get; set; }
    }
}

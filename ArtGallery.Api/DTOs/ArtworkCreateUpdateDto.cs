using ArtGallery.Api.Entities;

namespace ArtGallery.Api.DTOs;

public class ArtworkCreateUpdateDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;

    // canvas | oil
    public ArtworkType Type { get; set; } = ArtworkType.Canvas;

    public bool IsAvailable { get; set; }

    // category được chọn trong admin
    public List<Guid> CategoryIds { get; set; } = new();
}

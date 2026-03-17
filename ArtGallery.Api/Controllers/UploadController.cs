using ArtGallery.Api.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ArtGallery.Api.Controllers
{
    [ApiController]
    [Route("api/upload")]
    public class UploadController : ControllerBase
    {
        private readonly CloudinaryService _cloudinary;

        public UploadController(CloudinaryService cloudinary)
        {
            _cloudinary = cloudinary;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadDto dto)
        {
            if (dto.File == null || dto.File.Length == 0)
                return BadRequest("No file uploaded");

            var imageUrl = await _cloudinary.UploadImageAsync(dto.File);
            return Ok(new { imageUrl });
        }
    }
}

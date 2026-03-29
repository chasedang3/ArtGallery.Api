using ArtGallery.Api.Data;
using ArtGallery.Api.DTOs;
using ArtGallery.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtworksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ArtworksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/artworks
        // Supports paging, search, category filter, type filter, all=true
        [HttpGet]
        public async Task<IActionResult> GetArtworks(
            int page = 1,
            int pageSize = 12,
            string? search = null,
            Guid? categoryId = null,
            ArtworkType? type = null,
            bool all = false
        )
        {
            page = page < 1 ? 1 : page;
            pageSize = pageSize <= 0 ? 12 : pageSize;

            IQueryable<Artwork> query = _context.Artworks;

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(a =>
                    EF.Functions.Like(a.Title ?? "", $"%{search}%") ||
                    EF.Functions.Like(a.Description ?? "", $"%{search}%")
                );
            }

            if (categoryId.HasValue)
            {
                query = query.Where(a =>
                    a.ArtworkCategories.Any(ac => ac.CategoryId == categoryId.Value)
                );
            }

            if (type.HasValue)
            {
                query = query.Where(a => a.Type == type.Value);
            }

            if (all)
            {
                var list = await query
                    .OrderByDescending(a => a.CreatedAt)
                    .Select(a => new ArtworkListDto
                    {
                        Id = a.Id,
                        Title = a.Title,
                        Description = a.Description,
                        CreatedAt = a.CreatedAt,
                        ImageUrl = a.ImageUrl,
                        Type = a.Type,
                        Price = a.Price,
                        Categories = a.ArtworkCategories
                            .Select(ac => new CategoryDto
                            {
                                Id = ac.Category.Id,
                                Name = ac.Category.Name
                            })
                            .ToList()
                    })
                    .ToListAsync();

                return Ok(list);
            }

            // ===== PAGING =====
            var totalItems = await query.CountAsync();

            var artworks = await query
                .OrderByDescending(a => a.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(a => new ArtworkListDto
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    CreatedAt = a.CreatedAt,
                    ImageUrl = a.ImageUrl,
                    Type = a.Type,
                    Price = a.Price,
                    Categories = a.ArtworkCategories
                        .Select(ac => new CategoryDto
                        {
                            Id = ac.Category.Id,
                            Name = ac.Category.Name
                        })
                        .ToList()
                })
                .ToListAsync();

            return Ok(new
            {
                page,
                pageSize,
                totalItems,
                totalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                items = artworks
            });
        }

        // GET: api/artworks/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetArtwork(Guid id)
        {
            var artwork = await _context.Artworks
                            .Include(a => a.ArtworkCategories)
                            .ThenInclude(ac => ac.Category)
                            .FirstOrDefaultAsync(a => a.Id == id);

            return Ok(new ArtworkDto
            {
                Id = artwork.Id,
                Title = artwork.Title,
                Description = artwork.Description,
                Price = artwork.Price,
                ImageUrl = artwork.ImageUrl,
                Categories = artwork.ArtworkCategories
                    .Select(ac => new CategoryDto
                    {
                        Id = ac.Category.Id,
                        Name = ac.Category.Name
                    }).ToList()
            });
        }

        // POST: api/artworks
        [HttpPost]
        public async Task<IActionResult> CreateArtwork([FromBody] ArtworkCreateUpdateDto dto)
        {
            var artwork = new Artwork
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl,
                Type = dto.Type,
                IsAvailable = dto.IsAvailable,
                CreatedAt = DateTime.UtcNow
            };

            var validCategoryIds = await _context.Categories
                .Where(c => dto.CategoryIds.Contains(c.Id))
                .Select(c => c.Id)
                .ToListAsync();

            if (validCategoryIds.Count != dto.CategoryIds.Count)
            {
                return BadRequest("One or more categories are invalid");
            }

            // gán category
            foreach (var categoryId in dto.CategoryIds)
            {
                artwork.ArtworkCategories.Add(new ArtworkCategory
                {
                    ArtworkId = artwork.Id,
                    CategoryId = categoryId
                });
            }

            _context.Artworks.Add(artwork);
            await _context.SaveChangesAsync();

            return Ok(artwork);
        }

        // PUT: api/artworks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArtwork(Guid id, ArtworkCreateUpdateDto dto)
        {
            var artwork = await _context.Artworks
                .Include(a => a.ArtworkCategories)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (artwork == null)
                return NotFound();

            artwork.Title = dto.Title;
            artwork.Description = dto.Description;
            artwork.Price = dto.Price;
            artwork.ImageUrl = dto.ImageUrl;
            artwork.Type = dto.Type;
            artwork.IsAvailable = dto.IsAvailable;

            // cập nhật category
            artwork.ArtworkCategories.Clear();

            foreach (var categoryId in dto.CategoryIds)
            {
                artwork.ArtworkCategories.Add(new ArtworkCategory
                {
                    ArtworkId = artwork.Id,
                    CategoryId = categoryId
                });
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/artworks/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteArtwork(Guid id)
        {
            var artwork = await _context.Artworks.FindAsync(id);

            if (artwork == null)
                return NotFound();

            _context.Artworks.Remove(artwork);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

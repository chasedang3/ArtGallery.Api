using ArtGallery.Api.Data;
using ArtGallery.Api.DTOs;
using ArtGallery.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ArtGallery.Api.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoryController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/categories
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _context.Categories
            .OrderBy(c => c.Name)
            .Select(c => new
            {
                c.Id,
                c.Name,
                c.Slug
            })
            .ToListAsync();

        return Ok(categories);
    }

    // POST: api/categories
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CategoryCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            return BadRequest("Category name is required");

        var slug = GenerateSlug(dto.Name);

        var exists = await _context.Categories.AnyAsync(c => c.Slug == slug);
        if (exists)
            return BadRequest("Category already exists");

        var category = new Category
        {
            Name = dto.Name.Trim(),
            Slug = slug
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return Ok(category);
    }

    // DELETE: api/categories/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
            return NotFound();

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // helper: slug generator
    private static string GenerateSlug(string input)
    {
        var slug = input.ToLower().Trim();
        slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");
        slug = Regex.Replace(slug, @"\s+", "-");
        return slug;
    }
}

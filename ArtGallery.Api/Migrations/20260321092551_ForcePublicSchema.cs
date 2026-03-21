using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtGallery.Api.Migrations
{
    /// <inheritdoc />
    public partial class ForcePublicSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.RenameTable(
                name: "categories",
                newName: "categories",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "artworks",
                newName: "artworks",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "artwork_categories",
                newName: "artwork_categories",
                newSchema: "public");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "categories",
                schema: "public",
                newName: "categories");

            migrationBuilder.RenameTable(
                name: "artworks",
                schema: "public",
                newName: "artworks");

            migrationBuilder.RenameTable(
                name: "artwork_categories",
                schema: "public",
                newName: "artwork_categories");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtGallery.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitSupabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Artworks",
                table: "Artworks");

            migrationBuilder.RenameTable(
                name: "Artworks",
                newName: "artworks");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "artworks",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "artworks",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "artworks",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "artworks",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "artworks",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "artworks",
                newName: "image_url");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "artworks",
                newName: "created_at");

            migrationBuilder.AddColumn<bool>(
                name: "is_available",
                table: "artworks",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_artworks",
                table: "artworks",
                column: "id");

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    slug = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "artwork_categories",
                columns: table => new
                {
                    artwork_id = table.Column<Guid>(type: "uuid", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_artwork_categories", x => new { x.artwork_id, x.category_id });
                    table.ForeignKey(
                        name: "FK_artwork_categories_artworks_artwork_id",
                        column: x => x.artwork_id,
                        principalTable: "artworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_artwork_categories_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_artwork_categories_category_id",
                table: "artwork_categories",
                column: "category_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "artwork_categories");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_artworks",
                table: "artworks");

            migrationBuilder.DropColumn(
                name: "is_available",
                table: "artworks");

            migrationBuilder.RenameTable(
                name: "artworks",
                newName: "Artworks");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "Artworks",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Artworks",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Artworks",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Artworks",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Artworks",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "image_url",
                table: "Artworks",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Artworks",
                newName: "CreatedAt");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Artworks",
                table: "Artworks",
                column: "Id");
        }
    }
}

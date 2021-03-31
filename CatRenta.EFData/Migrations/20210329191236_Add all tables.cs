using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CatRenta.EFData.Migrations
{
    public partial class Addalltables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblCats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Birthday = table.Column<DateTime>(nullable: false),
                    Details = table.Column<string>(maxLength: 4000, nullable: false),
                    Gender = table.Column<bool>(nullable: false),
                    Image = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblCatPrices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Price = table.Column<decimal>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    CatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCatPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblCatPrices_tblCats_CatId",
                        column: x => x.CatId,
                        principalTable: "tblCats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblCatPrices_CatId",
                table: "tblCatPrices",
                column: "CatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblCatPrices");

            migrationBuilder.DropTable(
                name: "tblCats");
        }
    }
}

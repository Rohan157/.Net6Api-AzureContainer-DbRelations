using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookProject.Migrations
{
    public partial class createInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookWriters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookWriters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookCovers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookWriterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCovers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookCovers_BookWriters_BookWriterId",
                        column: x => x.BookWriterId,
                        principalTable: "BookWriters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ISBNNUMBER = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookCoverId = table.Column<int>(type: "int", nullable: false),
                    BookWriterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_BookCovers_BookCoverId",
                        column: x => x.BookCoverId,
                        principalTable: "BookCovers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_BookWriters_BookWriterId",
                        column: x => x.BookWriterId,
                        principalTable: "BookWriters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookCovers_BookWriterId",
                table: "BookCovers",
                column: "BookWriterId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookCoverId",
                table: "Books",
                column: "BookCoverId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookWriterId",
                table: "Books",
                column: "BookWriterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "BookCovers");

            migrationBuilder.DropTable(
                name: "BookWriters");
        }
    }
}

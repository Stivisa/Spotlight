using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Spotlight.Migrations.ListingDb
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllListings",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostHeadline = table.Column<string>(nullable: true),
                    PostContent = table.Column<string>(nullable: true),
                    TimeOfPosting = table.Column<DateTime>(nullable: true),
                    DonationQuote = table.Column<int>(nullable: false),
                    CurrentDonationValue = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllListings", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllListings");
        }
    }
}

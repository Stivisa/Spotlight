using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Spotlight.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllListings",
                columns: table => new
                {
                    DonationQuote = table.Column<double>(nullable: false),
                    id = table.Column<int>(nullable: false),
                    PostHeadline = table.Column<string>(nullable: true),
                    PostContent = table.Column<string>(nullable: true),
                    TimeOfPosting = table.Column<DateTime>(nullable: true),
                    CurrentDonationValue = table.Column<double>(nullable: false),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllListings", x => x.DonationQuote);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllListings");
        }
    }
}

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
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostHeadline = table.Column<string>(nullable: true),
                    PostContent = table.Column<string>(nullable: true),
                    TimeOfPosting = table.Column<DateTime>(nullable: true),
                    DonationQuote = table.Column<double>(nullable: false),
                    CurrentDonationValue = table.Column<double>(nullable: false),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllListings", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "DonationRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    value = table.Column<int>(nullable: false),
                    When = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationRecords", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllListings");

            migrationBuilder.DropTable(
                name: "DonationRecords");
        }
    }
}

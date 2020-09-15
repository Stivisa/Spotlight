using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Spotlight.Migrations
{
    public partial class NewsDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllNewsPosts",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostHeadline = table.Column<string>(nullable: true),
                    PostContent = table.Column<string>(nullable: true),
                    TimeOfPosting = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllNewsPosts", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllNewsPosts");
        }
    }
}

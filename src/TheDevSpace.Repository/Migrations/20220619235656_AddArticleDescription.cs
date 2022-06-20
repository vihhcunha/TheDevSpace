using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheDevSpace.Repository.Migrations
{
    public partial class AddArticleDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WriterId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Articles");

            migrationBuilder.AddColumn<Guid>(
                name: "WriterId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendSpotify.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class CangeTokenTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Tokens");

            migrationBuilder.AddColumn<string>(
                name: "Scope",
                table: "Tokens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TokenType",
                table: "Tokens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Scope",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "TokenType",
                table: "Tokens");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Tokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

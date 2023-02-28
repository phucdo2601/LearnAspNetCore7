using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnNet7AuthenAndAuthorB01.Migrations
{
    /// <inheritdoc />
    public partial class AddTblRefrehToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsssuedAt",
                table: "RefreshTokens",
                newName: "IssuedAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiredAt",
                table: "RefreshTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiredAt",
                table: "RefreshTokens");

            migrationBuilder.RenameColumn(
                name: "IssuedAt",
                table: "RefreshTokens",
                newName: "IsssuedAt");
        }
    }
}

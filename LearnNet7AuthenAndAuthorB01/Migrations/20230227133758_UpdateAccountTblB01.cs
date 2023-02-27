using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnNet7AuthenAndAuthorB01.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAccountTblB01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Accounts");
        }
    }
}

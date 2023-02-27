using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnNet7AuthenAndAuthorB01.Migrations
{
    /// <inheritdoc />
    public partial class UpdateItemTableb01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imgUrl",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imgUrl",
                table: "Items");
        }
    }
}

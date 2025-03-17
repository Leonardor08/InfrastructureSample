using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sample.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class CHANGENUMBERTOPHONE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Users",
                newName: "Phone");

            migrationBuilder.AddColumn<int>(
                name: "Status_Id",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status_Id",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Users",
                newName: "Number");
        }
    }
}

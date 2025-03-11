using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberShopee.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCustomerPropertiesInDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserRole",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryAddress",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UserRole",
                table: "Customers");
        }
    }
}

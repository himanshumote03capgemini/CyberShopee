using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberShopee.Migrations
{
    /// <inheritdoc />
    public partial class AdminAddedInDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserRole",
                table: "Customers");

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserRole = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.AdminId);
                });

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "AdminId", "AdminName", "Email", "Password", "UserRole" },
                values: new object[] { 1, "Himanshu", "admin@gmail.com", "AQAAAAIAAYagAAAAEKv7+X4ITmN5Xl1ROU+HZYvQNf5UIv22LaD0jktCZYS9G3tz1Dthcm87HMik2K/ByA==", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.AddColumn<string>(
                name: "UserRole",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

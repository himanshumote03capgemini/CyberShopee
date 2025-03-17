using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberShopee.Migrations
{
    /// <inheritdoc />
    public partial class Admin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admin",
                keyColumn: "AdminId",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEOXMs6rAGLqzLB47a57VCtnVwGskOXTPok0YsRykhtVumRSukmK/zirdtj8xbTnZMg==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admin",
                keyColumn: "AdminId",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEKv7+X4ITmN5Xl1ROU+HZYvQNf5UIv22LaD0jktCZYS9G3tz1Dthcm87HMik2K/ByA==");
        }
    }
}

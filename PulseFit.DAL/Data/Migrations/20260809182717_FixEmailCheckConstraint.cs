using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PulseFit.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixEmailCheckConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CheckValidEmailConstraint",
                table: "User");

            migrationBuilder.AddCheckConstraint(
                name: "CheckValidEmailConstraint",
                table: "User",
                sql: "Email Like '_@_%.%'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CheckValidEmailConstraint",
                table: "User");

            migrationBuilder.AddCheckConstraint(
                name: "CheckValidEmailConstraint",
                table: "User",
                sql: "Email Like '_0_%.%'");
        }
    }
}

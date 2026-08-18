using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PulseFit.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixEmailCheckConstraintPrefixLength : Migration
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
                sql: "Email Like '%_@__%.__%'");
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
                sql: "Email Like '_@_%.%'");
        }
    }
}

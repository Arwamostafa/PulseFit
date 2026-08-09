using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PulseFit.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class addJoinDateTomemeber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JoinDate",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JoinDate",
                table: "User");
        }
    }
}

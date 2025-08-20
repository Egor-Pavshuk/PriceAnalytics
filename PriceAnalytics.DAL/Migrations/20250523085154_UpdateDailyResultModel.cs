using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PriceAnalytics.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDailyResultModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationType",
                table: "SaleApplications",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "DailyResults",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationType",
                table: "SaleApplications");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "DailyResults");
        }
    }
}

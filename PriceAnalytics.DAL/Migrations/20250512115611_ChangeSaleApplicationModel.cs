using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PriceAnalytics.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSaleApplicationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hour",
                table: "SaleApplications");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "SaleApplications");

            migrationBuilder.DropColumn(
                name: "Step",
                table: "SaleApplications");

            migrationBuilder.AlterColumn<decimal>(
                name: "OfferedVolume",
                table: "DailyResults",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(uint),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<decimal>(
                name: "AcceptedVolume",
                table: "DailyResults",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(uint),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "DailyResults",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "RowData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SaleApplicationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RowData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RowData_SaleApplications_SaleApplicationId",
                        column: x => x.SaleApplicationId,
                        principalTable: "SaleApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HourData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RowDataId = table.Column<int>(type: "INTEGER", nullable: false),
                    Hour = table.Column<int>(type: "INTEGER", nullable: false),
                    Volume = table.Column<decimal>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HourData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HourData_RowData_RowDataId",
                        column: x => x.RowDataId,
                        principalTable: "RowData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HourData_RowDataId",
                table: "HourData",
                column: "RowDataId");

            migrationBuilder.CreateIndex(
                name: "IX_RowData_SaleApplicationId",
                table: "RowData",
                column: "SaleApplicationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HourData");

            migrationBuilder.DropTable(
                name: "RowData");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "DailyResults");

            migrationBuilder.AddColumn<byte>(
                name: "Hour",
                table: "SaleApplications",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<uint>(
                name: "Price",
                table: "SaleApplications",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<byte>(
                name: "Step",
                table: "SaleApplications",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AlterColumn<uint>(
                name: "OfferedVolume",
                table: "DailyResults",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<uint>(
                name: "AcceptedVolume",
                table: "DailyResults",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");
        }
    }
}

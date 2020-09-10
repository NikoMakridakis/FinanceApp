using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Symbol = table.Column<string>(nullable: true),
                    Exchange = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    TodaysDate = table.Column<DateTime>(nullable: false),
                    LatestPrice = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    LatestDate = table.Column<DateTime>(nullable: false),
                    Open = table.Column<double>(nullable: false),
                    OpenTime = table.Column<DateTime>(nullable: false),
                    Close = table.Column<double>(nullable: false),
                    CloseTime = table.Column<DateTime>(nullable: false),
                    PreviousClose = table.Column<double>(nullable: false),
                    Change = table.Column<double>(nullable: false),
                    ChangePercentage = table.Column<double>(nullable: false),
                    High = table.Column<double>(nullable: false),
                    Low = table.Column<double>(nullable: false),
                    IsUsMarketOpen = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks");
        }
    }
}

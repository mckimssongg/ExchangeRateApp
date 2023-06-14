using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class addRolTransactionExchangeRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RolRefID",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExchangeRate",
                columns: table => new
                {
                    ExchangeRateID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyBase = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrencyDestination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeRate", x => x.ExchangeRateID);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    RolID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.RolID);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserRefID = table.Column<int>(type: "int", nullable: false),
                    SourceCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExchangeRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transaction_User_UserRefID",
                        column: x => x.UserRefID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Rol",
                columns: new[] { "RolID", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "General" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_RolRefID",
                table: "User",
                column: "RolRefID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_UserRefID",
                table: "Transaction",
                column: "UserRefID");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Rol_RolRefID",
                table: "User",
                column: "RolRefID",
                principalTable: "Rol",
                principalColumn: "RolID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Rol_RolRefID",
                table: "User");

            migrationBuilder.DropTable(
                name: "ExchangeRate");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_User_RolRefID",
                table: "User");

            migrationBuilder.DropColumn(
                name: "RolRefID",
                table: "User");
        }
    }
}

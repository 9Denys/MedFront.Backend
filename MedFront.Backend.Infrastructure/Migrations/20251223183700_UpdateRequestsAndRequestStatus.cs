using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedFront.Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRequestsAndRequestStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Requests_StockId_CreatedAt",
                table: "Requests",
                columns: new[] { "StockId", "CreatedAt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Requests_StockId_CreatedAt",
                table: "Requests");
        }
    }
}

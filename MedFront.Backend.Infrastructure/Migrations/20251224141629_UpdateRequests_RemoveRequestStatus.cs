using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedFront.Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRequests_RemoveRequestStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_MedicationStocks_StockId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_RequestStatuses_StatusId",
                table: "Requests");

            migrationBuilder.DropTable(
                name: "RequestStatuses");

            migrationBuilder.RenameColumn(
                name: "StockId",
                table: "Requests",
                newName: "WarehouseId");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Requests",
                newName: "MedicationId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_StockId_CreatedAt",
                table: "Requests",
                newName: "IX_Requests_WarehouseId_CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_StockId",
                table: "Requests",
                newName: "IX_Requests_WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_StatusId",
                table: "Requests",
                newName: "IX_Requests_MedicationId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Requests",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequestStatus",
                table: "Requests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_MedicationId_CreatedAt",
                table: "Requests",
                columns: new[] { "MedicationId", "CreatedAt" });

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Medications_MedicationId",
                table: "Requests",
                column: "MedicationId",
                principalTable: "Medications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Warehouses_WarehouseId",
                table: "Requests",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Medications_MedicationId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Warehouses_WarehouseId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_MedicationId_CreatedAt",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "RequestStatus",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "WarehouseId",
                table: "Requests",
                newName: "StockId");

            migrationBuilder.RenameColumn(
                name: "MedicationId",
                table: "Requests",
                newName: "StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_WarehouseId_CreatedAt",
                table: "Requests",
                newName: "IX_Requests_StockId_CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_WarehouseId",
                table: "Requests",
                newName: "IX_Requests_StockId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_MedicationId",
                table: "Requests",
                newName: "IX_Requests_StatusId");

            migrationBuilder.CreateTable(
                name: "RequestStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    StatusName = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStatuses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestStatuses_StatusName",
                table: "RequestStatuses",
                column: "StatusName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_MedicationStocks_StockId",
                table: "Requests",
                column: "StockId",
                principalTable: "MedicationStocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_RequestStatuses_StatusId",
                table: "Requests",
                column: "StatusId",
                principalTable: "RequestStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedFront.Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fix_RequestStatus_StatusName_ToInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
        ALTER TABLE "RequestStatuses"
        ALTER COLUMN "StatusName" TYPE integer
        USING "StatusName"::integer;
    """);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
        ALTER TABLE "RequestStatuses"
        ALTER COLUMN "StatusName" TYPE character varying(50)
        USING "StatusName"::text;
    """);
        }

    }
}

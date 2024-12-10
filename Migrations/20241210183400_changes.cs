using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dietologist_backend.Migrations
{
    /// <inheritdoc />
    public partial class changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_appointments_provided_services_facilitiy_id",
                table: "appointments");

            migrationBuilder.RenameColumn(
                name: "facilitiy_id",
                table: "appointments",
                newName: "provided_service_id");

            migrationBuilder.RenameIndex(
                name: "ix_appointments_facilitiy_id",
                table: "appointments",
                newName: "ix_appointments_provided_service_id");

            migrationBuilder.AddForeignKey(
                name: "fk_appointments_provided_services_provided_service_id",
                table: "appointments",
                column: "provided_service_id",
                principalTable: "provided_services",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_appointments_provided_services_provided_service_id",
                table: "appointments");

            migrationBuilder.RenameColumn(
                name: "provided_service_id",
                table: "appointments",
                newName: "facilitiy_id");

            migrationBuilder.RenameIndex(
                name: "ix_appointments_provided_service_id",
                table: "appointments",
                newName: "ix_appointments_facilitiy_id");

            migrationBuilder.AddForeignKey(
                name: "fk_appointments_provided_services_facilitiy_id",
                table: "appointments",
                column: "facilitiy_id",
                principalTable: "provided_services",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

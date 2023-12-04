using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StolovkyZilina.Migrations
{
    /// <inheritdoc />
    public partial class InitAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "472ba632-6133-44a1-b158-6c10bd7d850d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3911a1cc-3331-4a7e-b9f9-27ae25e440ff", "AQAAAAIAAYagAAAAEPVU4dXJwj5tY9eNR8xAAOksRxe7RiQA43kh17R4PP5mxIPRz1hsTJNapxUpAUCnAA==", "bc5eb700-6444-417e-ae45-3f49f84ca6bc" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "472ba632-6133-44a1-b158-6c10bd7d850d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "25770fb7-6635-49e4-850f-dc8c132179cf", "AQAAAAIAAYagAAAAEINyh0o73CvD0NrWiaQowuvnaCjp0dHJ8PFxKbEJhE2nMaiZ3S20w/qizB/NeSxXgg==", "841c4a15-a376-4a2e-9dcd-6d6d3b72ee56" });
        }
    }
}

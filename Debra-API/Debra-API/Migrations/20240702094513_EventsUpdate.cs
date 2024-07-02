using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Debra_API.Migrations
{
    /// <inheritdoc />
    public partial class EventsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "Events",
                type: "VarBinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "VarBinary(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "Events",
                type: "VarBinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "VarBinary(max)",
                oldNullable: true);
        }
    }
}

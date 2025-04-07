using Microsoft.EntityFrameworkCore.Migrations;

namespace AppointmentSchedurer.Migrations
{
    public partial class RemoveIsForClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsForClient",
                table: "Appointments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsForClient",
                table: "Appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

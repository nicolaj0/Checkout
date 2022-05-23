using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Checkout.Migrations
{
    public partial class InitialCreate4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestCardTypeId",
                table: "PaymentRequests");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequestCardTypeId",
                table: "PaymentRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

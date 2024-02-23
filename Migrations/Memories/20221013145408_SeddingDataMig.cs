using Microsoft.EntityFrameworkCore.Migrations;

namespace Memories.Migrations.Memories
{
    public partial class SeddingDataMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Gateways",
                keyColumn: "GateWayId",
                keyValue: 2,
                column: "MerchReturnUrl",
                value: "https://memories.beintrackpay.com/Response");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Gateways",
                keyColumn: "GateWayId",
                keyValue: 2,
                column: "MerchReturnUrl",
                value: "https://Memories.beintrackpay.com/Response");
        }
    }
}

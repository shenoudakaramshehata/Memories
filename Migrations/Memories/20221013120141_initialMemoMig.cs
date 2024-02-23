using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Memories.Migrations.Memories
{
    public partial class initialMemoMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gateways",
                columns: table => new
                {
                    GateWayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    success_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    error_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductionURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchantUdf1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchantUdf2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchantUdf3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchantUdf4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchantUdf5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchantPaymentRef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchReturnUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchPayType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchantPaymentLang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ENCRP_KEY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchantId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApiKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Testmode = table.Column<int>(type: "int", nullable: true),
                    version = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gateways", x => x.GateWayId);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    LanguageModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.LanguageModelId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentStatus",
                columns: table => new
                {
                    PaymentStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentStatus", x => x.PaymentStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amout = table.Column<double>(type: "float", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GateWayId = table.Column<int>(type: "int", nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attachment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Userid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    issuccess = table.Column<bool>(type: "bit", nullable: false),
                    payment_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TranID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ref = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrackID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Auth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiptNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchUdf1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchUdf2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchUdf3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CCMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchUdf4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchUdf5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ErrorCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentStatusId = table.Column<int>(type: "int", nullable: true),
                    resultIndicator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    successIndicator = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentModelId);
                    table.ForeignKey(
                        name: "FK_Payments_Gateways_GateWayId",
                        column: x => x.GateWayId,
                        principalTable: "Gateways",
                        principalColumn: "GateWayId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_PaymentStatus_PaymentStatusId",
                        column: x => x.PaymentStatusId,
                        principalTable: "PaymentStatus",
                        principalColumn: "PaymentStatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Gateways",
                columns: new[] { "GateWayId", "ApiKey", "AuthUrl", "ENCRP_KEY", "MerchPayType", "MerchReturnUrl", "MerchantId", "MerchantPaymentLang", "MerchantPaymentRef", "MerchantUdf1", "MerchantUdf2", "MerchantUdf3", "MerchantUdf4", "MerchantUdf5", "Password", "ProductionURL", "TestURL", "Testmode", "Title", "UserName", "error_url", "success_url", "version" },
                values: new object[,]
                {
                    { 1, null, null, null, null, null, "900185001", null, null, null, null, null, null, null, "937cd5c82ab0949b7afad77cd8958122", null, "https://nbkpayment.gateway.mastercard.com/api/rest", null, "MasterCard-NBK", "merchant.900185001", null, null, "65" },
                    { 2, null, "https://pg.cbk.com/ePay/api/cbk/online/pg/merchant/Authenticate", "FbwZvfx-xudBGOszQa-nkarUVel9jDSqm7MKZKoJ9KyybsEXb9hfiP7gaJ3--BSL78VK-k2rd6tTeISdpCnRu9gSlspqr0jU90C1h-k3yXs1", null, "https://Memories.beintrackpay.com/Response", null, "en", "test", null, null, null, null, null, "-B76ERtLowiNGtH8fYTTU8yqkeAii9O99bP8lhD6xh81", null, "https://pg.cbk.com/ePay/pg/epay?_v=", null, "KNET-CBK", "27387462", null, null, null },
                    { 3, null, null, null, null, null, "BEINTRACK", null, null, null, null, null, null, null, "12aa799d8ad04626fb7f739550674868", null, "https://ap-gateway.mastercard.com/api/rest", null, "MasterCard-CBK", "merchant.BEINTRACK", null, null, "65" }
                });

            migrationBuilder.InsertData(
                table: "PaymentStatus",
                columns: new[] { "PaymentStatusId", "Title" },
                values: new object[,]
                {
                    { 1, "Waiting" },
                    { 2, "Success" },
                    { 3, "Failed" },
                    { 4, "Not Paied" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_GateWayId",
                table: "Payments",
                column: "GateWayId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentStatusId",
                table: "Payments",
                column: "PaymentStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Gateways");

            migrationBuilder.DropTable(
                name: "PaymentStatus");
        }
    }
}

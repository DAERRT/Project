using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kek.Migrations
{
    /// <inheritdoc />
    public partial class ini2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "261514b4-de9d-4d6f-97c2-9c93b0a9a529",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "dc52d48e-c6ff-4ab8-9c4c-c8f027488d43", "AQAAAAIAAYagAAAAEO+4GBBDp1IT6Lcbr5zt/Xx+fRv3MYVqmkma/vzI/KGtPmzdsny9O2tmGOyQJcT2IA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4D929078-D64B-483B-9F91-B5E943981BB2",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "49185084-370b-4add-8024-9df47b9d35ea", "CUSTOMER@GMAIL.COM", "AQAAAAIAAYagAAAAEAYiX+iDZOvtk2zWG1hswwhlth4aFjRiNFFBPZbNoffywV4LBE0iEcnRL8IyWIuVRw==", "customer@gmail.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "261514b4-de9d-4d6f-97c2-9c93b0a9a529",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "61f9a0eb-c75f-48e5-bcb9-6596e398d1a4", "AQAAAAIAAYagAAAAEAylDRpCs1XcLmXb8VtLsSK2Zd8YpMAsaXMOeT9je4FJmqsL7v7nV+5b2Vk2EleKXg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4D929078-D64B-483B-9F91-B5E943981BB2",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "6581df53-807b-416a-a1e4-f41b7363d4e2", "ЗАКАЗЧИК", "AQAAAAIAAYagAAAAEI7pnJAleajrDtUB5fZD73xiwybMu+oC2s2Iin8aqMRgePITutrGSJhX08Y3RCuTYQ==", "заказчик" });
        }
    }
}

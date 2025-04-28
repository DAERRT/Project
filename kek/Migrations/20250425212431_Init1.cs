using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kek.Migrations
{
    /// <inheritdoc />
    public partial class Init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "261514b4-de9d-4d6f-97c2-9c93b0a9a529",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "61f9a0eb-c75f-48e5-bcb9-6596e398d1a4", "AQAAAAIAAYagAAAAEAylDRpCs1XcLmXb8VtLsSK2Zd8YpMAsaXMOeT9je4FJmqsL7v7nV+5b2Vk2EleKXg==" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateCreated", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "StudyGroup", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4D929078-D64B-483B-9F91-B5E943981BB2", 0, "6581df53-807b-416a-a1e4-f41b7363d4e2", new DateTime(2025, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "customer@gmail.com", true, "заказчик", "заказчик", false, null, "CUSTOMER@GMAIL.COM", "ЗАКАЗЧИК", "AQAAAAIAAYagAAAAEI7pnJAleajrDtUB5fZD73xiwybMu+oC2s2Iin8aqMRgePITutrGSJhX08Y3RCuTYQ==", "89999999999", true, "", 2, "заказчик", false, "заказчик" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "9061B6D9-C4B0-4CDD-9D32-8D7E7BC73ADA", "4D929078-D64B-483B-9F91-B5E943981BB2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9061B6D9-C4B0-4CDD-9D32-8D7E7BC73ADA", "4D929078-D64B-483B-9F91-B5E943981BB2" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4D929078-D64B-483B-9F91-B5E943981BB2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "261514b4-de9d-4d6f-97c2-9c93b0a9a529",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e4ea4679-9201-4dce-9521-8135921a7d27", "AQAAAAIAAYagAAAAEDdqtcLzllZaFo2OCq7y/6XA6yuDIwCMCJPIMb/bYiuVOOh3dG3ldNleg5xLdXsynQ==" });
        }
    }
}

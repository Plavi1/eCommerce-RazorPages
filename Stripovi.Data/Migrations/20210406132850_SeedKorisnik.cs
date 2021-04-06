using Microsoft.EntityFrameworkCore.Migrations;

namespace Stripovi.Data.Migrations
{
    public partial class SeedKorisnik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "69378942-dd39-43f3-902b-6717196f2efd", "AQAAAAEAACcQAAAAENVzC/WvTwz1VCHQLS6CPVq+CS5Bt9D56228ez4I50fFB3cl58bptPh/4IIlfUVQ+g==", "5eec3675-a591-42c7-a681-3476dba533d2" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "02174cf0–9412–4cfe-afbf-59f706d72cf1", 0, "1d9d3a12-db3b-4c35-8a98-7c91b3996e3e", "korisnik1@korisnik.com", true, true, null, "KORISNIK1@KORISNIK.COM", "KORISNIK1@KORISNIK.COM", "AQAAAAEAACcQAAAAENNqRmXNlVzOjDt+H7fa4W9inWV/LCf4o0eis4K9oAf7/g1q8RIlv4aYnt+9OhTwPA==", null, false, "44d41187-ca2f-4def-bf52-8145be973402", false, "korisnik1@korisnik.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2955af9a-d6d7-4315-977a-1ecab14efa4b", "AQAAAAEAACcQAAAAECfgmn6g/qcsukB/MVIFi08yN5O1I00pi5EX3M5D5XhrqcxMTo48XEFx+9L6nKnv+w==", "90191b56-fbe7-4542-b86f-34189302a13e" });
        }
    }
}

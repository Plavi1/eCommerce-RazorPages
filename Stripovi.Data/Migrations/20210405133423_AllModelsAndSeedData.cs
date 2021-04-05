using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stripovi.Data.Migrations
{
    public partial class AllModelsAndSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kontakt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImePrezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Razlog = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Naslov = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Komentar = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kontakt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Korpa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IdStripa = table.Column<int>(type: "int", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Naslov = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Izdavac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VremePosta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Stanje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Jezik = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GodinaIzdanja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cena = table.Column<int>(type: "int", nullable: false),
                    imgRoute = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korpa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Korpa_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Porudzbina",
                columns: table => new
                {
                    IdPorudzbine = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Grad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostanskiBroj = table.Column<int>(type: "int", nullable: false),
                    Ulica = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KucniBroj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Placanje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pitanje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UkupnaCena = table.Column<int>(type: "int", nullable: false),
                    BrojPorucenihStripova = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VremePosiljke = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Porudzbina", x => x.IdPorudzbine);
                    table.ForeignKey(
                        name: "FK_Porudzbina_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Strip",
                columns: table => new
                {
                    IdStripa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Naslov = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Izdavac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stanje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Jezik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GodinaIzdanja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cena = table.Column<int>(type: "int", nullable: false),
                    imgRoute = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Strip", x => x.IdStripa);
                });

            migrationBuilder.CreateTable(
                name: "StripInPorudzbina",
                columns: table => new
                {
                    IdPorudzbine = table.Column<int>(type: "int", nullable: false),
                    IdStripa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StripInPorudzbina", x => new { x.IdPorudzbine, x.IdStripa });
                    table.ForeignKey(
                        name: "FK_StripInPorudzbina_Porudzbina_IdPorudzbine",
                        column: x => x.IdPorudzbine,
                        principalTable: "Porudzbina",
                        principalColumn: "IdPorudzbine",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StripInPorudzbina_Strip_IdStripa",
                        column: x => x.IdStripa,
                        principalTable: "Strip",
                        principalColumn: "IdStripa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2955af9a-d6d7-4315-977a-1ecab14efa4b", "AQAAAAEAACcQAAAAECfgmn6g/qcsukB/MVIFi08yN5O1I00pi5EX3M5D5XhrqcxMTo48XEFx+9L6nKnv+w==", "90191b56-fbe7-4542-b86f-34189302a13e" });

            migrationBuilder.InsertData(
                table: "Strip",
                columns: new[] { "IdStripa", "Cena", "GodinaIzdanja", "Izdavac", "Jezik", "Naslov", "Naziv", "Stanje", "imgRoute" },
                values: new object[,]
                {
                    { 1, 230, "2010", "Veseli Cetvrtak", "Srpski", "Cena izdaje", "Mister NO", "Novo", "MisterNO.jpg" },
                    { 2, 300, "2006", "Veseli Cetvrtak", "Srpski", "Prica o Dilanu Dogu", "Dilan Dog", "Novo", "DilanDog.jpg" },
                    { 3, 250, "1995", "Classic", "Srpski", "Tako je nastala grupa", "Alan Ford", "Novo", "AlanFord.jpg" },
                    { 4, 300, "2000", "Wizard", "Srpski", "Pobuna Trapera", "Blek", "Polovno", "Blek.jpg" },
                    { 5, 100, "2012", "Veseli Cetvrtak", "Srpski", "Gospodari", "Zagor", "Polovno", "Zagor.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Korpa_UserId",
                table: "Korpa",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Porudzbina_UserId",
                table: "Porudzbina",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StripInPorudzbina_IdStripa",
                table: "StripInPorudzbina",
                column: "IdStripa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kontakt");

            migrationBuilder.DropTable(
                name: "Korpa");

            migrationBuilder.DropTable(
                name: "StripInPorudzbina");

            migrationBuilder.DropTable(
                name: "Porudzbina");

            migrationBuilder.DropTable(
                name: "Strip");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cd7c928e-c667-41e2-87f0-a1543c5562c4", "AQAAAAEAACcQAAAAEAQb9geoabXxBZ1zaDSsmQtz/4Y8EBlMebhp6CqgWzZZfq/Cuda++fYt4ZdwPRRo3Q==", "cc3fdf91-784e-4ceb-bd0c-493ec2a112a7" });
        }
    }
}

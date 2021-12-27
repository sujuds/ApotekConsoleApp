using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApotekConsoleApp.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Obats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Kode = table.Column<string>(nullable: true),
                    Nama = table.Column<string>(nullable: true),
                    Stok = table.Column<int>(nullable: false),
                    Harga = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transaksis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Kode = table.Column<string>(nullable: true),
                    Total = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaksis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransaksiDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TransaksiId = table.Column<int>(nullable: true),
                    ObatId = table.Column<int>(nullable: true),
                    Jumlah = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransaksiDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransaksiDetails_Obats_ObatId",
                        column: x => x.ObatId,
                        principalTable: "Obats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransaksiDetails_Transaksis_TransaksiId",
                        column: x => x.TransaksiId,
                        principalTable: "Transaksis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransaksiDetails_ObatId",
                table: "TransaksiDetails",
                column: "ObatId");

            migrationBuilder.CreateIndex(
                name: "IX_TransaksiDetails_TransaksiId",
                table: "TransaksiDetails",
                column: "TransaksiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransaksiDetails");

            migrationBuilder.DropTable(
                name: "Obats");

            migrationBuilder.DropTable(
                name: "Transaksis");
        }
    }
}

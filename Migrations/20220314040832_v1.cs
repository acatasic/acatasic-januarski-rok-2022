using Microsoft.EntityFrameworkCore.Migrations;

namespace zoo.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategorija",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tip = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorija", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProdKuca",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdKuca", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Film",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ocena = table.Column<float>(type: "real", nullable: false),
                    BrojOcena = table.Column<int>(type: "int", nullable: false),
                    KategorijaID = table.Column<int>(type: "int", nullable: true),
                    ProdukcijskaKucaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Film", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Film_Kategorija_KategorijaID",
                        column: x => x.KategorijaID,
                        principalTable: "Kategorija",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Film_ProdKuca_ProdukcijskaKucaID",
                        column: x => x.ProdukcijskaKucaID,
                        principalTable: "ProdKuca",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KategorijaProdukcijskaKuca",
                columns: table => new
                {
                    KategorijaID = table.Column<int>(type: "int", nullable: false),
                    ProdukcijskaKucaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KategorijaProdukcijskaKuca", x => new { x.KategorijaID, x.ProdukcijskaKucaID });
                    table.ForeignKey(
                        name: "FK_KategorijaProdukcijskaKuca_Kategorija_KategorijaID",
                        column: x => x.KategorijaID,
                        principalTable: "Kategorija",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KategorijaProdukcijskaKuca_ProdKuca_ProdukcijskaKucaID",
                        column: x => x.ProdukcijskaKucaID,
                        principalTable: "ProdKuca",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Film_KategorijaID",
                table: "Film",
                column: "KategorijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Film_ProdukcijskaKucaID",
                table: "Film",
                column: "ProdukcijskaKucaID");

            migrationBuilder.CreateIndex(
                name: "IX_KategorijaProdukcijskaKuca_ProdukcijskaKucaID",
                table: "KategorijaProdukcijskaKuca",
                column: "ProdukcijskaKucaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Film");

            migrationBuilder.DropTable(
                name: "KategorijaProdukcijskaKuca");

            migrationBuilder.DropTable(
                name: "Kategorija");

            migrationBuilder.DropTable(
                name: "ProdKuca");
        }
    }
}

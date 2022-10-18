using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DostavaHrane.Data.Migrations
{
    public partial class initialsetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Restoran",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restoran", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Jelo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cijena = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RestoranID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jelo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Jelo_Restoran_RestoranID",
                        column: x => x.RestoranID,
                        principalTable: "Restoran",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jelo_RestoranID",
                table: "Jelo",
                column: "RestoranID");



            //inicijalni unos podataka (olakšica da već budu popunjenje tabele kako bi odmah mogli testirati pregled entiteta

            migrationBuilder.InsertData(
               table: "Restoran",
          columns: new[] { "Naziv", "Adresa", "Telefon" },
          values: new object[,]
          {{ "Montana", "Grbavička 22", "333-222" }, { "Kordoba", "Ferhadija 22", "333-222" }, { "Chipas", "Dzemala Bijedica 22", "333-222" }  });

            migrationBuilder.InsertData(
               table: "Jelo",
          columns: new[] { "Naziv", "Cijena", "RestoranID" },
          values: new object[,]
          {{ "piletina", 9.50, 1 }, { "sendvič", 3.90,  2 }, { "čorba", 4,  2 }  });


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jelo");

            migrationBuilder.DropTable(
                name: "Restoran");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DentistaCadeirasAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cadeiras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fabricante = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UltimaManutencao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ProximaManutencao = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cadeiras", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Alocacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Inicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Fim = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CadeiraId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alocacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alocacoes_Cadeiras_CadeiraId",
                        column: x => x.CadeiraId,
                        principalTable: "Cadeiras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Cadeiras",
                columns: new[] { "Id", "Descricao", "Fabricante", "Numero", "ProximaManutencao", "UltimaManutencao" },
                values: new object[,]
                {
                    { 1, "Cadeira 1", "Fabricante A", 1, new DateTime(2024, 6, 25, 13, 28, 16, 0, DateTimeKind.Local).AddTicks(7964), new DateTime(2024, 6, 25, 13, 28, 16, 0, DateTimeKind.Local).AddTicks(7952) },
                    { 2, "Cadeira 2", "Fabricante A", 2, new DateTime(2024, 6, 25, 13, 28, 16, 0, DateTimeKind.Local).AddTicks(7966), new DateTime(2024, 6, 25, 13, 28, 16, 0, DateTimeKind.Local).AddTicks(7966) },
                    { 3, "Cadeira 3", "Fabricante B", 3, new DateTime(2024, 6, 25, 13, 28, 16, 0, DateTimeKind.Local).AddTicks(7968), new DateTime(2024, 6, 25, 13, 28, 16, 0, DateTimeKind.Local).AddTicks(7968) },
                    { 4, "Cadeira 4", "Fabricante B", 4, new DateTime(2024, 6, 25, 13, 28, 16, 0, DateTimeKind.Local).AddTicks(7970), new DateTime(2024, 6, 25, 13, 28, 16, 0, DateTimeKind.Local).AddTicks(7969) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alocacoes_CadeiraId",
                table: "Alocacoes",
                column: "CadeiraId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alocacoes");

            migrationBuilder.DropTable(
                name: "Cadeiras");
        }
    }
}

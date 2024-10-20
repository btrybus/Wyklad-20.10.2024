using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FabrykaNTPZ.Data.Migrations
{
    /// <inheritdoc />
    public partial class fabryka1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hale",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hale", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operatorzy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwisko = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Placa = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operatorzy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Maszyny",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data_uruchomienia = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HalaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maszyny", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Maszyny_Hale_HalaId",
                        column: x => x.HalaId,
                        principalTable: "Hale",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Operator_Maszyna",
                columns: table => new
                {
                    OperatorId = table.Column<int>(type: "int", nullable: false),
                    MaszynaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operator_Maszyna", x => new { x.MaszynaId, x.OperatorId });
                    table.ForeignKey(
                        name: "FK_Operator_Maszyna_Maszyny_MaszynaId",
                        column: x => x.MaszynaId,
                        principalTable: "Maszyny",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Operator_Maszyna_Operatorzy_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "Operatorzy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Maszyny_HalaId",
                table: "Maszyny",
                column: "HalaId");

            migrationBuilder.CreateIndex(
                name: "IX_Operator_Maszyna_OperatorId",
                table: "Operator_Maszyna",
                column: "OperatorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Operator_Maszyna");

            migrationBuilder.DropTable(
                name: "Maszyny");

            migrationBuilder.DropTable(
                name: "Operatorzy");

            migrationBuilder.DropTable(
                name: "Hale");
        }
    }
}

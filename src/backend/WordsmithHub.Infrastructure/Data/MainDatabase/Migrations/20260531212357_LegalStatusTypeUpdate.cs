using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WordsmithHub.Infrastructure.Data.MainDatabase.Migrations
{
    /// <inheritdoc />
    public partial class LegalStatusTypeUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "LegalStatuses");

            migrationBuilder.AddColumn<int>(
                name: "LegalStatusTypeId",
                table: "LegalStatuses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LegalStatusTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    Code = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalStatusTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "LegalStatusTypes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "author", "Artiste-auteur" },
                    { 2, "self-employed", "Auto-entrepreneur" },
                    { 3, "wagePortage", "Portage salarial" },
                    { 4, "llc", "SARL" },
                    { 5, "eurl", "EURL" },
                    { 6, "sasu", "SASU" },
                    { 7, "ei", "Entreprise individuelle" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LegalStatuses_LegalStatusTypeId",
                table: "LegalStatuses",
                column: "LegalStatusTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LegalStatuses_LegalStatusTypes_LegalStatusTypeId",
                table: "LegalStatuses",
                column: "LegalStatusTypeId",
                principalTable: "LegalStatusTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LegalStatuses_LegalStatusTypes_LegalStatusTypeId",
                table: "LegalStatuses");

            migrationBuilder.DropTable(
                name: "LegalStatusTypes");

            migrationBuilder.DropIndex(
                name: "IX_LegalStatuses_LegalStatusTypeId",
                table: "LegalStatuses");

            migrationBuilder.DropColumn(
                name: "LegalStatusTypeId",
                table: "LegalStatuses");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "LegalStatuses",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}

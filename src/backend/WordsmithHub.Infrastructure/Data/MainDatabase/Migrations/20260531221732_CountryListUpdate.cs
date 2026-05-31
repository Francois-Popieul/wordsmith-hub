using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WordsmithHub.Infrastructure.Data.MainDatabase.Migrations
{
    /// <inheritdoc />
    public partial class CountryListUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "IsEuropeanUnionMember", "Name" },
                values: new object[,]
                {
                    { 14, "AUT", true, "Autriche" },
                    { 15, "BGR", true, "Bulgarie" },
                    { 16, "HRV", true, "Croatie" },
                    { 17, "CYP", true, "Chypre" },
                    { 18, "CZE", true, "Tchéquie" },
                    { 19, "DNK", true, "Danemark" },
                    { 20, "EST", true, "Estonie" },
                    { 21, "FIN", true, "Finlande" },
                    { 22, "GRC", true, "Grèce" },
                    { 23, "HUN", true, "Hongrie" },
                    { 24, "IRL", true, "Irlande" },
                    { 25, "LVA", true, "Lettonie" },
                    { 26, "LTU", true, "Lituanie" },
                    { 27, "LUX", true, "Luxembourg" },
                    { 28, "MLT", true, "Malte" },
                    { 29, "POL", true, "Pologne" },
                    { 30, "ROU", true, "Roumanie" },
                    { 31, "SVK", true, "Slovaquie" },
                    { 32, "SVN", true, "Slovénie" },
                    { 33, "SWE", true, "Suède" },
                    { 34, "BRA", false, "Brésil" },
                    { 35, "MEX", false, "Mexique" },
                    { 36, "ARG", false, "Argentine" },
                    { 37, "CHL", false, "Chili" },
                    { 38, "CHN", false, "Chine" },
                    { 39, "KOR", false, "Corée du Sud" },
                    { 40, "IND", false, "Inde" },
                    { 41, "IDN", false, "Indonésie" },
                    { 42, "RUS", false, "Russie" },
                    { 43, "TUR", false, "Turquie" },
                    { 44, "SAU", false, "Arabie saoudite" },
                    { 45, "ZAF", false, "Afrique du Sud" },
                    { 46, "NOR", false, "Norvège" },
                    { 47, "ISL", false, "Islande" },
                    { 48, "UKR", false, "Ukraine" },
                    { 49, "SRB", false, "Serbie" },
                    { 50, "MAR", false, "Maroc" },
                    { 51, "TUN", false, "Tunisie" },
                    { 52, "EGY", false, "Égypte" },
                    { 53, "DZA", false, "Algérie" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 53);
        }
    }
}

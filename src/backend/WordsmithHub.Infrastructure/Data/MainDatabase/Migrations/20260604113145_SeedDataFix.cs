using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WordsmithHub.Infrastructure.Data.MainDatabase.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PricingUnits",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "PricingUnits",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "PricingUnits",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DomainTypes",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "DomainTypes",
                type: "character varying(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "DomainTypes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.InsertData(
                table: "DomainTypes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "administration", "Administration" },
                    { 2, "publicAdministration", "Administration publique" },
                    { 3, "aeronautics", "Aéronautique" },
                    { 4, "agriculture", "Agriculture" },
                    { 5, "architecture", "Architecture" },
                    { 6, "arts", "Arts" },
                    { 7, "appliedArts", "Arts appliqués" },
                    { 8, "performingArts", "Arts du spectacle" },
                    { 9, "visualArts", "Arts plastiques" },
                    { 10, "insurance", "Assurances" },
                    { 11, "astronautics", "Astronautique" },
                    { 12, "astronomy", "Astronomie" },
                    { 13, "audiovisual", "Audiovisuel" },
                    { 14, "automotive", "Automobile" },
                    { 15, "banking", "Banque" },
                    { 16, "comicsManga", "BD et manga" },
                    { 17, "fineArts", "Beaux-arts" },
                    { 18, "biology", "Biologie" },
                    { 19, "botany", "Botanique" },
                    { 20, "quarrying", "Carrières" },
                    { 21, "boilermaking", "Chaudronnerie" },
                    { 22, "chemistry", "Chimie" },
                    { 23, "petrochemistry", "Chimie du pétrole" },
                    { 24, "surgery", "Chirurgie" },
                    { 25, "cinema", "Cinéma" },
                    { 26, "commerce", "Commerce" },
                    { 27, "accounting", "Comptabilité" },
                    { 28, "construction", "Construction" },
                    { 29, "crypto", "Cryptomonnaies" },
                    { 30, "gastronomy", "Cuisine et gastronomie" },
                    { 31, "culture", "Culture" },
                    { 32, "cybernetics", "Cybernétique" },
                    { 33, "cybersecurity", "Cybersécurité" },
                    { 34, "demography", "Démographie" },
                    { 35, "dentistry", "Dentisterie" },
                    { 36, "administrativeLaw", "Droit administratif" },
                    { 37, "commercialLaw", "Droit commercial" },
                    { 38, "constitutionalLaw", "Droit constitutionnel" },
                    { 39, "lawJustice", "Droit et justice" },
                    { 40, "internationalLaw", "Droit international" },
                    { 41, "judicialLaw", "Droit judiciaire" },
                    { 42, "miningLaw", "Droit minier" },
                    { 43, "criminalLaw", "Droit pénal" },
                    { 44, "privateLaw", "Droit privé" },
                    { 45, "socialLaw", "Droit social" },
                    { 46, "ecology", "Écologie" },
                    { 47, "economics", "Économie" },
                    { 48, "education", "Éducation" },
                    { 49, "physicalEducation", "Éducation physique et sportive" },
                    { 50, "electricity", "Électricité" },
                    { 51, "electronics", "Électronique" },
                    { 52, "animalFarming", "Élevage" },
                    { 53, "packaging", "Emballages" },
                    { 54, "energies", "Énergies" },
                    { 55, "renewableEnergy", "Énergies renouvelables" },
                    { 56, "environment", "Environnement" },
                    { 57, "medicalEquipment", "Équipement médico-chirurgical" },
                    { 58, "mining", "Exploitation minière" },
                    { 59, "oilGasExtraction", "Extraction du pétrole et du gaz naturel" },
                    { 60, "finance", "Finances" },
                    { 61, "geography", "Géographie" },
                    { 62, "geology", "Géologie" },
                    { 63, "businessManagement", "Gestion de l'entreprise" },
                    { 64, "hrManagement", "Gestion du personnel" },
                    { 65, "history", "Histoire" },
                    { 66, "hospitality", "Hôtellerie" },
                    { 67, "medicalImaging", "Imagerie médicale" },
                    { 68, "realEstate", "Immobilier" },
                    { 69, "industry", "Industrie" },
                    { 70, "it", "Informatique" },
                    { 71, "engineering", "Ingénierie" },
                    { 72, "ai", "Intelligence artificielle" },
                    { 73, "videoGame", "Jeu vidéo" },
                    { 74, "toysGames", "Jeux et jouets" },
                    { 75, "jewelry", "Joaillerie" },
                    { 76, "lifting", "Levage" },
                    { 77, "linguistics", "Linguistique" },
                    { 78, "literature", "Littérature" },
                    { 79, "logistics", "Logistique" },
                    { 80, "legalDocuments", "Lois et documents juridiques" },
                    { 81, "leisure", "Loisirs" },
                    { 82, "handling", "Manutention" },
                    { 83, "marketing", "Marketing" },
                    { 84, "militaryEquipment", "Matériel militaire" },
                    { 85, "mathematics", "Mathématiques" },
                    { 86, "mechanics", "Mécanique" },
                    { 87, "medicine", "Médecine" },
                    { 88, "veterinaryMedicine", "Médecine vétérinaire" },
                    { 89, "metallurgy", "Métallurgie" },
                    { 90, "meteorology", "Météorologie" },
                    { 91, "defense", "Militaire et défense" },
                    { 92, "minesQuarries", "Mines et carrières" },
                    { 93, "metalMining", "Mines métalliques" },
                    { 94, "fashionBeauty", "Mode et beauté" },
                    { 95, "museology", "Muséologie et patrimoine" },
                    { 96, "music", "Musique" },
                    { 97, "nuclear", "Nucléaire" },
                    { 98, "oenology", "Œnologie" },
                    { 99, "ophthalmology", "Ophtalmologie" },
                    { 100, "paperIndustry", "Papeterie" },
                    { 101, "paramilitary", "Paramilitaire" },
                    { 102, "pedagogy", "Pédagogie" },
                    { 103, "oil", "Pétrole" },
                    { 104, "crudeOilProducts", "Pétrole brut et dérivés" },
                    { 105, "pharmacology", "Pharmacologie" },
                    { 106, "philosophyReligion", "Philosophie et religion" },
                    { 107, "photography", "Photographie" },
                    { 108, "physics", "Physique" },
                    { 109, "plumbing", "Plomberie" },
                    { 110, "politics", "Politique" },
                    { 111, "socialPolicies", "Politiques sociales" },
                    { 112, "postalServices", "Postes" },
                    { 113, "mineralProspecting", "Prospection minière" },
                    { 114, "robotics", "Robotique" },
                    { 115, "psychology", "Psychologie" },
                    { 116, "advertising", "Publicité" },
                    { 117, "oilRefining", "Raffinage du pétrole" },
                    { 118, "scientificResearch", "Recherche scientifique" },
                    { 119, "publicRelations", "Relations publiques" },
                    { 120, "restaurantIndustry", "Restauration" },
                    { 121, "health", "Santé" },
                    { 122, "science", "Sciences" },
                    { 123, "humanities", "Sciences humaines" },
                    { 124, "naturalSciences", "Sciences naturelles" },
                    { 125, "politicalScience", "Sciences politiques" },
                    { 126, "security", "Sécurité" },
                    { 127, "fireSafety", "Sécurité incendie" },
                    { 128, "steelIndustry", "Sidérurgie" },
                    { 129, "sociology", "Sociologie" },
                    { 130, "sports", "Sports" },
                    { 131, "combatSports", "Sports de combat" },
                    { 132, "teamSports", "Sports d'équipe" },
                    { 133, "waterSports", "Sports nautiques" },
                    { 134, "storage", "Stockage" },
                    { 135, "weaponSystems", "Systèmes d'armes" },
                    { 136, "petroleumTech", "Technologies pétrolières" },
                    { 137, "telecom", "Télécommunications" },
                    { 138, "remoteSensing", "Télédétection" },
                    { 139, "telegraphy", "Télégraphie" },
                    { 140, "telephony", "Téléphonie" },
                    { 141, "tourism", "Tourisme" },
                    { 142, "airTransport", "Transport aérien" },
                    { 143, "waterTransport", "Transport par eau" },
                    { 144, "railTransport", "Transport par rail" },
                    { 145, "roadTransport", "Transport routier" },
                    { 146, "transport", "Transports" },
                    { 147, "urbanPlanning", "Urbanisme" },
                    { 148, "zoology", "Zoologie" },
                    { 200, "other", "Autre" }
                });

            migrationBuilder.InsertData(
                table: "PricingUnits",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "perWord", "par mot" },
                    { 2, "perMinute", "par minute" },
                    { 3, "perHour", "par heure" },
                    { 4, "perDay", "par jour" },
                    { 5, "flatRate", "forfait" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "DomainTypes",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "PricingUnits",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PricingUnits",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PricingUnits",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PricingUnits",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PricingUnits",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PricingUnits",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "PricingUnits",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "PricingUnits",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DomainTypes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "DomainTypes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "DomainTypes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
        }
    }
}

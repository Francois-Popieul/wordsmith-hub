using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WordsmithHub.Infrastructure.Data.MainDatabase.Migrations
{
    /// <inheritdoc />
    public partial class DomainEntitiesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DirectClients");

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Code = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    IsEuropeanUnionMember = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    Symbol = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "text", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    VatAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalAmountWithVat = table.Column<decimal>(type: "numeric", nullable: false),
                    WorkOrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    FreelanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    DirectCustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CustomerName = table.Column<string>(type: "text", nullable: false),
                    CustomerPhone = table.Column<string>(type: "text", nullable: false),
                    CustomerAddress = table.Column<string>(type: "text", nullable: false),
                    CustomerSiretOrSiren = table.Column<string>(type: "text", nullable: true),
                    FreelanceName = table.Column<string>(type: "text", nullable: false),
                    FreelancePhone = table.Column<string>(type: "text", nullable: false),
                    FreelanceAddress = table.Column<string>(type: "text", nullable: false),
                    FreelanceSiret = table.Column<string>(type: "text", nullable: true),
                    FreelanceVatNumber = table.Column<string>(type: "text", nullable: true),
                    FreelanceVatExemption = table.Column<bool>(type: "boolean", nullable: false),
                    FreelanceTaxDeductionExemption = table.Column<bool>(type: "boolean", nullable: false),
                    FreelanceBankName = table.Column<string>(type: "text", nullable: false),
                    FreelanceBankAccountHolder = table.Column<string>(type: "text", nullable: false),
                    FreelanceIban = table.Column<string>(type: "text", nullable: false),
                    FreelanceBic = table.Column<string>(type: "text", nullable: false),
                    UsedCurrencyId = table.Column<int>(type: "integer", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    Category = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TranslationLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    Code = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranslationLanguages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EndCustomers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndCustomers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EndCustomers_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Freelances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    Address_StreetInfo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Address_Complement = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Address_PostCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Address_City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Address_State = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Address_CountryId = table.Column<int>(type: "integer", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    Freelance_Address_CountryId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Freelances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Freelances_Countries_Freelance_Address_CountryId",
                        column: x => x.Freelance_Address_CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Freelances_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Label = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BankName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    AccountHolderName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Iban = table.Column<string>(type: "character varying(34)", maxLength: 34, nullable: false),
                    Bic = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    FreelanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccounts_Freelances_FreelanceId",
                        column: x => x.FreelanceId,
                        principalTable: "Freelances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BankAccounts_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DirectCustomers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Code = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Phone = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Address_StreetInfo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Address_Complement = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Address_PostCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Address_City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Address_State = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Address_CountryId = table.Column<int>(type: "integer", nullable: false),
                    SiretOrSiren = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    PaymentDelay = table.Column<int>(type: "integer", nullable: false),
                    FreelanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrencyId = table.Column<int>(type: "integer", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    DirectCustomer_Address_CountryId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectCustomers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DirectCustomers_Countries_DirectCustomer_Address_CountryId",
                        column: x => x.DirectCustomer_Address_CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectCustomers_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectCustomers_Freelances_FreelanceId",
                        column: x => x.FreelanceId,
                        principalTable: "Freelances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectCustomers_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LegalStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Siret = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: true),
                    VatNumber = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: true),
                    VatExemption = table.Column<bool>(type: "boolean", nullable: false),
                    VatRate = table.Column<decimal>(type: "numeric(4,2)", nullable: true),
                    TaxDeductionExemption = table.Column<bool>(type: "boolean", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "date", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "date", nullable: true),
                    FreelanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LegalStatuses_Freelances_FreelanceId",
                        column: x => x.FreelanceId,
                        principalTable: "Freelances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LegalStatuses_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Domain = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    FreelanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    EndCustomerId = table.Column<Guid>(type: "uuid", nullable: true),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_EndCustomers_EndCustomerId",
                        column: x => x.EndCustomerId,
                        principalTable: "EndCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_Freelances_FreelanceId",
                        column: x => x.FreelanceId,
                        principalTable: "Freelances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(10,4)", nullable: false),
                    Unit = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    SourceLanguageId = table.Column<int>(type: "integer", nullable: false),
                    TargetLanguageId = table.Column<int>(type: "integer", nullable: false),
                    ServiceId = table.Column<int>(type: "integer", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    DirectCustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    FreelanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rates_DirectCustomers_DirectCustomerId",
                        column: x => x.DirectCustomerId,
                        principalTable: "DirectCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rates_Freelances_FreelanceId",
                        column: x => x.FreelanceId,
                        principalTable: "Freelances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rates_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rates_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rates_TranslationLanguages_SourceLanguageId",
                        column: x => x.SourceLanguageId,
                        principalTable: "TranslationLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rates_TranslationLanguages_TargetLanguageId",
                        column: x => x.TargetLanguageId,
                        principalTable: "TranslationLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Manages",
                columns: table => new
                {
                    DirectCustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manages", x => new { x.DirectCustomerId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_Manages_DirectCustomer",
                        column: x => x.DirectCustomerId,
                        principalTable: "DirectCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Manages_Project",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Reference = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    FreelanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    DirectCustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uuid", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrders_DirectCustomers_DirectCustomerId",
                        column: x => x.DirectCustomerId,
                        principalTable: "DirectCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrders_Freelances_FreelanceId",
                        column: x => x.FreelanceId,
                        principalTable: "Freelances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrders_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrders_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrders_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderLines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric(10,4)", nullable: false),
                    AppliedUnitPrice = table.Column<decimal>(type: "numeric(10,4)", nullable: false),
                    UsedUnit = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    WorkOrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    SourceLanguageId = table.Column<int>(type: "integer", nullable: false),
                    TargetLanguageId = table.Column<int>(type: "integer", nullable: false),
                    ServiceId = table.Column<int>(type: "integer", nullable: false),
                    Notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderLines_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderLines_TranslationLanguages_SourceLanguageId",
                        column: x => x.SourceLanguageId,
                        principalTable: "TranslationLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderLines_TranslationLanguages_TargetLanguageId",
                        column: x => x.TargetLanguageId,
                        principalTable: "TranslationLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderLines_WorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "IsEuropeanUnionMember", "Name" },
                values: new object[,]
                {
                    { 1, "FRA", true, "France" },
                    { 2, "DEU", true, "Allemagne" },
                    { 3, "ESP", true, "Espagne" },
                    { 4, "ITA", true, "Italie" },
                    { 5, "PRT", true, "Portugal" },
                    { 6, "BEL", true, "Belgique" },
                    { 7, "NLD", true, "Pays-Bas" },
                    { 8, "CHE", false, "Suisse" },
                    { 9, "GBR", false, "Royaume-Uni" },
                    { 10, "USA", false, "États-Unis d’Amérique" },
                    { 11, "CAN", false, "Canada" },
                    { 12, "AUS", false, "Australie" },
                    { 13, "JPN", false, "Japon" }
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Code", "Name", "Symbol" },
                values: new object[,]
                {
                    { 1, "USD", "Dollar", "$" },
                    { 2, "EUR", "Euro", "€" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Traduction" },
                    { 2, "Relecture" },
                    { 3, "Sous-titrage" },
                    { 4, "Post-édition" },
                    { 5, "Contrôle qualité" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Category", "Name" },
                values: new object[,]
                {
                    { 1, "General", "Actif" },
                    { 2, "General", "Inactif" },
                    { 10, "Invoice", "Brouillon" },
                    { 11, "Invoice", "Envoyée" },
                    { 12, "Invoice", "Payée" },
                    { 13, "Invoice", "Annulée" },
                    { 20, "WorkOrder", "En attente" },
                    { 21, "WorkOrder", "En cours" },
                    { 22, "WorkOrder", "Terminée" },
                    { 23, "WorkOrder", "Livrée" },
                    { 30, "Project", "En cours" },
                    { 31, "Project", "Terminé" }
                });

            migrationBuilder.InsertData(
                table: "TranslationLanguages",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "EN", "Anglais" },
                    { 2, "FR", "Français" },
                    { 3, "ES", "Espagnol" },
                    { 4, "DE", "Allemand" },
                    { 5, "IT", "Italien" },
                    { 6, "PT", "Portugais" },
                    { 7, "NL", "Néerlandais" },
                    { 8, "RU", "Russe" },
                    { 9, "JA", "Japonais" },
                    { 10, "ZH", "Chinois" },
                    { 11, "AR", "Arabe" },
                    { 12, "HI", "Hindi" },
                    { 13, "KO", "Coréen" },
                    { 14, "TR", "Turc" },
                    { 15, "PL", "Polonais" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_FreelanceId",
                table: "BankAccounts",
                column: "FreelanceId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_StatusId",
                table: "BankAccounts",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCustomers_CurrencyId",
                table: "DirectCustomers",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCustomers_DirectCustomer_Address_CountryId",
                table: "DirectCustomers",
                column: "DirectCustomer_Address_CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCustomers_FreelanceId",
                table: "DirectCustomers",
                column: "FreelanceId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCustomers_StatusId",
                table: "DirectCustomers",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_EndCustomers_StatusId",
                table: "EndCustomers",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Freelances_Freelance_Address_CountryId",
                table: "Freelances",
                column: "Freelance_Address_CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Freelances_StatusId",
                table: "Freelances",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalStatuses_FreelanceId",
                table: "LegalStatuses",
                column: "FreelanceId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalStatuses_StatusId",
                table: "LegalStatuses",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Manages_ProjectId",
                table: "Manages",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_ServiceId",
                table: "OrderLines",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_SourceLanguageId",
                table: "OrderLines",
                column: "SourceLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_TargetLanguageId",
                table: "OrderLines",
                column: "TargetLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_WorkOrderId",
                table: "OrderLines",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_EndCustomerId",
                table: "Projects",
                column: "EndCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_FreelanceId",
                table: "Projects",
                column: "FreelanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_StatusId",
                table: "Projects",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_DirectCustomerId",
                table: "Rates",
                column: "DirectCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_FreelanceId",
                table: "Rates",
                column: "FreelanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_ServiceId",
                table: "Rates",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_SourceLanguageId",
                table: "Rates",
                column: "SourceLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_StatusId",
                table: "Rates",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_TargetLanguageId",
                table: "Rates",
                column: "TargetLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_DirectCustomerId",
                table: "WorkOrders",
                column: "DirectCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_FreelanceId",
                table: "WorkOrders",
                column: "FreelanceId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_InvoiceId",
                table: "WorkOrders",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_ProjectId",
                table: "WorkOrders",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_StatusId",
                table: "WorkOrders",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "LegalStatuses");

            migrationBuilder.DropTable(
                name: "Manages");

            migrationBuilder.DropTable(
                name: "OrderLines");

            migrationBuilder.DropTable(
                name: "Rates");

            migrationBuilder.DropTable(
                name: "WorkOrders");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "TranslationLanguages");

            migrationBuilder.DropTable(
                name: "DirectCustomers");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "EndCustomers");

            migrationBuilder.DropTable(
                name: "Freelances");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.CreateTable(
                name: "DirectClients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CompanyName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PaymentDelay = table.Column<int>(type: "integer", nullable: false),
                    Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectClients", x => x.Id);
                });
        }
    }
}

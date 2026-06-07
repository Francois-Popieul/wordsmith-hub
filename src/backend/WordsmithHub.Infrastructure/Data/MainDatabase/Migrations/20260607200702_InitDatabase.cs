using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WordsmithHub.Infrastructure.Data.MainDatabase.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
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
                name: "DataProtectionKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FriendlyName = table.Column<string>(type: "text", nullable: true),
                    Xml = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataProtectionKeys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DomainTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomainTypes", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "PricingUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricingUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
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
                    Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Address_StreetInfo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Address_Complement = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Address_PostCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Address_City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Address_State = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Address_CountryId = table.Column<int>(type: "integer", nullable: true),
                    AppUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Freelances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Freelances_Countries_Address_CountryId",
                        column: x => x.Address_CountryId,
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
                    Iban = table.Column<string>(type: "text", nullable: false),
                    Bic = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
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
                    Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
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
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectCustomers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DirectCustomers_Countries_Address_CountryId",
                        column: x => x.Address_CountryId,
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
                name: "FreelanceServices",
                columns: table => new
                {
                    FreelanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreelanceServices", x => new { x.FreelanceId, x.ServiceId });
                    table.ForeignKey(
                        name: "FK_FreelanceServices_Freelances_FreelanceId",
                        column: x => x.FreelanceId,
                        principalTable: "Freelances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FreelanceServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FreelanceSourceLanguages",
                columns: table => new
                {
                    FreelanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    SourceLanguageId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreelanceSourceLanguages", x => new { x.FreelanceId, x.SourceLanguageId });
                    table.ForeignKey(
                        name: "FK_FreelanceSourceLanguages_Freelances_FreelanceId",
                        column: x => x.FreelanceId,
                        principalTable: "Freelances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FreelanceSourceLanguages_TranslationLanguages_SourceLanguag~",
                        column: x => x.SourceLanguageId,
                        principalTable: "TranslationLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FreelanceTargetLanguages",
                columns: table => new
                {
                    FreelanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetLanguageId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreelanceTargetLanguages", x => new { x.FreelanceId, x.TargetLanguageId });
                    table.ForeignKey(
                        name: "FK_FreelanceTargetLanguages_Freelances_FreelanceId",
                        column: x => x.FreelanceId,
                        principalTable: "Freelances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FreelanceTargetLanguages_TranslationLanguages_TargetLanguag~",
                        column: x => x.TargetLanguageId,
                        principalTable: "TranslationLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LegalStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LegalStatusTypeId = table.Column<int>(type: "integer", nullable: false),
                    Siret = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: true),
                    VatNumber = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: true),
                    VatExemption = table.Column<bool>(type: "boolean", nullable: false),
                    VatRate = table.Column<decimal>(type: "numeric(4,2)", nullable: true),
                    TaxDeductionExemption = table.Column<bool>(type: "boolean", nullable: false),
                    ValidFrom = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ValidTo = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
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
                        name: "FK_LegalStatuses_LegalStatusTypes_LegalStatusTypeId",
                        column: x => x.LegalStatusTypeId,
                        principalTable: "LegalStatusTypes",
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
                name: "ProjectDirectCustomers",
                columns: table => new
                {
                    DirectCustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDirectCustomers", x => new { x.DirectCustomerId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_ProjectDirectCustomers_DirectCustomer",
                        column: x => x.DirectCustomerId,
                        principalTable: "DirectCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectDirectCustomers_Project",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    { 13, "JPN", false, "Japon" },
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

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Code", "Name", "Symbol" },
                values: new object[,]
                {
                    { 1, "USD", "Dollar américain", "$" },
                    { 2, "EUR", "Euro", "€" },
                    { 3, "JPY", "Yen japonais", "¥" },
                    { 4, "GBP", "Livre sterling", "£" },
                    { 5, "AUD", "Dollar australien", "$" },
                    { 6, "CAD", "Dollar canadien", "$" },
                    { 7, "CHF", "Franc suisse", "CHF" },
                    { 8, "CNY", "Yuan renminbi chinois", "¥" },
                    { 9, "SEK", "Couronne suédoise", "kr" },
                    { 10, "NZD", "Dollar néo‑zélandais", "$" },
                    { 11, "MXN", "Peso mexicain", "$" },
                    { 12, "SGD", "Dollar de Singapour", "$" },
                    { 13, "HKD", "Dollar de Hong Kong", "$" },
                    { 14, "NOK", "Couronne norvégienne", "kr" },
                    { 15, "KRW", "Won sud‑coréen", "₩" },
                    { 16, "TRY", "Livre turque", "₺" },
                    { 17, "RUB", "Rouble russe", "₽" },
                    { 18, "INR", "Roupie indienne", "₹" },
                    { 19, "BRL", "Real brésilien", "R$" },
                    { 20, "ZAR", "Rand sud‑africain", "R" },
                    { 21, "DKK", "Couronne danoise", "kr" },
                    { 22, "PLN", "Zloty polonais", "zł" },
                    { 23, "THB", "Baht thaïlandais", "฿" },
                    { 24, "HUF", "Forint hongrois", "Ft" },
                    { 25, "CZK", "Couronne tchèque", "Kč" },
                    { 26, "ILS", "Shekel israélien", "₪" },
                    { 27, "PHP", "Peso philippin", "₱" },
                    { 28, "MYR", "Ringgit malaisien", "RM" },
                    { 29, "AED", "Dirham des Émirats arabes unis", "د.إ" },
                    { 30, "SAR", "Riyal saoudien", "﷼" },
                    { 31, "KWD", "Dinar koweïtien", "KD" },
                    { 32, "BHD", "Dinar bahreïni", "BD" },
                    { 33, "ARS", "Peso argentin", "$" },
                    { 34, "NGN", "Naira nigérian", "₦" },
                    { 35, "KES", "Shilling kényan", "KSh" }
                });

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

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Traduction" },
                    { 2, "Relecture" },
                    { 3, "Sous-titrage" },
                    { 4, "Post-édition" },
                    { 5, "Contrôle qualité" },
                    { 6, "Transcription" },
                    { 7, "Traduction certifiée" },
                    { 8, "Localisation" },
                    { 9, "Transcréation" },
                    { 10, "Révision bilingue" },
                    { 11, "Correction monolingue" },
                    { 12, "Alignement de documents" },
                    { 13, "Gestion terminologique" },
                    { 14, "Création de glossaire" },
                    { 15, "Traduction SEO" },
                    { 16, "Voix off" },
                    { 17, "Doublage" },
                    { 18, "Interprétation simultanée" },
                    { 19, "Interprétation consécutive" },
                    { 20, "Interprétation téléphonique" },
                    { 21, "Mise en page (DTP)" },
                    { 22, "Formatage de fichiers" },
                    { 23, "Extraction de texte" },
                    { 24, "Nettoyage de fichiers" },
                    { 25, "Évaluation linguistique" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Category", "Name" },
                values: new object[,]
                {
                    { 1, "General", "Actif" },
                    { 2, "General", "Inactif" },
                    { 3, "General", "Brouillon" },
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
                    { 15, "PL", "Polonais" },
                    { 16, "HE", "Hébreu" },
                    { 17, "UR", "Urdu" },
                    { 18, "VI", "Vietnamien" },
                    { 19, "SV", "Suédois" },
                    { 20, "DA", "Danois" },
                    { 21, "FI", "Finnois" },
                    { 22, "NO", "Norvégien" },
                    { 23, "EL", "Grec" },
                    { 24, "CS", "Tchèque" },
                    { 25, "SK", "Slovaque" },
                    { 26, "HU", "Hongrois" },
                    { 27, "RO", "Roumain" },
                    { 28, "BG", "Bulgare" },
                    { 29, "UK", "Ukrainien" },
                    { 30, "SR", "Serbe" },
                    { 31, "HR", "Croate" },
                    { 32, "SL", "Slovène" },
                    { 33, "ID", "Indonésien" },
                    { 34, "MS", "Malais" },
                    { 35, "TH", "Thaï" },
                    { 36, "BN", "Bengali" },
                    { 37, "TA", "Tamoul" },
                    { 38, "FA", "Persan (Farsi)" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_FreelanceId_IsDefault",
                table: "BankAccounts",
                columns: new[] { "FreelanceId", "IsDefault" },
                unique: true,
                filter: "\"IsDefault\" = TRUE");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_StatusId",
                table: "BankAccounts",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCustomers_Address_CountryId",
                table: "DirectCustomers",
                column: "Address_CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCustomers_CurrencyId",
                table: "DirectCustomers",
                column: "CurrencyId");

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
                name: "IX_Freelances_Address_CountryId",
                table: "Freelances",
                column: "Address_CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Freelances_StatusId",
                table: "Freelances",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_FreelanceServices_ServiceId",
                table: "FreelanceServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_FreelanceSourceLanguages_SourceLanguageId",
                table: "FreelanceSourceLanguages",
                column: "SourceLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_FreelanceTargetLanguages_TargetLanguageId",
                table: "FreelanceTargetLanguages",
                column: "TargetLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalStatuses_FreelanceId",
                table: "LegalStatuses",
                column: "FreelanceId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalStatuses_LegalStatusTypeId",
                table: "LegalStatuses",
                column: "LegalStatusTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalStatuses_StatusId",
                table: "LegalStatuses",
                column: "StatusId");

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
                name: "IX_ProjectDirectCustomers_ProjectId",
                table: "ProjectDirectCustomers",
                column: "ProjectId");

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
                name: "DataProtectionKeys");

            migrationBuilder.DropTable(
                name: "DomainTypes");

            migrationBuilder.DropTable(
                name: "FreelanceServices");

            migrationBuilder.DropTable(
                name: "FreelanceSourceLanguages");

            migrationBuilder.DropTable(
                name: "FreelanceTargetLanguages");

            migrationBuilder.DropTable(
                name: "LegalStatuses");

            migrationBuilder.DropTable(
                name: "OrderLines");

            migrationBuilder.DropTable(
                name: "PricingUnits");

            migrationBuilder.DropTable(
                name: "ProjectDirectCustomers");

            migrationBuilder.DropTable(
                name: "Rates");

            migrationBuilder.DropTable(
                name: "LegalStatusTypes");

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
        }
    }
}

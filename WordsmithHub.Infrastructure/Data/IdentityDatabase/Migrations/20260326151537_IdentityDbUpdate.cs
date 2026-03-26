using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordsmithHub.Infrastructure.Data.IdentityDatabase.Migrations
{
    /// <inheritdoc />
    public partial class IdentityDbUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8feb56a9-5b14-4a47-be5f-b56e1c822e1c",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEAlbrepvlKVebZAroVmr5FbaT7Vrw7NJ4xlz+tywUvL5PcTuiA2S0Bp2cduePDp9Eg==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8feb56a9-5b14-4a47-be5f-b56e1c822e1c",
                column: "PasswordHash",
                value: "$2a$12$sYInbRCWSrpoZMGr7I0v2eUIGj2NE2kyAmOF5EE62B83tIePYvcdO");
        }
    }
}

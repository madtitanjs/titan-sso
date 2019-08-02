using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SSO.Core.Data.Persist
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OidcDeviceFlowCodes",
                columns: table => new
                {
                    UserCode = table.Column<string>(maxLength: 200, nullable: false),
                    DeviceCode = table.Column<string>(maxLength: 200, nullable: false),
                    SubjectId = table.Column<string>(maxLength: 200, nullable: true),
                    ClientId = table.Column<string>(maxLength: 200, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Expiration = table.Column<DateTime>(nullable: false),
                    Data = table.Column<string>(maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OidcDeviceFlowCodes", x => x.UserCode);
                });

            migrationBuilder.CreateTable(
                name: "OidcPersistedGrants",
                columns: table => new
                {
                    Key = table.Column<string>(maxLength: 200, nullable: false),
                    Type = table.Column<string>(maxLength: 50, nullable: false),
                    SubjectId = table.Column<string>(maxLength: 200, nullable: true),
                    ClientId = table.Column<string>(maxLength: 200, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Expiration = table.Column<DateTime>(nullable: true),
                    Data = table.Column<string>(maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OidcPersistedGrants", x => x.Key);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OidcDeviceFlowCodes_DeviceCode",
                table: "OidcDeviceFlowCodes",
                column: "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OidcDeviceFlowCodes_Expiration",
                table: "OidcDeviceFlowCodes",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_OidcPersistedGrants_SubjectId_ClientId_Type_Expiration",
                table: "OidcPersistedGrants",
                columns: new[] { "SubjectId", "ClientId", "Type", "Expiration" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OidcDeviceFlowCodes");

            migrationBuilder.DropTable(
                name: "OidcPersistedGrants");
        }
    }
}

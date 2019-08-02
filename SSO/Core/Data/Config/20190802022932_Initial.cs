using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SSO.Core.Data.Config
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OidcApiResource",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Enabled = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    LastAccessed = table.Column<DateTime>(nullable: true),
                    NonEditable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OidcApiResource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OidcClient",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Enabled = table.Column<bool>(nullable: false),
                    ClientId = table.Column<string>(maxLength: 200, nullable: false),
                    ProtocolType = table.Column<string>(maxLength: 200, nullable: false),
                    RequireClientSecret = table.Column<bool>(nullable: false),
                    ClientName = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    ClientUri = table.Column<string>(maxLength: 2000, nullable: true),
                    LogoUri = table.Column<string>(maxLength: 2000, nullable: true),
                    RequireConsent = table.Column<bool>(nullable: false),
                    AllowRememberConsent = table.Column<bool>(nullable: false),
                    AlwaysIncludeUserClaimsInIdToken = table.Column<bool>(nullable: false),
                    RequirePkce = table.Column<bool>(nullable: false),
                    AllowPlainTextPkce = table.Column<bool>(nullable: false),
                    AllowAccessTokensViaBrowser = table.Column<bool>(nullable: false),
                    FrontChannelLogoutUri = table.Column<string>(maxLength: 2000, nullable: true),
                    FrontChannelLogoutSessionRequired = table.Column<bool>(nullable: false),
                    BackChannelLogoutUri = table.Column<string>(maxLength: 2000, nullable: true),
                    BackChannelLogoutSessionRequired = table.Column<bool>(nullable: false),
                    AllowOfflineAccess = table.Column<bool>(nullable: false),
                    IdentityTokenLifetime = table.Column<int>(nullable: false),
                    AccessTokenLifetime = table.Column<int>(nullable: false),
                    AuthorizationCodeLifetime = table.Column<int>(nullable: false),
                    ConsentLifetime = table.Column<int>(nullable: true),
                    AbsoluteRefreshTokenLifetime = table.Column<int>(nullable: false),
                    SlidingRefreshTokenLifetime = table.Column<int>(nullable: false),
                    RefreshTokenUsage = table.Column<int>(nullable: false),
                    UpdateAccessTokenClaimsOnRefresh = table.Column<bool>(nullable: false),
                    RefreshTokenExpiration = table.Column<int>(nullable: false),
                    AccessTokenType = table.Column<int>(nullable: false),
                    EnableLocalLogin = table.Column<bool>(nullable: false),
                    IncludeJwtId = table.Column<bool>(nullable: false),
                    AlwaysSendClientClaims = table.Column<bool>(nullable: false),
                    ClientClaimsPrefix = table.Column<string>(maxLength: 200, nullable: true),
                    PairWiseSubjectSalt = table.Column<string>(maxLength: 200, nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    LastAccessed = table.Column<DateTime>(nullable: true),
                    UserSsoLifetime = table.Column<int>(nullable: true),
                    UserCodeType = table.Column<string>(maxLength: 100, nullable: true),
                    DeviceCodeLifetime = table.Column<int>(nullable: false),
                    NonEditable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OidcClient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OidcIdentityResource",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Enabled = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Required = table.Column<bool>(nullable: false),
                    Emphasize = table.Column<bool>(nullable: false),
                    ShowInDiscoveryDocument = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    NonEditable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OidcIdentityResource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OidcApiClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(maxLength: 200, nullable: false),
                    ApiResourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OidcApiClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OidcApiClaims_OidcApiResource_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalTable: "OidcApiResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OidcApiResourceProperty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(maxLength: 250, nullable: false),
                    Value = table.Column<string>(maxLength: 2000, nullable: false),
                    ApiResourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OidcApiResourceProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OidcApiResourceProperty_OidcApiResource_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalTable: "OidcApiResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OidcApiScope",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Required = table.Column<bool>(nullable: false),
                    Emphasize = table.Column<bool>(nullable: false),
                    ShowInDiscoveryDocument = table.Column<bool>(nullable: false),
                    ApiResourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OidcApiScope", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OidcApiScope_OidcApiResource_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalTable: "OidcApiResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OidcApiSecret",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Value = table.Column<string>(maxLength: 4000, nullable: false),
                    Expiration = table.Column<DateTime>(nullable: true),
                    Type = table.Column<string>(maxLength: 250, nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    ApiResourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OidcApiSecret", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OidcApiSecret_OidcApiResource_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalTable: "OidcApiResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OidcClientClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(maxLength: 250, nullable: false),
                    Value = table.Column<string>(maxLength: 250, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OidcClientClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OidcClientClaims_OidcClient_ClientId",
                        column: x => x.ClientId,
                        principalTable: "OidcClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OidcClientCorsOrigin",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Origin = table.Column<string>(maxLength: 150, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OidcClientCorsOrigin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OidcClientCorsOrigin_OidcClient_ClientId",
                        column: x => x.ClientId,
                        principalTable: "OidcClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OidcClientGrantType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GrantType = table.Column<string>(maxLength: 250, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OidcClientGrantType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OidcClientGrantType_OidcClient_ClientId",
                        column: x => x.ClientId,
                        principalTable: "OidcClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OidcClientIdPRestriction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Provider = table.Column<string>(maxLength: 200, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OidcClientIdPRestriction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OidcClientIdPRestriction_OidcClient_ClientId",
                        column: x => x.ClientId,
                        principalTable: "OidcClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OidcClientPostLogoutRedirectUri",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PostLogoutRedirectUri = table.Column<string>(maxLength: 2000, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OidcClientPostLogoutRedirectUri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OidcClientPostLogoutRedirectUri_OidcClient_ClientId",
                        column: x => x.ClientId,
                        principalTable: "OidcClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OidcClientProperty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(maxLength: 250, nullable: false),
                    Value = table.Column<string>(maxLength: 2000, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OidcClientProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OidcClientProperty_OidcClient_ClientId",
                        column: x => x.ClientId,
                        principalTable: "OidcClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OidcClientRedirectUri",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RedirectUri = table.Column<string>(maxLength: 2000, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OidcClientRedirectUri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OidcClientRedirectUri_OidcClient_ClientId",
                        column: x => x.ClientId,
                        principalTable: "OidcClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OidcClientScopes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Scope = table.Column<string>(maxLength: 200, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OidcClientScopes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OidcClientScopes_OidcClient_ClientId",
                        column: x => x.ClientId,
                        principalTable: "OidcClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OidcClientSecret",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    Value = table.Column<string>(maxLength: 4000, nullable: false),
                    Expiration = table.Column<DateTime>(nullable: true),
                    Type = table.Column<string>(maxLength: 250, nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OidcClientSecret", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OidcClientSecret_OidcClient_ClientId",
                        column: x => x.ClientId,
                        principalTable: "OidcClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OidcIdentityClaim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(maxLength: 200, nullable: false),
                    IdentityResourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OidcIdentityClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OidcIdentityClaim_OidcIdentityResource_IdentityResourceId",
                        column: x => x.IdentityResourceId,
                        principalTable: "OidcIdentityResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OidcIdentityResourceProperty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(maxLength: 250, nullable: false),
                    Value = table.Column<string>(maxLength: 2000, nullable: false),
                    IdentityResourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OidcIdentityResourceProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OidcIdentityResourceProperty_OidcIdentityResource_IdentityResourceId",
                        column: x => x.IdentityResourceId,
                        principalTable: "OidcIdentityResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OidcApiScopeClaim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(maxLength: 200, nullable: false),
                    ApiScopeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OidcApiScopeClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OidcApiScopeClaim_OidcApiScope_ApiScopeId",
                        column: x => x.ApiScopeId,
                        principalTable: "OidcApiScope",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OidcApiClaims_ApiResourceId",
                table: "OidcApiClaims",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_OidcApiResource_Name",
                table: "OidcApiResource",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OidcApiResourceProperty_ApiResourceId",
                table: "OidcApiResourceProperty",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_OidcApiScope_ApiResourceId",
                table: "OidcApiScope",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_OidcApiScope_Name",
                table: "OidcApiScope",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OidcApiScopeClaim_ApiScopeId",
                table: "OidcApiScopeClaim",
                column: "ApiScopeId");

            migrationBuilder.CreateIndex(
                name: "IX_OidcApiSecret_ApiResourceId",
                table: "OidcApiSecret",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_OidcClient_ClientId",
                table: "OidcClient",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OidcClientClaims_ClientId",
                table: "OidcClientClaims",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OidcClientCorsOrigin_ClientId",
                table: "OidcClientCorsOrigin",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OidcClientGrantType_ClientId",
                table: "OidcClientGrantType",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OidcClientIdPRestriction_ClientId",
                table: "OidcClientIdPRestriction",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OidcClientPostLogoutRedirectUri_ClientId",
                table: "OidcClientPostLogoutRedirectUri",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OidcClientProperty_ClientId",
                table: "OidcClientProperty",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OidcClientRedirectUri_ClientId",
                table: "OidcClientRedirectUri",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OidcClientScopes_ClientId",
                table: "OidcClientScopes",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OidcClientSecret_ClientId",
                table: "OidcClientSecret",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OidcIdentityClaim_IdentityResourceId",
                table: "OidcIdentityClaim",
                column: "IdentityResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_OidcIdentityResource_Name",
                table: "OidcIdentityResource",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OidcIdentityResourceProperty_IdentityResourceId",
                table: "OidcIdentityResourceProperty",
                column: "IdentityResourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OidcApiClaims");

            migrationBuilder.DropTable(
                name: "OidcApiResourceProperty");

            migrationBuilder.DropTable(
                name: "OidcApiScopeClaim");

            migrationBuilder.DropTable(
                name: "OidcApiSecret");

            migrationBuilder.DropTable(
                name: "OidcClientClaims");

            migrationBuilder.DropTable(
                name: "OidcClientCorsOrigin");

            migrationBuilder.DropTable(
                name: "OidcClientGrantType");

            migrationBuilder.DropTable(
                name: "OidcClientIdPRestriction");

            migrationBuilder.DropTable(
                name: "OidcClientPostLogoutRedirectUri");

            migrationBuilder.DropTable(
                name: "OidcClientProperty");

            migrationBuilder.DropTable(
                name: "OidcClientRedirectUri");

            migrationBuilder.DropTable(
                name: "OidcClientScopes");

            migrationBuilder.DropTable(
                name: "OidcClientSecret");

            migrationBuilder.DropTable(
                name: "OidcIdentityClaim");

            migrationBuilder.DropTable(
                name: "OidcIdentityResourceProperty");

            migrationBuilder.DropTable(
                name: "OidcApiScope");

            migrationBuilder.DropTable(
                name: "OidcClient");

            migrationBuilder.DropTable(
                name: "OidcIdentityResource");

            migrationBuilder.DropTable(
                name: "OidcApiResource");
        }
    }
}

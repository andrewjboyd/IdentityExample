using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityExample.Migrations
{
    /// <inheritdoc />
    public partial class AspNetClaims : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetClaims",
                columns: table => new
                {
                    ClaimValue = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetClaims", x => x.ClaimValue);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetClaims",
                column: "ClaimValue",
                values: new object[]
                {
                    "identity.admin.approve",
                    "identity.admin.archive",
                    "identity.admin.create",
                    "identity.admin.delete",
                    "identity.admin.download",
                    "identity.admin.edit",
                    "identity.admin.email",
                    "identity.admin.export",
                    "identity.admin.import",
                    "identity.admin.print",
                    "identity.admin.reject",
                    "identity.admin.restore",
                    "identity.admin.upload",
                    "identity.admin.view",
                    "identity.analytics.approve",
                    "identity.analytics.archive",
                    "identity.analytics.create",
                    "identity.analytics.delete",
                    "identity.analytics.download",
                    "identity.analytics.edit",
                    "identity.analytics.email",
                    "identity.analytics.export",
                    "identity.analytics.import",
                    "identity.analytics.print",
                    "identity.analytics.reject",
                    "identity.analytics.restore",
                    "identity.analytics.upload",
                    "identity.analytics.view",
                    "identity.customer.approve",
                    "identity.customer.archive",
                    "identity.customer.create",
                    "identity.customer.delete",
                    "identity.customer.download",
                    "identity.customer.edit",
                    "identity.customer.email",
                    "identity.customer.export",
                    "identity.customer.import",
                    "identity.customer.print",
                    "identity.customer.reject",
                    "identity.customer.restore",
                    "identity.customer.upload",
                    "identity.customer.view",
                    "identity.inventory.approve",
                    "identity.inventory.archive",
                    "identity.inventory.create",
                    "identity.inventory.delete",
                    "identity.inventory.download",
                    "identity.inventory.edit",
                    "identity.inventory.email",
                    "identity.inventory.export",
                    "identity.inventory.import",
                    "identity.inventory.print",
                    "identity.inventory.reject",
                    "identity.inventory.restore",
                    "identity.inventory.upload",
                    "identity.inventory.view",
                    "identity.invoice.approve",
                    "identity.invoice.archive",
                    "identity.invoice.create",
                    "identity.invoice.delete",
                    "identity.invoice.download",
                    "identity.invoice.edit",
                    "identity.invoice.email",
                    "identity.invoice.export",
                    "identity.invoice.import",
                    "identity.invoice.print",
                    "identity.invoice.reject",
                    "identity.invoice.restore",
                    "identity.invoice.upload",
                    "identity.invoice.view",
                    "identity.order.approve",
                    "identity.order.archive",
                    "identity.order.create",
                    "identity.order.delete",
                    "identity.order.download",
                    "identity.order.edit",
                    "identity.order.email",
                    "identity.order.export",
                    "identity.order.import",
                    "identity.order.print",
                    "identity.order.reject",
                    "identity.order.restore",
                    "identity.order.upload",
                    "identity.order.view",
                    "identity.payment.approve",
                    "identity.payment.archive",
                    "identity.payment.create",
                    "identity.payment.delete",
                    "identity.payment.download",
                    "identity.payment.edit",
                    "identity.payment.email",
                    "identity.payment.export",
                    "identity.payment.import",
                    "identity.payment.print",
                    "identity.payment.reject",
                    "identity.payment.restore",
                    "identity.payment.upload",
                    "identity.payment.view",
                    "identity.product.approve",
                    "identity.product.archive",
                    "identity.product.create",
                    "identity.product.delete",
                    "identity.product.download",
                    "identity.product.edit",
                    "identity.product.email",
                    "identity.product.export",
                    "identity.product.import",
                    "identity.product.print",
                    "identity.product.reject",
                    "identity.product.restore",
                    "identity.product.upload",
                    "identity.product.view",
                    "identity.report.approve",
                    "identity.report.archive",
                    "identity.report.create",
                    "identity.report.delete",
                    "identity.report.download",
                    "identity.report.edit",
                    "identity.report.email",
                    "identity.report.export",
                    "identity.report.import",
                    "identity.report.print",
                    "identity.report.reject",
                    "identity.report.restore",
                    "identity.report.upload",
                    "identity.report.view",
                    "identity.user.approve",
                    "identity.user.archive",
                    "identity.user.create",
                    "identity.user.delete",
                    "identity.user.download",
                    "identity.user.edit",
                    "identity.user.email",
                    "identity.user.export",
                    "identity.user.import",
                    "identity.user.print",
                    "identity.user.reject",
                    "identity.user.restore",
                    "identity.user.upload",
                    "identity.user.view"
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetClaims");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}

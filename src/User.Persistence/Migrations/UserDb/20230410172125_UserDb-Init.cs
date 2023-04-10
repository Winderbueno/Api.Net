using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace User.Persistence.Migrations.UserDb
{
    /// <inheritdoc />
    public partial class UserDbInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    FeatureId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Beta = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.FeatureId);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    PermissionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.PermissionId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    UserType = table.Column<int>(type: "INTEGER", nullable: false),
                    Fonction = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "FeaturePermission",
                columns: table => new
                {
                    FeaturesFeatureId = table.Column<int>(type: "INTEGER", nullable: false),
                    PermissionsPermissionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeaturePermission", x => new { x.FeaturesFeatureId, x.PermissionsPermissionId });
                    table.ForeignKey(
                        name: "FK_FeaturePermission_Features_FeaturesFeatureId",
                        column: x => x.FeaturesFeatureId,
                        principalTable: "Features",
                        principalColumn: "FeatureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeaturePermission_Permissions_PermissionsPermissionId",
                        column: x => x.PermissionsPermissionId,
                        principalTable: "Permissions",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeatureRole",
                columns: table => new
                {
                    FeaturesFeatureId = table.Column<int>(type: "INTEGER", nullable: false),
                    RolesRoleId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureRole", x => new { x.FeaturesFeatureId, x.RolesRoleId });
                    table.ForeignKey(
                        name: "FK_FeatureRole_Features_FeaturesFeatureId",
                        column: x => x.FeaturesFeatureId,
                        principalTable: "Features",
                        principalColumn: "FeatureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeatureRole_Roles_RolesRoleId",
                        column: x => x.RolesRoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserKId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdentityId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserKId);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BetaFeatureUserK",
                columns: table => new
                {
                    BetaFeaturesFeatureId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsersUserKId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BetaFeatureUserK", x => new { x.BetaFeaturesFeatureId, x.UsersUserKId });
                    table.ForeignKey(
                        name: "FK_BetaFeatureUserK_Features_BetaFeaturesFeatureId",
                        column: x => x.BetaFeaturesFeatureId,
                        principalTable: "Features",
                        principalColumn: "FeatureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BetaFeatureUserK_Users_UsersUserKId",
                        column: x => x.UsersUserKId,
                        principalTable: "Users",
                        principalColumn: "UserKId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "FeatureId", "Beta", "CreatedAt", "CreatedBy", "ModifiedAt", "ModifiedBy", "Name" },
                values: new object[,]
                {
                    { 1, false, new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7257), "DbInit", new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7259), "DbInit", "client" },
                    { 2, false, new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7260), "DbInit", new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7261), "DbInit", "user" },
                    { 3, false, new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7263), "DbInit", new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7264), "DbInit", "task" },
                    { 4, false, new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7265), "DbInit", new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7266), "DbInit", "contract" }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "CreatedAt", "CreatedBy", "ModifiedAt", "ModifiedBy", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7310), "DbInit", new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7312), "DbInit", "client.read" },
                    { 2, new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7313), "DbInit", new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7314), "DbInit", "client.create" },
                    { 3, new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7315), "DbInit", new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7316), "DbInit", "client.update" },
                    { 4, new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7317), "DbInit", new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7318), "DbInit", "user.read" },
                    { 5, new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7319), "DbInit", new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7320), "DbInit", "user.impersonate" },
                    { 6, new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7321), "DbInit", new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7322), "DbInit", "user.create" },
                    { 7, new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7324), "DbInit", new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7325), "DbInit", "user.update" },
                    { 8, new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7326), "DbInit", new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7327), "DbInit", "user.suspend" },
                    { 9, new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7328), "DbInit", new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7329), "DbInit", "user.deactivate" },
                    { 10, new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7330), "DbInit", new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7331), "DbInit", "task.read" },
                    { 11, new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7332), "DbInit", new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7333), "DbInit", "task.create" },
                    { 12, new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7334), "DbInit", new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7335), "DbInit", "task.update" },
                    { 13, new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7336), "DbInit", new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7337), "DbInit", "contract.read" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "CreatedAt", "CreatedBy", "Fonction", "ModifiedAt", "ModifiedBy", "Name", "UserType" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7123), "DbInit", 5, new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7168), "DbInit", "Entreprise | Support", 0 },
                    { 2, new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7170), "DbInit", 5, new DateTime(2023, 4, 10, 19, 21, 25, 749, DateTimeKind.Local).AddTicks(7171), "DbInit", "Entreprise | Distributeur", 0 }
                });

            migrationBuilder.InsertData(
                table: "FeaturePermission",
                columns: new[] { "FeaturesFeatureId", "PermissionsPermissionId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 4 },
                    { 2, 5 },
                    { 2, 6 },
                    { 2, 7 },
                    { 2, 8 },
                    { 2, 9 },
                    { 3, 10 },
                    { 3, 11 },
                    { 3, 12 },
                    { 4, 13 }
                });

            migrationBuilder.InsertData(
                table: "FeatureRole",
                columns: new[] { "FeaturesFeatureId", "RolesRoleId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 1 },
                    { 3, 1 },
                    { 3, 2 },
                    { 4, 1 },
                    { 4, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BetaFeatureUserK_UsersUserKId",
                table: "BetaFeatureUserK",
                column: "UsersUserKId");

            migrationBuilder.CreateIndex(
                name: "IX_FeaturePermission_PermissionsPermissionId",
                table: "FeaturePermission",
                column: "PermissionsPermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureRole_RolesRoleId",
                table: "FeatureRole",
                column: "RolesRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BetaFeatureUserK");

            migrationBuilder.DropTable(
                name: "FeaturePermission");

            migrationBuilder.DropTable(
                name: "FeatureRole");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}

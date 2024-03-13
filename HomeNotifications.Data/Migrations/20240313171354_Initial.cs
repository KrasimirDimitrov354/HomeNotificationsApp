using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HomeNotifications.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Color = table.Column<string>(type: "char(7)", nullable: false),
                    Created_By_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified_By_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Modified_Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Created_By_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified_By_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Modified_Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PasswordHash = table.Column<string>(type: "char(110)", nullable: false),
                    ChangePassword = table.Column<bool>(type: "bit", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Created_By_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified_By_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Modified_Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_UserRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    NotificationTypeId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created_By_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified_By_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Modified_Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_NotificationTypes_NotificationTypeId",
                        column: x => x.NotificationTypeId,
                        principalTable: "NotificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "NotificationTypes",
                columns: new[] { "Id", "Color", "Created_By_Id", "Created_Date", "Modified_By_Id", "Modified_Date", "Name" },
                values: new object[,]
                {
                    { 1, "#FF0000", new Guid("c85a27d4-d7c9-4293-9f18-93c836896b50"), new DateTime(2024, 3, 13, 19, 13, 54, 111, DateTimeKind.Local).AddTicks(1132), null, null, "High Priority" },
                    { 2, "#FFFF00", new Guid("c85a27d4-d7c9-4293-9f18-93c836896b50"), new DateTime(2024, 3, 13, 19, 13, 54, 111, DateTimeKind.Local).AddTicks(1188), null, null, "Medium Priority" },
                    { 3, "#0080FF", new Guid("c85a27d4-d7c9-4293-9f18-93c836896b50"), new DateTime(2024, 3, 13, 19, 13, 54, 111, DateTimeKind.Local).AddTicks(1192), null, null, "Low Priority" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "Created_By_Id", "Created_Date", "Modified_By_Id", "Modified_Date", "Name" },
                values: new object[,]
                {
                    { 1, new Guid("c85a27d4-d7c9-4293-9f18-93c836896b50"), new DateTime(2024, 3, 13, 19, 13, 54, 116, DateTimeKind.Local).AddTicks(803), null, null, "Admin" },
                    { 2, new Guid("c85a27d4-d7c9-4293-9f18-93c836896b50"), new DateTime(2024, 3, 13, 19, 13, 54, 116, DateTimeKind.Local).AddTicks(820), null, null, "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ChangePassword", "Created_By_Id", "Created_Date", "Modified_By_Id", "Modified_Date", "PasswordHash", "RoleId", "Username" },
                values: new object[] { new Guid("c85a27d4-d7c9-4293-9f18-93c836896b50"), false, new Guid("c85a27d4-d7c9-4293-9f18-93c836896b50"), new DateTime(2024, 3, 13, 19, 13, 54, 115, DateTimeKind.Local).AddTicks(9906), null, null, "392AF712F5350B0C9B9E5D4F2886A908375E2E2B55D8847A45F321ED935AF64F:B9AC4CC6C054102D5C2342BE211EF015:50000:SHA256", 1, "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotificationTypeId",
                table: "Notifications",
                column: "NotificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_OwnerId",
                table: "Notifications",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "NotificationTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserRoles");
        }
    }
}

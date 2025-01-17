﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    HashedPassword = table.Column<string>(type: "longtext", nullable: false),
                    Onleave = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Reason = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    Duration = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AllowedDuration = table.Column<int>(type: "int", nullable: false),
                    RemainingDurationAllowed = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LeaveRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Reason = table.Column<string>(type: "longtext", nullable: false),
                    Duration = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DaysLeft = table.Column<int>(type: "int", nullable: false),
                    AllowedDuration = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Permitted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_UserId",
                table: "LeaveRequests",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveRequests");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

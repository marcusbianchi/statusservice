using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace statusservice.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    statusId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    thingId = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.statusId);
                });

            migrationBuilder.CreateTable(
                name: "StatusDescriptions",
                columns: table => new
                {
                    statusDescriptionId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    context = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    statusId = table.Column<int>(type: "int4", nullable: true),
                    statusName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    timestampTicks = table.Column<long>(type: "int8", nullable: false),
                    value = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusDescriptions", x => x.statusDescriptionId);
                    table.ForeignKey(
                        name: "FK_StatusDescriptions_Status_statusId",
                        column: x => x.statusId,
                        principalTable: "Status",
                        principalColumn: "statusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StatusDescriptions_statusId",
                table: "StatusDescriptions",
                column: "statusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatusDescriptions");

            migrationBuilder.DropTable(
                name: "Status");
        }
    }
}

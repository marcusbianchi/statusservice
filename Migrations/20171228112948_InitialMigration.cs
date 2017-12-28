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
                name: "ThingStatus",
                columns: table => new
                {
                    thingStatusId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    thingId = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThingStatus", x => x.thingStatusId);
                });

            migrationBuilder.CreateTable(
                name: "ContextStatus",
                columns: table => new
                {
                    contextStatusId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    context = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    contextDescription = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    endTimestampTicks = table.Column<long>(type: "int8", nullable: false),
                    startTimestampTicks = table.Column<long>(type: "int8", nullable: false),
                    statusName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    thingStatusId = table.Column<int>(type: "int4", nullable: true),
                    value = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContextStatus", x => x.contextStatusId);
                    table.ForeignKey(
                        name: "FK_ContextStatus_ThingStatus_thingStatusId",
                        column: x => x.thingStatusId,
                        principalTable: "ThingStatus",
                        principalColumn: "thingStatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContextStatus_thingStatusId",
                table: "ContextStatus",
                column: "thingStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContextStatus");

            migrationBuilder.DropTable(
                name: "ThingStatus");
        }
    }
}

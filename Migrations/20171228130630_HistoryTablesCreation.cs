using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace statusservice.Migrations
{
    public partial class HistoryTablesCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContextStatus_ThingStatus_thingStatusId",
                table: "ContextStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ThingStatus",
                table: "ThingStatus");

            migrationBuilder.RenameTable(
                name: "ThingStatus",
                newName: "ActiveThingStatus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActiveThingStatus",
                table: "ActiveThingStatus",
                column: "thingStatusId");

            migrationBuilder.CreateTable(
                name: "HistoryThingStatus",
                columns: table => new
                {
                    historyThingStatusId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    thingId = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryThingStatus", x => x.historyThingStatusId);
                });

            migrationBuilder.CreateTable(
                name: "HistoryContextStatus",
                columns: table => new
                {
                    HistoryContextStatusId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    context = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    contextDescription = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    endTimestampTicks = table.Column<long>(type: "int8", nullable: false),
                    historyThingStatusId = table.Column<int>(type: "int4", nullable: true),
                    startTimestampTicks = table.Column<long>(type: "int8", nullable: false),
                    statusName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    value = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryContextStatus", x => x.HistoryContextStatusId);
                    table.ForeignKey(
                        name: "FK_HistoryContextStatus_HistoryThingStatus_historyThingStatusId",
                        column: x => x.historyThingStatusId,
                        principalTable: "HistoryThingStatus",
                        principalColumn: "historyThingStatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryContextStatus_historyThingStatusId",
                table: "HistoryContextStatus",
                column: "historyThingStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContextStatus_ActiveThingStatus_thingStatusId",
                table: "ContextStatus",
                column: "thingStatusId",
                principalTable: "ActiveThingStatus",
                principalColumn: "thingStatusId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContextStatus_ActiveThingStatus_thingStatusId",
                table: "ContextStatus");

            migrationBuilder.DropTable(
                name: "HistoryContextStatus");

            migrationBuilder.DropTable(
                name: "HistoryThingStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActiveThingStatus",
                table: "ActiveThingStatus");

            migrationBuilder.RenameTable(
                name: "ActiveThingStatus",
                newName: "ThingStatus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ThingStatus",
                table: "ThingStatus",
                column: "thingStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContextStatus_ThingStatus_thingStatusId",
                table: "ContextStatus",
                column: "thingStatusId",
                principalTable: "ThingStatus",
                principalColumn: "thingStatusId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

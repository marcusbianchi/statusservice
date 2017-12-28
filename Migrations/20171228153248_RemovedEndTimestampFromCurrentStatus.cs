using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace statusservice.Migrations
{
    public partial class RemovedEndTimestampFromCurrentStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "endTimestampTicks",
                table: "ContextStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "endTimestampTicks",
                table: "ContextStatus",
                nullable: false,
                defaultValue: 0L);
        }
    }
}

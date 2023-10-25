using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class learn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "ApplicationUsers",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "Password" },
                values: new object[] { new DateTime(2023, 8, 24, 16, 58, 22, 640, DateTimeKind.Local).AddTicks(5277), "$2a$11$rJqQ.ew0xFFKd.LQAqm4o.CrGHzHP7xSDaagArqD5DVR58yd8RBU." });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Gender",
                keyValue: null,
                column: "Gender",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "ApplicationUsers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "Password" },
                values: new object[] { new DateTime(2023, 8, 20, 8, 39, 34, 121, DateTimeKind.Local).AddTicks(3077), "$2a$11$4xhp.w9ylXJb9yqInmNIfuLYb3sy5cimp7cl7S67B0/tntQRiwIGC" });
        }
    }
}

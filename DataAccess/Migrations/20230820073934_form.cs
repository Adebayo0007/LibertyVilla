using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class form : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "RoomOrderDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "Password" },
                values: new object[] { new DateTime(2023, 8, 20, 8, 39, 34, 121, DateTimeKind.Local).AddTicks(3077), "$2a$11$4xhp.w9ylXJb9yqInmNIfuLYb3sy5cimp7cl7S67B0/tntQRiwIGC" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "RoomOrderDetails",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "Password" },
                values: new object[] { new DateTime(2023, 8, 18, 23, 40, 33, 867, DateTimeKind.Local).AddTicks(6780), "$2a$11$04Xe5zKbL6BhrDmIkgE8JOLAdaMQ78MG0vicy0O92nz/Zx5Fzf.8K" });
        }
    }
}

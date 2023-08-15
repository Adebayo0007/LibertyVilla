using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "ApplicationUsers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "Gender", "Password" },
                values: new object[] { new DateTime(2023, 8, 9, 7, 1, 15, 135, DateTimeKind.Local).AddTicks(5025), "Male", "$2a$11$f0nU3w9cqx6ETU7/LHy9rOQz1qnyESu8bB42hohRRfh7pQhZjpeJ." });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "ApplicationUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "Gender", "Password" },
                values: new object[] { new DateTime(2023, 8, 9, 6, 52, 15, 345, DateTimeKind.Local).AddTicks(6630), 1, "$2a$11$x3UrRB91M1Z8cvBwpGs2/Oh88HMNyJdn10JBWxYB.B.E9bBQJErgu" });
        }
    }
}

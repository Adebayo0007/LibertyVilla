using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class addingRoomImageUrlModel1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelRoomImages_HotelRooms_HotelRoomId",
                table: "HotelRoomImages");

            migrationBuilder.DropIndex(
                name: "IX_HotelRoomImages_HotelRoomId",
                table: "HotelRoomImages");

            migrationBuilder.DropColumn(
                name: "HotelRoomId",
                table: "HotelRoomImages");

            migrationBuilder.CreateIndex(
                name: "IX_HotelRoomImages_RoomId",
                table: "HotelRoomImages",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelRoomImages_HotelRooms_RoomId",
                table: "HotelRoomImages",
                column: "RoomId",
                principalTable: "HotelRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelRoomImages_HotelRooms_RoomId",
                table: "HotelRoomImages");

            migrationBuilder.DropIndex(
                name: "IX_HotelRoomImages_RoomId",
                table: "HotelRoomImages");

            migrationBuilder.AddColumn<int>(
                name: "HotelRoomId",
                table: "HotelRoomImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_HotelRoomImages_HotelRoomId",
                table: "HotelRoomImages",
                column: "HotelRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelRoomImages_HotelRooms_HotelRoomId",
                table: "HotelRoomImages",
                column: "HotelRoomId",
                principalTable: "HotelRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

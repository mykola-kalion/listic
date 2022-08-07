using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Listonic.Migrations
{
    public partial class user_list_relatives : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ListItems",
                keyColumns: new[] { "ItemId", "ListId" },
                keyValues: new object[] { 200, 100 });

            migrationBuilder.DeleteData(
                table: "Lists",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "Lists",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DropColumn(
                name: "Owner",
                table: "Lists");

            migrationBuilder.CreateTable(
                name: "UsersLists",
                columns: table => new
                {
                    OwnerId = table.Column<string>(type: "TEXT", nullable: false),
                    ListId = table.Column<int>(type: "INTEGER", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersLists", x => new { x.ListId, x.OwnerId });
                    table.ForeignKey(
                        name: "FK_UsersLists_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersLists_Lists_ListId",
                        column: x => x.ListId,
                        principalTable: "Lists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersLists_OwnerId",
                table: "UsersLists",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersLists");

            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "Lists",
                type: "TEXT",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Name" },
                values: new object[] { 200, "Carrot" });

            migrationBuilder.InsertData(
                table: "Lists",
                columns: new[] { "Id", "Name", "Owner" },
                values: new object[] { 100, "My first list", null });

            migrationBuilder.InsertData(
                table: "Lists",
                columns: new[] { "Id", "Name", "Owner" },
                values: new object[] { 101, "My second list", null });

            migrationBuilder.InsertData(
                table: "ListItems",
                columns: new[] { "ItemId", "ListId", "Id", "Quantity" },
                values: new object[] { 200, 100, 1, 5 });
        }
    }
}

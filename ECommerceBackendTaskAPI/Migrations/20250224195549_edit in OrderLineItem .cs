using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceBackendTaskAPI.Migrations
{
    /// <inheritdoc />
    public partial class editinOrderLineItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderLineItems_Orders_OrderID",
                table: "OrderLineItems");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "OrderLineItems");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "OrderLineItems");

            migrationBuilder.RenameColumn(
                name: "OrderID",
                table: "OrderLineItems",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderLineItems_OrderID",
                table: "OrderLineItems",
                newName: "IX_OrderLineItems_OrderId");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "OrderLineItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLineItems_Orders_OrderId",
                table: "OrderLineItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderLineItems_Orders_OrderId",
                table: "OrderLineItems");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrderLineItems",
                newName: "OrderID");

            migrationBuilder.RenameIndex(
                name: "IX_OrderLineItems_OrderId",
                table: "OrderLineItems",
                newName: "IX_OrderLineItems_OrderID");

            migrationBuilder.AlterColumn<int>(
                name: "OrderID",
                table: "OrderLineItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "OrderLineItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "OrderLineItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLineItems_Orders_OrderID",
                table: "OrderLineItems",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "ID");
        }
    }
}

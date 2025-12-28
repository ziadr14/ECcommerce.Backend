using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECom.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixShippingAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShippingAddresses_Orders_OrderId",
                table: "ShippingAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShippingAddresses",
                table: "ShippingAddresses");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "ShippingAddresses");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ShippingAddresses",
                newName: "OrdersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShippingAddresses",
                table: "ShippingAddresses",
                column: "OrdersId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingAddresses_Orders_OrdersId",
                table: "ShippingAddresses",
                column: "OrdersId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShippingAddresses_Orders_OrdersId",
                table: "ShippingAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShippingAddresses",
                table: "ShippingAddresses");

            migrationBuilder.RenameColumn(
                name: "OrdersId",
                table: "ShippingAddresses",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "ShippingAddresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShippingAddresses",
                table: "ShippingAddresses",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingAddresses_Orders_OrderId",
                table: "ShippingAddresses",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

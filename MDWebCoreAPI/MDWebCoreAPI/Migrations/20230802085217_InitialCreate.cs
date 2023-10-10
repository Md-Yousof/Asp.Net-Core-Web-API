using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MDWebCoreAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderMasters",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    OrderDate = table.Column<DateTime>(nullable: true),
                    IsComplete = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderMasters", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    DetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.DetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetails_OrderMasters_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrderMasters",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "OrderMasters",
                columns: new[] { "OrderId", "CustomerName", "ImagePath", "IsComplete", "OrderDate" },
                values: new object[,]
                {
                    { 1, "John Doe", null, true, new DateTime(2023, 8, 2, 14, 52, 17, 74, DateTimeKind.Local).AddTicks(5669) },
                    { 2, "Jane Smith", null, false, new DateTime(2023, 8, 1, 14, 52, 17, 75, DateTimeKind.Local).AddTicks(3900) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Name" },
                values: new object[,]
                {
                    { 1, "Product 1" },
                    { 2, "Product 2" },
                    { 3, "Product 3" }
                });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "DetailId", "OrderId", "Price", "ProductId", "Quantity" },
                values: new object[] { 1, 1, 100m, 1, 1 });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "DetailId", "OrderId", "Price", "ProductId", "Quantity" },
                values: new object[] { 2, 1, 200m, 2, 2 });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "DetailId", "OrderId", "Price", "ProductId", "Quantity" },
                values: new object[] { 3, 2, 300m, 3, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "OrderMasters");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}

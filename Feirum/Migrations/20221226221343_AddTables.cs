using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feirum.Migrations
{
    public partial class AddTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);

                });

            migrationBuilder.CreateTable(
                name: "Fairs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    State = table.Column<bool>(nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fairs_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);

                    table.ForeignKey(
                        name: "FK_Fairs_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
               name: "Products",
               columns: table => new
               {
                   Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                   FairId = table.Column<int>(nullable: false),
                   Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                   Quantity = table.Column<int>(nullable: false),
                   UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                   Image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Products", x => x.Id);
                   table.ForeignKey(
                        name: "FK_Products_Fairs_FairId",
                        column: x => x.FairId,
                        principalTable: "Fairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
               });


            migrationBuilder.CreateTable(
                name: "FavoriteFair",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FairId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteFair", x => new { x.UserId, x.FairId });
                    table.ForeignKey(
                        name: "FK_FavoriteFair_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);

                    table.ForeignKey(
                        name: "FK_FavoriteFair_Fairs_FairId",
                        column: x => x.FairId,
                        principalTable: "Fairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "FairImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FairId = table.Column<int>(nullable: false),
                    Path = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FairImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FairImages_Fairs_FairId",
                        column: x => x.FairId,
                        principalTable: "Fairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                         .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(nullable: false),
                    Path = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryImages_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
               name: "IX_Orders_BuyerId",
               table: "Orders",
               column: "BuyerId");

            migrationBuilder.CreateIndex(
               name: "IX_Fairs_OwnerId",
               table: "Fairs",
               column: "OwnerId");

            migrationBuilder.CreateIndex(
               name: "IX_FairImages_FairId",
               table: "FairImages",
               column: "FairId");

            migrationBuilder.CreateIndex(
              name: "IX_CategoryImages_CategoryId",
              table: "CategoryImages",
              column: "CategoryId");

            migrationBuilder.CreateIndex(
               name: "IX_Products_FairId",
               table: "Products",
               column: "FairId");

            migrationBuilder.CreateIndex(
              name: "IX_OrderItems_OrderId",
              table: "OrderItems",
              column: "OrderId");

            migrationBuilder.CreateIndex(
             name: "IX_FavoriteFair_UserId",
             table: "FavoriteFair",
             column: "UserId");

            migrationBuilder.CreateIndex(
             name: "IX_FavoriteFair_FairId",
             table: "FavoriteFair",
             column: "FairId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
              name: "Categories");

            migrationBuilder.DropTable(
               name: "Products");

            migrationBuilder.DropTable(
                name: "Fairs");

            migrationBuilder.DropTable(
              name: "FavoriteFair");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "FairImages");

            migrationBuilder.DropTable(
                name: "CategoryImages");
        }
    }
}
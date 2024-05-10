using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class ProductSpecificationNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsSpecifications_Products_ProductId",
                table: "ProductsSpecifications");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsSpecifications_Specifications_SpecificationId",
                table: "ProductsSpecifications");

            migrationBuilder.DropIndex(
                name: "IX_ProductsSpecifications_ProductId",
                table: "ProductsSpecifications");

            migrationBuilder.DropIndex(
                name: "IX_ProductsSpecifications_SpecificationId",
                table: "ProductsSpecifications");

            migrationBuilder.CreateTable(
                name: "ProductSpecification",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    SpecificationsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSpecification", x => new { x.ProductsId, x.SpecificationsId });
                    table.ForeignKey(
                        name: "FK_ProductSpecification_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProductSpecification_Specifications_SpecificationsId",
                        column: x => x.SpecificationsId,
                        principalTable: "Specifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductSpecification_SpecificationsId",
                table: "ProductSpecification",
                column: "SpecificationsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductSpecification");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsSpecifications_ProductId",
                table: "ProductsSpecifications",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsSpecifications_SpecificationId",
                table: "ProductsSpecifications",
                column: "SpecificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsSpecifications_Products_ProductId",
                table: "ProductsSpecifications",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsSpecifications_Specifications_SpecificationId",
                table: "ProductsSpecifications",
                column: "SpecificationId",
                principalTable: "Specifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

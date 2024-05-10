using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class addProductSpecRepos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSpecification_Products_ProductId",
                table: "ProductSpecification");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSpecification_Specifications_SpecificationId",
                table: "ProductSpecification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSpecification",
                table: "ProductSpecification");

            migrationBuilder.RenameTable(
                name: "ProductSpecification",
                newName: "ProductsSpecifications");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSpecification_SpecificationId",
                table: "ProductsSpecifications",
                newName: "IX_ProductsSpecifications_SpecificationId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProductsSpecifications",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ProductsSpecifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsSpecifications",
                table: "ProductsSpecifications",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsSpecifications_ProductId",
                table: "ProductsSpecifications",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsSpecifications_Products_ProductId",
                table: "ProductsSpecifications",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsSpecifications_Specifications_SpecificationId",
                table: "ProductsSpecifications",
                column: "SpecificationId",
                principalTable: "Specifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsSpecifications_Products_ProductId",
                table: "ProductsSpecifications");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsSpecifications_Specifications_SpecificationId",
                table: "ProductsSpecifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsSpecifications",
                table: "ProductsSpecifications");

            migrationBuilder.DropIndex(
                name: "IX_ProductsSpecifications_ProductId",
                table: "ProductsSpecifications");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductsSpecifications");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ProductsSpecifications");

            migrationBuilder.RenameTable(
                name: "ProductsSpecifications",
                newName: "ProductSpecification");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsSpecifications_SpecificationId",
                table: "ProductSpecification",
                newName: "IX_ProductSpecification_SpecificationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSpecification",
                table: "ProductSpecification",
                columns: new[] { "ProductId", "SpecificationId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSpecification_Products_ProductId",
                table: "ProductSpecification",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSpecification_Specifications_SpecificationId",
                table: "ProductSpecification",
                column: "SpecificationId",
                principalTable: "Specifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

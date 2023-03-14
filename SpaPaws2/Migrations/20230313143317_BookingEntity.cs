using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpaPaws2.Migrations
{
    /// <inheritdoc />
    public partial class BookingEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_AnimalId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Animals");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AnimalId",
                table: "Customers",
                column: "AnimalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_AnimalId",
                table: "Customers");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Animals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AnimalId",
                table: "Customers",
                column: "AnimalId",
                unique: true);
        }
    }
}

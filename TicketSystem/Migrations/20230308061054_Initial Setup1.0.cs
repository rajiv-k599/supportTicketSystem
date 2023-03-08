using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialSetup10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CustomerId",
                table: "CustomerContacts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContacts_CustomerId",
                table: "CustomerContacts",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerContacts_Customers_CustomerId",
                table: "CustomerContacts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerContacts_Customers_CustomerId",
                table: "CustomerContacts");

            migrationBuilder.DropIndex(
                name: "IX_CustomerContacts_CustomerId",
                table: "CustomerContacts");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "CustomerContacts");
        }
    }
}

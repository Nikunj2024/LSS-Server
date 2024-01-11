using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LSS.Migrations
{
    /// <inheritdoc />
    public partial class Create1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Loans_loan_number",
                table: "Loans",
                column: "loan_number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Loans_loan_number",
                table: "Loans");
        }
    }
}

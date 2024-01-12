using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LSS.Migrations
{
    /// <inheritdoc />
    public partial class InitailCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    loan_number = table.Column<int>(type: "integer", nullable: false),
                    note_date = table.Column<DateOnly>(type: "date", nullable: false),
                    note_rate = table.Column<float>(type: "real", nullable: false),
                    boarding_date = table.Column<DateOnly>(type: "date", nullable: false),
                    upb_amount = table.Column<float>(type: "real", nullable: false),
                    current_rate = table.Column<float>(type: "real", nullable: false),
                    pmt_due_date = table.Column<DateOnly>(type: "date", nullable: false),
                    principal_intrest = table.Column<float>(type: "real", nullable: false),
                    tax_insurance = table.Column<float>(type: "real", nullable: false),
                    pmt_amount = table.Column<float>(type: "real", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    ppr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Loans_loan_number",
                table: "Loans",
                column: "loan_number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loans");
        }
    }
}

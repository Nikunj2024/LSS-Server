using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LSS.Migrations
{
    /// <inheritdoc />
<<<<<<<< HEAD:Migrations/20240111114326_InitailCreate.cs
    public partial class InitailCreate : Migration
========
    public partial class initialCreate : Migration
>>>>>>>> 76107112be05df5fd5c78981b9971ff31dce46d4:Migrations/20240112080608_initialCreate.cs
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    boarding_date = table.Column<DateOnly>(type: "date", nullable: false),
                    current_rate = table.Column<float>(type: "real", nullable: false),
                    loan_number = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    note_date = table.Column<DateOnly>(type: "date", nullable: false),
                    note_rate = table.Column<float>(type: "real", nullable: false),
                    pmt_amount = table.Column<float>(type: "real", nullable: false),
                    pmt_due_date = table.Column<DateOnly>(type: "date", nullable: false),
                    ppr = table.Column<string>(type: "text", nullable: true),
                    principal_intrest = table.Column<float>(type: "real", nullable: false),
                    tax_insurance = table.Column<float>(type: "real", nullable: false),
                    upb_amount = table.Column<float>(type: "real", nullable: false)
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

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceCorp.Data.Migrations
{
    /// <inheritdoc />
    public partial class birthname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BirthNumber",
                table: "Persons",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthNumber",
                table: "Persons");
        }
    }
}

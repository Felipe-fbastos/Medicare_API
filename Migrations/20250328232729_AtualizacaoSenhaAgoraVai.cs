using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medicare_API.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoSenhaAgoraVai : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "SenhaSalt",
                table: "Utilizadores",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SenhaSalt",
                table: "Utilizadores");
        }
    }
}

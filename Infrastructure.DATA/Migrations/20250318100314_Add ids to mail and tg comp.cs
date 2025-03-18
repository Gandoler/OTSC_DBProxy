using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addidstomailandtgcomp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tg_comprehensions_appid",
                table: "tg_comprehensions");

            migrationBuilder.DropIndex(
                name: "IX_mail_comprehensions_appid",
                table: "mail_comprehensions");

            migrationBuilder.AlterColumn<long>(
                name: "tg_id",
                table: "tg_comprehensions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "tg_comprehensions_pkey",
                table: "tg_comprehensions",
                columns: new[] { "appid", "tg_id" });

            migrationBuilder.AddPrimaryKey(
                name: "mail_comprehensions_pkey",
                table: "mail_comprehensions",
                columns: new[] { "appid", "mail" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "tg_comprehensions_pkey",
                table: "tg_comprehensions");

            migrationBuilder.DropPrimaryKey(
                name: "mail_comprehensions_pkey",
                table: "mail_comprehensions");

            migrationBuilder.AlterColumn<long>(
                name: "tg_id",
                table: "tg_comprehensions",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_tg_comprehensions_appid",
                table: "tg_comprehensions",
                column: "appid");

            migrationBuilder.CreateIndex(
                name: "IX_mail_comprehensions_appid",
                table: "mail_comprehensions",
                column: "appid");
        }
    }
}

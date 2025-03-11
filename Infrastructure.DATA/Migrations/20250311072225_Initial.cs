using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pozdrik",
                columns: table => new
                {
                    id_pozdr = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    interest = table.Column<string>(type: "text", nullable: true),
                    pozhelanie = table.Column<string>(type: "text", nullable: true),
                    textpozdr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pozdrik_pkey", x => x.id_pozdr);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    appid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    login = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("users_pkey", x => x.appid);
                });

            migrationBuilder.CreateTable(
                name: "friend_list",
                columns: table => new
                {
                    appid = table.Column<Guid>(type: "uuid", nullable: false),
                    friend_username = table.Column<string>(type: "text", nullable: true),
                    friend_name = table.Column<string>(type: "text", nullable: false),
                    date_birth = table.Column<DateOnly>(type: "date", nullable: false),
                    id_pozdr = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "friend_list_appid_fkey",
                        column: x => x.appid,
                        principalTable: "users",
                        principalColumn: "appid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "friend_list_id_pozdr_fkey",
                        column: x => x.id_pozdr,
                        principalTable: "pozdrik",
                        principalColumn: "id_pozdr",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "mail_comprehensions",
                columns: table => new
                {
                    appid = table.Column<Guid>(type: "uuid", nullable: false),
                    mail = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "mail_comprehensions_appid_fkey",
                        column: x => x.appid,
                        principalTable: "users",
                        principalColumn: "appid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tg_comprehensions",
                columns: table => new
                {
                    appid = table.Column<Guid>(type: "uuid", nullable: false),
                    tg_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "tg_comprehensions_appid_fkey",
                        column: x => x.appid,
                        principalTable: "users",
                        principalColumn: "appid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_friend_list_appid",
                table: "friend_list",
                column: "appid");

            migrationBuilder.CreateIndex(
                name: "IX_friend_list_id_pozdr",
                table: "friend_list",
                column: "id_pozdr");

            migrationBuilder.CreateIndex(
                name: "IX_mail_comprehensions_appid",
                table: "mail_comprehensions",
                column: "appid");

            migrationBuilder.CreateIndex(
                name: "mail_comprehensions_mail_key",
                table: "mail_comprehensions",
                column: "mail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tg_comprehensions_appid",
                table: "tg_comprehensions",
                column: "appid");

            migrationBuilder.CreateIndex(
                name: "tg_comprehensions_tg_id_key",
                table: "tg_comprehensions",
                column: "tg_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "users_login_key",
                table: "users",
                column: "login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "friend_list");

            migrationBuilder.DropTable(
                name: "mail_comprehensions");

            migrationBuilder.DropTable(
                name: "tg_comprehensions");

            migrationBuilder.DropTable(
                name: "pozdrik");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}

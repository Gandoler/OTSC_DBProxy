// <copyright file="20250311073826_add comb key for friend list.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#nullable disable

namespace Infrastructure.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    /// <inheritdoc />
    public partial class Addcombkeyforfriendlist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_friend_list_appid",
                table: "friend_list");

            migrationBuilder.AlterColumn<string>(
                name: "friend_username",
                table: "friend_list",
                type: "text",
                nullable: false,
                defaultValue: string.Empty,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "friend_list_pkey",
                table: "friend_list",
                columns: new[] { "appid", "friend_username" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "friend_list_pkey",
                table: "friend_list");

            migrationBuilder.AlterColumn<string>(
                name: "friend_username",
                table: "friend_list",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_friend_list_appid",
                table: "friend_list",
                column: "appid");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mottu.RentalApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Foreingkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rentals_motorcycles_MotorcycleId",
                table: "rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_rentals_riders_RiderId",
                table: "rentals");

            migrationBuilder.AddColumn<string>(
                name: "Identifier",
                table: "riders",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "RiderId",
                table: "rentals",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "MotorcycleId",
                table: "rentals",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "Identifier",
                table: "motorcycles",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_riders_Identifier",
                table: "riders",
                column: "Identifier");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_motorcycles_Identifier",
                table: "motorcycles",
                column: "Identifier");

            migrationBuilder.AddForeignKey(
                name: "FK_rentals_motorcycles_MotorcycleId",
                table: "rentals",
                column: "MotorcycleId",
                principalTable: "motorcycles",
                principalColumn: "Identifier",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_rentals_riders_RiderId",
                table: "rentals",
                column: "RiderId",
                principalTable: "riders",
                principalColumn: "Identifier",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rentals_motorcycles_MotorcycleId",
                table: "rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_rentals_riders_RiderId",
                table: "rentals");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_riders_Identifier",
                table: "riders");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_motorcycles_Identifier",
                table: "motorcycles");

            migrationBuilder.DropColumn(
                name: "Identifier",
                table: "riders");

            migrationBuilder.AlterColumn<Guid>(
                name: "RiderId",
                table: "rentals",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<Guid>(
                name: "MotorcycleId",
                table: "rentals",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "Identifier",
                table: "motorcycles",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(64)",
                oldMaxLength: 64);

            migrationBuilder.AddForeignKey(
                name: "FK_rentals_motorcycles_MotorcycleId",
                table: "rentals",
                column: "MotorcycleId",
                principalTable: "motorcycles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_rentals_riders_RiderId",
                table: "rentals",
                column: "RiderId",
                principalTable: "riders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

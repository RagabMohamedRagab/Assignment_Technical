using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSomeColumnsInProductAndModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreateOn",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "UpdateBy",
                table: "Products",
                newName: "Date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Products",
                newName: "UpdateBy");

            migrationBuilder.AddColumn<Guid>(
                name: "CreateBy",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateOn",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

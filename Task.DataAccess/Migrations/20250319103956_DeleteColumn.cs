using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DeleteColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Date",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}

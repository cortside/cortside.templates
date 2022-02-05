using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acme.WebApiStarter.Data.Migrations
{
    public partial class Orders3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreateSubjectId",
                schema: "dbo",
                table: "Address",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "dbo",
                table: "Address",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                schema: "dbo",
                table: "Address",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifiedSubjectId",
                schema: "dbo",
                table: "Address",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Address_CreateSubjectId",
                schema: "dbo",
                table: "Address",
                column: "CreateSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_LastModifiedSubjectId",
                schema: "dbo",
                table: "Address",
                column: "LastModifiedSubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Subject_CreateSubjectId",
                schema: "dbo",
                table: "Address",
                column: "CreateSubjectId",
                principalSchema: "dbo",
                principalTable: "Subject",
                principalColumn: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Subject_LastModifiedSubjectId",
                schema: "dbo",
                table: "Address",
                column: "LastModifiedSubjectId",
                principalSchema: "dbo",
                principalTable: "Subject",
                principalColumn: "SubjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Subject_CreateSubjectId",
                schema: "dbo",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_Subject_LastModifiedSubjectId",
                schema: "dbo",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_CreateSubjectId",
                schema: "dbo",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_LastModifiedSubjectId",
                schema: "dbo",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "CreateSubjectId",
                schema: "dbo",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "dbo",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                schema: "dbo",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "LastModifiedSubjectId",
                schema: "dbo",
                table: "Address");
        }
    }
}

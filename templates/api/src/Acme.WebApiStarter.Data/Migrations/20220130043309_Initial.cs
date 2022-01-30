using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acme.WebApiStarter.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Outbox",
                schema: "dbo",
                columns: table => new
                {
                    OutboxId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    CorrelationId = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    EventType = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Topic = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RoutingKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScheduledDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LockId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Outbox", x => x.OutboxId);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                schema: "dbo",
                columns: table => new
                {
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GivenName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FamilyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserPrincipalName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.SubjectId);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "dbo",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateSubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedSubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customer_Subject_CreateSubjectId",
                        column: x => x.CreateSubjectId,
                        principalSchema: "dbo",
                        principalTable: "Subject",
                        principalColumn: "SubjectId");
                    table.ForeignKey(
                        name: "FK_Customer_Subject_LastModifiedSubjectId",
                        column: x => x.LastModifiedSubjectId,
                        principalSchema: "dbo",
                        principalTable: "Subject",
                        principalColumn: "SubjectId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CreateSubjectId",
                schema: "dbo",
                table: "Customer",
                column: "CreateSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_LastModifiedSubjectId",
                schema: "dbo",
                table: "Customer",
                column: "LastModifiedSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Outbox_MessageId",
                schema: "dbo",
                table: "Outbox",
                column: "MessageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleDate_Status",
                schema: "dbo",
                table: "Outbox",
                columns: new[] { "ScheduledDate", "Status" })
                .Annotation("SqlServer:Include", new[] { "EventType" });

            migrationBuilder.CreateIndex(
                name: "IX_Status_LockId_ScheduleDate",
                schema: "dbo",
                table: "Outbox",
                columns: new[] { "Status", "LockId", "ScheduledDate" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Outbox",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Subject",
                schema: "dbo");
        }
    }
}

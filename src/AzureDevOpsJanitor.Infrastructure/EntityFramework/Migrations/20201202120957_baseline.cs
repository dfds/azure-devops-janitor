using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AzureDevOpsJanitor.Infrastructure.EntityFramework.Migrations
{
    public partial class baseline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuildDefinition",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    DefinitionId = table.Column<int>(nullable: false),
                    Yaml = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildDefinition", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "BuildStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Build",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DefinitionName = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Build", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Build_BuildDefinition_DefinitionName",
                        column: x => x.DefinitionName,
                        principalTable: "BuildDefinition",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Build_BuildStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "BuildStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Build_DefinitionName",
                table: "Build",
                column: "DefinitionName");

            migrationBuilder.CreateIndex(
                name: "IX_Build_StatusId",
                table: "Build",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Build");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "BuildDefinition");

            migrationBuilder.DropTable(
                name: "BuildStatus");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AzureDevOpsJanitor.Infrastructure.EntityFramework.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artifact",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artifact", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "BuildDefinition",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Yaml = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildDefinition", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "BuildStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Environment",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Environment", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Release",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Release", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Build",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CapabilityIdentifier = table.Column<string>(type: "TEXT", nullable: true),
                    ProjectId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DefinitionName = table.Column<string>(type: "TEXT", nullable: true),
                    StatusId = table.Column<int>(type: "INTEGER", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "ArtifactReleaseRoot",
                columns: table => new
                {
                    ArtifactsId = table.Column<int>(type: "INTEGER", nullable: false),
                    ArtifactsName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtifactReleaseRoot", x => new { x.ArtifactsId, x.ArtifactsName });
                    table.ForeignKey(
                        name: "FK_ArtifactReleaseRoot_Artifact_ArtifactsName",
                        column: x => x.ArtifactsName,
                        principalTable: "Artifact",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtifactReleaseRoot_Release_ArtifactsId",
                        column: x => x.ArtifactsId,
                        principalTable: "Release",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuildEnvironmentReleaseRoot",
                columns: table => new
                {
                    EnvironmentsId = table.Column<int>(type: "INTEGER", nullable: false),
                    EnvironmentsName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildEnvironmentReleaseRoot", x => new { x.EnvironmentsId, x.EnvironmentsName });
                    table.ForeignKey(
                        name: "FK_BuildEnvironmentReleaseRoot_Environment_EnvironmentsName",
                        column: x => x.EnvironmentsName,
                        principalTable: "Environment",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuildEnvironmentReleaseRoot_Release_EnvironmentsId",
                        column: x => x.EnvironmentsId,
                        principalTable: "Release",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    BuildRootId = table.Column<int>(type: "INTEGER", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => new { x.BuildRootId, x.Id });
                    table.ForeignKey(
                        name: "FK_Tag_Build_BuildRootId",
                        column: x => x.BuildRootId,
                        principalTable: "Build",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtifactReleaseRoot_ArtifactsName",
                table: "ArtifactReleaseRoot",
                column: "ArtifactsName");

            migrationBuilder.CreateIndex(
                name: "IX_Build_DefinitionName",
                table: "Build",
                column: "DefinitionName");

            migrationBuilder.CreateIndex(
                name: "IX_Build_StatusId",
                table: "Build",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildEnvironmentReleaseRoot_EnvironmentsName",
                table: "BuildEnvironmentReleaseRoot",
                column: "EnvironmentsName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtifactReleaseRoot");

            migrationBuilder.DropTable(
                name: "BuildEnvironmentReleaseRoot");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Artifact");

            migrationBuilder.DropTable(
                name: "Environment");

            migrationBuilder.DropTable(
                name: "Release");

            migrationBuilder.DropTable(
                name: "Build");

            migrationBuilder.DropTable(
                name: "BuildDefinition");

            migrationBuilder.DropTable(
                name: "BuildStatus");
        }
    }
}

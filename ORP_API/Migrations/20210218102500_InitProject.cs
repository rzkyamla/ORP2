using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ORP_API.Migrations
{
    public partial class InitProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_m_customer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_overtime_form",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    SubmissionDate = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_overtime_form", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_role",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Details",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    Activity = table.Column<string>(maxLength: 100, nullable: false),
                    AdditionalSalary = table.Column<int>(nullable: false),
                    OvertimeFormId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Details_tb_m_overtime_form_OvertimeFormId",
                        column: x => x.OvertimeFormId,
                        principalTable: "tb_m_overtime_form",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_employee",
                columns: table => new
                {
                    NIK = table.Column<string>(maxLength: 10, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    BirthDate = table.Column<string>(nullable: false),
                    Gender = table.Column<string>(nullable: false),
                    Religion = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 12, nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_employee", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_tb_m_employee_tb_m_customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "tb_m_customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_m_employee_tb_m_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tb_m_role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_account",
                columns: table => new
                {
                    NIK = table.Column<string>(maxLength: 10, nullable: false),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_account", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_tb_m_account_tb_m_employee_NIK",
                        column: x => x.NIK,
                        principalTable: "tb_m_employee",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_overtime_form_employee",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    NIK = table.Column<string>(nullable: true),
                    OvertimeFormId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_overtime_form_employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_m_overtime_form_employee_tb_m_employee_NIK",
                        column: x => x.NIK,
                        principalTable: "tb_m_employee",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_m_overtime_form_employee_tb_m_overtime_form_OvertimeFormId",
                        column: x => x.OvertimeFormId,
                        principalTable: "tb_m_overtime_form",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Details_OvertimeFormId",
                table: "Details",
                column: "OvertimeFormId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_employee_CustomerId",
                table: "tb_m_employee",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_employee_RoleId",
                table: "tb_m_employee",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_overtime_form_employee_NIK",
                table: "tb_m_overtime_form_employee",
                column: "NIK");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_overtime_form_employee_OvertimeFormId",
                table: "tb_m_overtime_form_employee",
                column: "OvertimeFormId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Details");

            migrationBuilder.DropTable(
                name: "tb_m_account");

            migrationBuilder.DropTable(
                name: "tb_m_overtime_form_employee");

            migrationBuilder.DropTable(
                name: "tb_m_employee");

            migrationBuilder.DropTable(
                name: "tb_m_overtime_form");

            migrationBuilder.DropTable(
                name: "tb_m_customer");

            migrationBuilder.DropTable(
                name: "tb_m_role");
        }
    }
}

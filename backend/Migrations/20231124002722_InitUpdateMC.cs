using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FLCManagementProject.Migrations
{
    /// <inheritdoc />
    public partial class InitUpdateMC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "customerPhone",
                table: "Customers",
                newName: "UserPhone");

            migrationBuilder.RenameColumn(
                name: "customerLName",
                table: "Customers",
                newName: "UserLName");

            migrationBuilder.RenameColumn(
                name: "customerFName",
                table: "Customers",
                newName: "UserFName");

            migrationBuilder.RenameColumn(
                name: "customerEmail",
                table: "Customers",
                newName: "UserEmail");

            migrationBuilder.RenameColumn(
                name: "customerDob",
                table: "Customers",
                newName: "UserDob");

            migrationBuilder.CreateTable(
                name: "BillCusClasses",
                columns: table => new
                {
                    billCusClassID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    classID = table.Column<int>(type: "int", nullable: false),
                    customerID = table.Column<int>(type: "int", nullable: false),
                    billCusClassDayCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    billCusClassPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillCusClasses", x => x.billCusClassID);
                    table.ForeignKey(
                        name: "FK_BillCusClasses_Classes_classID",
                        column: x => x.classID,
                        principalTable: "Classes",
                        principalColumn: "classID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillCusClasses_Customers_customerID",
                        column: x => x.customerID,
                        principalTable: "Customers",
                        principalColumn: "customerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamBatches",
                columns: table => new
                {
                    examBatchID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    examID = table.Column<int>(type: "int", nullable: false),
                    examBatchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    examBatchStatus = table.Column<bool>(type: "bit", nullable: false),
                    ExamTypeexamtID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamBatches", x => x.examBatchID);
                    table.ForeignKey(
                        name: "FK_ExamBatches_ExamTypes_ExamTypeexamtID",
                        column: x => x.ExamTypeexamtID,
                        principalTable: "ExamTypes",
                        principalColumn: "examtID");
                    table.ForeignKey(
                        name: "FK_ExamBatches_Exams_examID",
                        column: x => x.examID,
                        principalTable: "Exams",
                        principalColumn: "examID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClipBoard",
                columns: table => new
                {
                    clipBoardID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customerID = table.Column<int>(type: "int", nullable: false),
                    examBatchID = table.Column<int>(type: "int", nullable: false),
                    examTypeID = table.Column<int>(type: "int", nullable: false),
                    clipBoardPoint = table.Column<float>(type: "real", nullable: false),
                    clipBoardStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClipBoard", x => x.clipBoardID);
                    table.ForeignKey(
                        name: "FK_ClipBoard_Customers_customerID",
                        column: x => x.customerID,
                        principalTable: "Customers",
                        principalColumn: "customerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClipBoard_ExamBatches_examBatchID",
                        column: x => x.examBatchID,
                        principalTable: "ExamBatches",
                        principalColumn: "examBatchID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClipBoard_ExamTypes_examTypeID",
                        column: x => x.examTypeID,
                        principalTable: "ExamTypes",
                        principalColumn: "examtID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillCusClasses_classID",
                table: "BillCusClasses",
                column: "classID");

            migrationBuilder.CreateIndex(
                name: "IX_BillCusClasses_customerID",
                table: "BillCusClasses",
                column: "customerID");

            migrationBuilder.CreateIndex(
                name: "IX_ClipBoard_customerID",
                table: "ClipBoard",
                column: "customerID");

            migrationBuilder.CreateIndex(
                name: "IX_ClipBoard_examBatchID",
                table: "ClipBoard",
                column: "examBatchID");

            migrationBuilder.CreateIndex(
                name: "IX_ClipBoard_examTypeID",
                table: "ClipBoard",
                column: "examTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamBatches_examID",
                table: "ExamBatches",
                column: "examID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamBatches_ExamTypeexamtID",
                table: "ExamBatches",
                column: "ExamTypeexamtID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillCusClasses");

            migrationBuilder.DropTable(
                name: "ClipBoard");

            migrationBuilder.DropTable(
                name: "ExamBatches");

            migrationBuilder.RenameColumn(
                name: "UserPhone",
                table: "Customers",
                newName: "customerPhone");

            migrationBuilder.RenameColumn(
                name: "UserLName",
                table: "Customers",
                newName: "customerLName");

            migrationBuilder.RenameColumn(
                name: "UserFName",
                table: "Customers",
                newName: "customerFName");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "Customers",
                newName: "customerEmail");

            migrationBuilder.RenameColumn(
                name: "UserDob",
                table: "Customers",
                newName: "customerDob");
        }
    }
}

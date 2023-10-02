using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FLCManagement.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    branchID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    branchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    branchAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.branchID);
                });

            migrationBuilder.CreateTable(
                name: "Certificates",
                columns: table => new
                {
                    certificateID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    certificateName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.certificateID);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    customerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customerFName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    customerLName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    customerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    customerPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    customerDob = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.customerID);
                });

            migrationBuilder.CreateTable(
                name: "ExamTypes",
                columns: table => new
                {
                    examtID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    examtName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamTypes", x => x.examtID);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    languageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    languageName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.languageID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    roleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    roleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.roleID);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    courseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    courseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    courseDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    coursePrice = table.Column<int>(type: "int", nullable: false),
                    certificateID = table.Column<int>(type: "int", nullable: false),
                    languageID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.courseID);
                    table.ForeignKey(
                        name: "FK_Courses_Certificates_certificateID",
                        column: x => x.certificateID,
                        principalTable: "Certificates",
                        principalColumn: "certificateID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_Languages_languageID",
                        column: x => x.languageID,
                        principalTable: "Languages",
                        principalColumn: "languageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    examID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    examName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    examDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    examPrice = table.Column<int>(type: "int", nullable: false),
                    certificateID = table.Column<int>(type: "int", nullable: false),
                    languageID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.examID);
                    table.ForeignKey(
                        name: "FK_Exams_Certificates_certificateID",
                        column: x => x.certificateID,
                        principalTable: "Certificates",
                        principalColumn: "certificateID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Exams_Languages_languageID",
                        column: x => x.languageID,
                        principalTable: "Languages",
                        principalColumn: "languageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    employeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employeeFName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    employeeLName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    employeeEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    employeePhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    employeePassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    roleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.employeeID);
                    table.ForeignKey(
                        name: "FK_Employees_Roles_roleID",
                        column: x => x.roleID,
                        principalTable: "Roles",
                        principalColumn: "roleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    classID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    className = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    classSize = table.Column<int>(type: "int", nullable: false),
                    classStartD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    classEndD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    classStatus = table.Column<bool>(type: "bit", nullable: false),
                    branchID = table.Column<int>(type: "int", nullable: false),
                    courseID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.classID);
                    table.ForeignKey(
                        name: "FK_Classes_Branches_branchID",
                        column: x => x.branchID,
                        principalTable: "Branches",
                        principalColumn: "branchID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Classes_Courses_courseID",
                        column: x => x.courseID,
                        principalTable: "Courses",
                        principalColumn: "courseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamExamTypes",
                columns: table => new
                {
                    examExamTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    examID = table.Column<int>(type: "int", nullable: false),
                    ExamTypeexamtID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamExamTypes", x => x.examExamTypeID);
                    table.ForeignKey(
                        name: "FK_ExamExamTypes_ExamTypes_ExamTypeexamtID",
                        column: x => x.ExamTypeexamtID,
                        principalTable: "ExamTypes",
                        principalColumn: "examtID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamExamTypes_Exams_examID",
                        column: x => x.examID,
                        principalTable: "Exams",
                        principalColumn: "examID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamSessions",
                columns: table => new
                {
                    examsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    examsName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    examsStatus = table.Column<bool>(type: "bit", nullable: false),
                    examID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamSessions", x => x.examsID);
                    table.ForeignKey(
                        name: "FK_ExamSessions_Exams_examID",
                        column: x => x.examID,
                        principalTable: "Exams",
                        principalColumn: "examID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    orderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    orderDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    orderTotal = table.Column<int>(type: "int", nullable: false),
                    orderCreatedAt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    courseID = table.Column<int>(type: "int", nullable: false),
                    examID = table.Column<int>(type: "int", nullable: false),
                    customerID = table.Column<int>(type: "int", nullable: false),
                    customerID1 = table.Column<int>(type: "int", nullable: true),
                    examID1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.orderID);
                    table.ForeignKey(
                        name: "FK_Orders_Courses_courseID",
                        column: x => x.courseID,
                        principalTable: "Courses",
                        principalColumn: "courseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_customerID",
                        column: x => x.customerID,
                        principalTable: "Customers",
                        principalColumn: "customerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_customerID1",
                        column: x => x.customerID1,
                        principalTable: "Customers",
                        principalColumn: "customerID");
                    table.ForeignKey(
                        name: "FK_Orders_Exams_examID",
                        column: x => x.examID,
                        principalTable: "Exams",
                        principalColumn: "examID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Exams_examID1",
                        column: x => x.examID1,
                        principalTable: "Exams",
                        principalColumn: "examID");
                });

            migrationBuilder.CreateTable(
                name: "ClassDetails",
                columns: table => new
                {
                    classdID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    classID = table.Column<int>(type: "int", nullable: false),
                    customerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassDetails", x => x.classdID);
                    table.ForeignKey(
                        name: "FK_ClassDetails_Classes_classID",
                        column: x => x.classID,
                        principalTable: "Classes",
                        principalColumn: "classID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassDetails_Customers_customerID",
                        column: x => x.customerID,
                        principalTable: "Customers",
                        principalColumn: "customerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    lessonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    lessonStartD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lessonEndD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lessonShift = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lessonStartH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lessonEndH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    classID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.lessonID);
                    table.ForeignKey(
                        name: "FK_Lessons_Classes_classID",
                        column: x => x.classID,
                        principalTable: "Classes",
                        principalColumn: "classID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamSessionResults",
                columns: table => new
                {
                    examsrID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    examsrResult = table.Column<bool>(type: "bit", nullable: false),
                    customerID = table.Column<int>(type: "int", nullable: false),
                    ExamSessionexamsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamSessionResults", x => x.examsrID);
                    table.ForeignKey(
                        name: "FK_ExamSessionResults_Customers_customerID",
                        column: x => x.customerID,
                        principalTable: "Customers",
                        principalColumn: "customerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamSessionResults_ExamSessions_ExamSessionexamsID",
                        column: x => x.ExamSessionexamsID,
                        principalTable: "ExamSessions",
                        principalColumn: "examsID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamShifts",
                columns: table => new
                {
                    examstID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    examstSize = table.Column<int>(type: "int", nullable: false),
                    examstStartH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    examstEndH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    exemstDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    branchID = table.Column<int>(type: "int", nullable: false),
                    ExamTypeexamtID = table.Column<int>(type: "int", nullable: false),
                    ExamSessionexamsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamShifts", x => x.examstID);
                    table.ForeignKey(
                        name: "FK_ExamShifts_Branches_branchID",
                        column: x => x.branchID,
                        principalTable: "Branches",
                        principalColumn: "branchID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamShifts_ExamSessions_ExamSessionexamsID",
                        column: x => x.ExamSessionexamsID,
                        principalTable: "ExamSessions",
                        principalColumn: "examsID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamShifts_ExamTypes_ExamTypeexamtID",
                        column: x => x.ExamTypeexamtID,
                        principalTable: "ExamTypes",
                        principalColumn: "examtID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerExamShifts",
                columns: table => new
                {
                    customeresID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customeresScore = table.Column<float>(type: "real", nullable: false),
                    customerID = table.Column<int>(type: "int", nullable: false),
                    ExamShiftexamstID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerExamShifts", x => x.customeresID);
                    table.ForeignKey(
                        name: "FK_CustomerExamShifts_Customers_customerID",
                        column: x => x.customerID,
                        principalTable: "Customers",
                        principalColumn: "customerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerExamShifts_ExamShifts_ExamShiftexamstID",
                        column: x => x.ExamShiftexamstID,
                        principalTable: "ExamShifts",
                        principalColumn: "examstID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassDetails_classID",
                table: "ClassDetails",
                column: "classID");

            migrationBuilder.CreateIndex(
                name: "IX_ClassDetails_customerID",
                table: "ClassDetails",
                column: "customerID");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_branchID",
                table: "Classes",
                column: "branchID");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_courseID",
                table: "Classes",
                column: "courseID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_certificateID",
                table: "Courses",
                column: "certificateID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_languageID",
                table: "Courses",
                column: "languageID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerExamShifts_customerID",
                table: "CustomerExamShifts",
                column: "customerID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerExamShifts_ExamShiftexamstID",
                table: "CustomerExamShifts",
                column: "ExamShiftexamstID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_roleID",
                table: "Employees",
                column: "roleID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamExamTypes_examID",
                table: "ExamExamTypes",
                column: "examID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamExamTypes_ExamTypeexamtID",
                table: "ExamExamTypes",
                column: "ExamTypeexamtID");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_certificateID",
                table: "Exams",
                column: "certificateID");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_languageID",
                table: "Exams",
                column: "languageID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSessionResults_customerID",
                table: "ExamSessionResults",
                column: "customerID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSessionResults_ExamSessionexamsID",
                table: "ExamSessionResults",
                column: "ExamSessionexamsID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSessions_examID",
                table: "ExamSessions",
                column: "examID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamShifts_branchID",
                table: "ExamShifts",
                column: "branchID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamShifts_ExamSessionexamsID",
                table: "ExamShifts",
                column: "ExamSessionexamsID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamShifts_ExamTypeexamtID",
                table: "ExamShifts",
                column: "ExamTypeexamtID");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_classID",
                table: "Lessons",
                column: "classID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_courseID",
                table: "Orders",
                column: "courseID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_customerID",
                table: "Orders",
                column: "customerID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_customerID1",
                table: "Orders",
                column: "customerID1");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_examID",
                table: "Orders",
                column: "examID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_examID1",
                table: "Orders",
                column: "examID1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassDetails");

            migrationBuilder.DropTable(
                name: "CustomerExamShifts");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "ExamExamTypes");

            migrationBuilder.DropTable(
                name: "ExamSessionResults");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ExamShifts");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "ExamSessions");

            migrationBuilder.DropTable(
                name: "ExamTypes");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "Certificates");

            migrationBuilder.DropTable(
                name: "Languages");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FLCManagementProject.Migrations
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
                name: "UserDtos",
                columns: table => new
                {
                    userFName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userLName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userDob = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userRole = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    courseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    languageID = table.Column<int>(type: "int", nullable: false),
                    certificateID = table.Column<int>(type: "int", nullable: false),
                    courseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    courseDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    coursePrice = table.Column<int>(type: "int", nullable: false)
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
                    languageID = table.Column<int>(type: "int", nullable: false),
                    certificateID = table.Column<int>(type: "int", nullable: false),
                    examName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    examDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    examPrice = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "Customers",
                columns: table => new
                {
                    customerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    roleID = table.Column<int>(type: "int", nullable: false),
                    customerFName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    customerLName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    customerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    customerPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    customerDob = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.customerID);
                    table.ForeignKey(
                        name: "FK_Customers_Roles_roleID",
                        column: x => x.roleID,
                        principalTable: "Roles",
                        principalColumn: "roleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    employeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    roleID = table.Column<int>(type: "int", nullable: false),
                    employeeFName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    employeeLName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    employeeEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    employeePhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    employeeDob = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    employeePassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserFName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserLName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserDob = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleID",
                        column: x => x.RoleID,
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
                    branchID = table.Column<int>(type: "int", nullable: false),
                    courseID = table.Column<int>(type: "int", nullable: false),
                    className = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    classSize = table.Column<int>(type: "int", nullable: false),
                    classStartD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    classEndD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    classStatus = table.Column<bool>(type: "bit", nullable: false)
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
                name: "ExamSessions",
                columns: table => new
                {
                    examsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    examID = table.Column<int>(type: "int", nullable: false),
                    examsName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    examsStatus = table.Column<bool>(type: "bit", nullable: false)
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
                    courseID = table.Column<int>(type: "int", nullable: false),
                    examID = table.Column<int>(type: "int", nullable: false),
                    customerID = table.Column<int>(type: "int", nullable: false),
                    orderDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    orderTotal = table.Column<int>(type: "int", nullable: false),
                    orderCreatedAt = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Exams_examID",
                        column: x => x.examID,
                        principalTable: "Exams",
                        principalColumn: "examID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClassDetail",
                columns: table => new
                {
                    classdID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    classID = table.Column<int>(type: "int", nullable: false),
                    customerID = table.Column<int>(type: "int", nullable: false),
                    dayCreate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassDetail", x => x.classdID);
                    table.ForeignKey(
                        name: "FK_ClassDetail_Classes_classID",
                        column: x => x.classID,
                        principalTable: "Classes",
                        principalColumn: "classID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassDetail_Customers_customerID",
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
                    classID = table.Column<int>(type: "int", nullable: false),
                    lessonStartD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lessonEndD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lessonShift = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lessonStartH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lessonEndH = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "ExamSessionResult",
                columns: table => new
                {
                    examsrID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customerID = table.Column<int>(type: "int", nullable: false),
                    examsID = table.Column<int>(type: "int", nullable: false),
                    examsrResult = table.Column<float>(type: "real", nullable: false),
                    ExamSessionexamsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamSessionResult", x => x.examsrID);
                    table.ForeignKey(
                        name: "FK_ExamSessionResult_Customers_customerID",
                        column: x => x.customerID,
                        principalTable: "Customers",
                        principalColumn: "customerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamSessionResult_ExamSessions_ExamSessionexamsID",
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
                    branchID = table.Column<int>(type: "int", nullable: false),
                    examtID = table.Column<int>(type: "int", nullable: false),
                    examsID = table.Column<int>(type: "int", nullable: false),
                    examstSize = table.Column<int>(type: "int", nullable: false),
                    examstStartH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    examstEndH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    examstDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "CustomerExamShift",
                columns: table => new
                {
                    customeresID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customerID = table.Column<int>(type: "int", nullable: false),
                    examssID = table.Column<int>(type: "int", nullable: false),
                    customeresScore = table.Column<float>(type: "real", nullable: false),
                    ExamShiftexamstID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerExamShift", x => x.customeresID);
                    table.ForeignKey(
                        name: "FK_CustomerExamShift_Customers_customerID",
                        column: x => x.customerID,
                        principalTable: "Customers",
                        principalColumn: "customerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerExamShift_ExamShifts_ExamShiftexamstID",
                        column: x => x.ExamShiftexamstID,
                        principalTable: "ExamShifts",
                        principalColumn: "examstID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassDetail_classID",
                table: "ClassDetail",
                column: "classID");

            migrationBuilder.CreateIndex(
                name: "IX_ClassDetail_customerID",
                table: "ClassDetail",
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
                name: "IX_CustomerExamShift_customerID",
                table: "CustomerExamShift",
                column: "customerID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerExamShift_ExamShiftexamstID",
                table: "CustomerExamShift",
                column: "ExamShiftexamstID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_roleID",
                table: "Customers",
                column: "roleID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_roleID",
                table: "Employees",
                column: "roleID");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_certificateID",
                table: "Exams",
                column: "certificateID");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_languageID",
                table: "Exams",
                column: "languageID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSessionResult_customerID",
                table: "ExamSessionResult",
                column: "customerID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSessionResult_ExamSessionexamsID",
                table: "ExamSessionResult",
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
                name: "IX_Orders_examID",
                table: "Orders",
                column: "examID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleID",
                table: "Users",
                column: "RoleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassDetail");

            migrationBuilder.DropTable(
                name: "CustomerExamShift");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "ExamSessionResult");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "UserDtos");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ExamShifts");

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
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "Certificates");

            migrationBuilder.DropTable(
                name: "Languages");
        }
    }
}

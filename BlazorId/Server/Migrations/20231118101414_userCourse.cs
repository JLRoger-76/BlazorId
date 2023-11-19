using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorId.Server.Migrations
{
    /// <inheritdoc />
    public partial class userCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserCourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Welcome = table.Column<int>(type: "int", nullable: true),
                    Enviromnent = table.Column<int>(type: "int", nullable: true),
                    Content = table.Column<int>(type: "int", nullable: true),
                    TeacherPedagogy = table.Column<int>(type: "int", nullable: true),
                    TeacherExpertise = table.Column<int>(type: "int", nullable: true),
                    TeacherAvailability = table.Column<int>(type: "int", nullable: true),
                    TeacherAnswers = table.Column<int>(type: "int", nullable: true),
                    TeacherAnimation = table.Column<int>(type: "int", nullable: true),
                    Satisfaction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Recommendation = table.Column<bool>(type: "bit", nullable: true),
                    CourseProject = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCourses_CourseId",
                table: "UserCourses",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCourses");
        }
    }
}

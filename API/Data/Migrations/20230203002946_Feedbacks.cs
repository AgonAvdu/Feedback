using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class Feedbacks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faculties_Cities_CityId",
                table: "Faculties");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1e30bdb0-ad54-4876-a971-65fad034f131");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3ba7c840-1126-43ff-aa29-bac78099a2a3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8234f8dd-8e1b-431e-992b-2d7736092bcb");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Faculties",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TeacherId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Feedbacks_AspNetUsers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Feedbacks_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1312bbb1-fdf3-4e66-ae8b-8f93d49e6a20", "ab8b454e-f555-4d1b-9ce3-0795135a7fbf", "Teacher", "TEACHER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1451033d-5c1b-406d-8981-bd0a11b144a1", "740ac2d2-3c7f-4e0d-88d0-bb0a6093fa3d", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9e807a4f-dfa0-40b6-acf2-0b5e9ac6df40", "8f9a0537-4a45-4b89-80a5-3ea6230aee99", "Student", "STUDENT" });

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_StudentId_SubjectId",
                table: "Feedbacks",
                columns: new[] { "StudentId", "SubjectId" },
                unique: true,
                filter: "[StudentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_SubjectId",
                table: "Feedbacks",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_TeacherId",
                table: "Feedbacks",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Faculties_Cities_CityId",
                table: "Faculties",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faculties_Cities_CityId",
                table: "Faculties");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1312bbb1-fdf3-4e66-ae8b-8f93d49e6a20");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1451033d-5c1b-406d-8981-bd0a11b144a1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9e807a4f-dfa0-40b6-acf2-0b5e9ac6df40");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Faculties",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1e30bdb0-ad54-4876-a971-65fad034f131", "452a0b5d-887f-48d4-8c2c-37f3b08a8da0", "Teacher", "TEACHER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3ba7c840-1126-43ff-aa29-bac78099a2a3", "6ec4c287-e363-4b18-b4f3-772e9aab75f2", "Student", "STUDENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8234f8dd-8e1b-431e-992b-2d7736092bcb", "63aa33e2-3905-496f-ad8c-821c22bd0f4f", "Admin", "ADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK_Faculties_Cities_CityId",
                table: "Faculties",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");
        }
    }
}

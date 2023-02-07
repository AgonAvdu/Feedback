using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class FeedbacksScore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<float>(
                name: "SentimentScore",
                table: "Feedbacks",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1cc9d644-d282-43d9-a0b9-44a2424c6596", "a23371ea-aaea-47ea-93df-e3d6545c5384", "Teacher", "TEACHER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "407314bb-e710-482c-9a8e-5c6a46a6802b", "94aae382-2ce9-4522-9d3f-58099e72d13d", "Student", "STUDENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f22b984b-0bf9-4767-b3ac-093999f272c7", "0d10d115-2fff-4510-a63f-f6f060b0b463", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1cc9d644-d282-43d9-a0b9-44a2424c6596");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "407314bb-e710-482c-9a8e-5c6a46a6802b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f22b984b-0bf9-4767-b3ac-093999f272c7");

            migrationBuilder.DropColumn(
                name: "SentimentScore",
                table: "Feedbacks");

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
        }
    }
}

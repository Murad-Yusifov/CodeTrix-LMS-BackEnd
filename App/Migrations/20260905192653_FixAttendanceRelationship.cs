using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Migrations
{
    /// <inheritdoc />
    public partial class FixAttendanceRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttendances_Users_StudentUserId",
                table: "StudentAttendances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentAttendances",
                table: "StudentAttendances");

            migrationBuilder.DropIndex(
                name: "IX_StudentAttendances_StudentUserId",
                table: "StudentAttendances");

            migrationBuilder.DropColumn(
                name: "StudentUserId",
                table: "StudentAttendances");

            migrationBuilder.AlterColumn<int>(
                name: "AttendanceId",
                table: "StudentAttendances",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "StudentAttendances",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentAttendances",
                table: "StudentAttendances",
                column: "AttendanceId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAttendances_StudentId",
                table: "StudentAttendances",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttendances_Users_StudentId",
                table: "StudentAttendances",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttendances_Users_StudentId",
                table: "StudentAttendances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentAttendances",
                table: "StudentAttendances");

            migrationBuilder.DropIndex(
                name: "IX_StudentAttendances_StudentId",
                table: "StudentAttendances");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "StudentAttendances",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "AttendanceId",
                table: "StudentAttendances",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "StudentUserId",
                table: "StudentAttendances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentAttendances",
                table: "StudentAttendances",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAttendances_StudentUserId",
                table: "StudentAttendances",
                column: "StudentUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttendances_Users_StudentUserId",
                table: "StudentAttendances",
                column: "StudentUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

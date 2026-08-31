using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTaskModelSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentTasks_Users_StudentId",
                table: "StudentTasks");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "Deadline",
                table: "Tasks",
                newName: "DeadLine");

            migrationBuilder.RenameColumn(
                name: "TaskLink",
                table: "Tasks",
                newName: "MentorComment");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Tasks",
                newName: "DateTime");

            migrationBuilder.AddColumn<string>(
                name: "TaskStatus",
                table: "Tasks",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserModelUserId",
                table: "StudentTasks",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentTasks_UserModelUserId",
                table: "StudentTasks",
                column: "UserModelUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentTasks_Users_StudentId",
                table: "StudentTasks",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentTasks_Users_UserModelUserId",
                table: "StudentTasks",
                column: "UserModelUserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentTasks_Users_StudentId",
                table: "StudentTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentTasks_Users_UserModelUserId",
                table: "StudentTasks");

            migrationBuilder.DropIndex(
                name: "IX_StudentTasks_UserModelUserId",
                table: "StudentTasks");

            migrationBuilder.DropColumn(
                name: "TaskStatus",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "UserModelUserId",
                table: "StudentTasks");

            migrationBuilder.RenameColumn(
                name: "DeadLine",
                table: "Tasks",
                newName: "Deadline");

            migrationBuilder.RenameColumn(
                name: "MentorComment",
                table: "Tasks",
                newName: "TaskLink");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Tasks",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Tasks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentTasks_Users_StudentId",
                table: "StudentTasks",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Migrations
{
    /// <inheritdoc />
    public partial class StudentTaskModelAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentTaskModel_Tasks_TaskId",
                table: "StudentTaskModel");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentTaskModel_Users_StudentId",
                table: "StudentTaskModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentTaskModel",
                table: "StudentTaskModel");

            migrationBuilder.RenameTable(
                name: "StudentTaskModel",
                newName: "StudentTasks");

            migrationBuilder.RenameIndex(
                name: "IX_StudentTaskModel_TaskId",
                table: "StudentTasks",
                newName: "IX_StudentTasks_TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentTaskModel_StudentId",
                table: "StudentTasks",
                newName: "IX_StudentTasks_StudentId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Tasks",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubmissionLink",
                table: "StudentTasks",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentTasks",
                table: "StudentTasks",
                column: "StudentTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentTasks_Tasks_TaskId",
                table: "StudentTasks",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "TaskId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentTasks_Users_StudentId",
                table: "StudentTasks",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentTasks_Tasks_TaskId",
                table: "StudentTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentTasks_Users_StudentId",
                table: "StudentTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentTasks",
                table: "StudentTasks");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "SubmissionLink",
                table: "StudentTasks");

            migrationBuilder.RenameTable(
                name: "StudentTasks",
                newName: "StudentTaskModel");

            migrationBuilder.RenameIndex(
                name: "IX_StudentTasks_TaskId",
                table: "StudentTaskModel",
                newName: "IX_StudentTaskModel_TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentTasks_StudentId",
                table: "StudentTaskModel",
                newName: "IX_StudentTaskModel_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentTaskModel",
                table: "StudentTaskModel",
                column: "StudentTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentTaskModel_Tasks_TaskId",
                table: "StudentTaskModel",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "TaskId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentTaskModel_Users_StudentId",
                table: "StudentTaskModel",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

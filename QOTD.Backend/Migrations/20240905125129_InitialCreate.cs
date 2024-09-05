using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QOTD.Backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "ReputationMaster",
                columns: table => new
                {
                    ReputationMasterId = table.Column<int>(type: "int", nullable: false),
                    MinPoints = table.Column<int>(type: "int", nullable: false),
                    UptoPoints = table.Column<int>(type: "int", nullable: false),
                    Badge = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReputationMaster", x => x.ReputationMasterId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GoogleId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedON = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedON = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    Point = table.Column<int>(type: "int", nullable: false),
                    HasMultipleAnswers = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    QuestionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SnapShot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Question_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Question_User_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnswerOption",
                columns: table => new
                {
                    AnswerOptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    Option = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerOption", x => x.AnswerOptionId);
                    table.ForeignKey(
                        name: "FK_AnswerOption_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnswerKey",
                columns: table => new
                {
                    AnswerKeyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    AnswerOptionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerKey", x => x.AnswerKeyId);
                    table.ForeignKey(
                        name: "FK_AnswerKey_AnswerOption_AnswerOptionId",
                        column: x => x.AnswerOptionId,
                        principalTable: "AnswerOption",
                        principalColumn: "AnswerOptionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnswerKey_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserResponse",
                columns: table => new
                {
                    UserResponseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AnswerOptionId = table.Column<int>(type: "int", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserResponse", x => x.UserResponseId);
                    table.ForeignKey(
                        name: "FK_UserResponse_AnswerOption_AnswerOptionId",
                        column: x => x.AnswerOptionId,
                        principalTable: "AnswerOption",
                        principalColumn: "AnswerOptionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserResponse_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UserResponse_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Fun" },
                    { 2, "Math" },
                    { 3, "Func" }
                });

            migrationBuilder.InsertData(
                table: "ReputationMaster",
                columns: new[] { "ReputationMasterId", "Badge", "MinPoints", "UptoPoints" },
                values: new object[,]
                {
                    { 1, "Novice", 0, 49 },
                    { 2, "Learner", 50, 99 },
                    { 3, "Apprentice", 100, 199 },
                    { 4, "Explorer", 200, 349 },
                    { 5, "Adept", 350, 499 },
                    { 6, "Skilled", 500, 749 },
                    { 7, "Expert", 750, 999 },
                    { 8, "Master", 1000, 1449 },
                    { 9, "Veteran", 1500, 1999 },
                    { 10, "Hero", 2000, 2999 },
                    { 11, "Champion", 3000, 4999 },
                    { 12, "Centurion", 4500, 5999 },
                    { 13, "Guardian", 6000, 7999 },
                    { 14, "Sentinel", 8000, 9999 },
                    { 15, "Commander", 10000, 12499 },
                    { 16, "Legend", 12500, 14999 },
                    { 17, "Sage", 15000, 19999 },
                    { 18, "Archon", 20000, 24999 },
                    { 19, "Paladin", 25000, 29999 },
                    { 20, "Grandmaster", 30000, 39999 },
                    { 21, "Overlord", 40000, 49999 },
                    { 22, "Oracle", 50000, 100000 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerKey_AnswerOptionId",
                table: "AnswerKey",
                column: "AnswerOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerKey_QuestionId",
                table: "AnswerKey",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerOption_QuestionId",
                table: "AnswerOption",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_AuthorId",
                table: "Question",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_CategoryId",
                table: "Question",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserResponse_AnswerOptionId",
                table: "UserResponse",
                column: "AnswerOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserResponse_QuestionId",
                table: "UserResponse",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserResponse_UserId",
                table: "UserResponse",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerKey");

            migrationBuilder.DropTable(
                name: "ReputationMaster");

            migrationBuilder.DropTable(
                name: "UserResponse");

            migrationBuilder.DropTable(
                name: "AnswerOption");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodDonationSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BloodTypes",
                columns: table => new
                {
                    BloodTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BloodTyp__B489BA630F3E6DE1", x => x.BloodTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsDonor = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    LastDonationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Active"),
                    BloodTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CC4CA6BE1634", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "BloodStored",
                columns: table => new
                {
                    StoredId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BloodTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    LastUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BloodSto__A98AEA7D25309813", x => x.StoredId);
                    table.ForeignKey(
                        name: "FK_BloodStored_BloodTypes",
                        column: x => x.BloodTypeId,
                        principalTable: "BloodTypes",
                        principalColumn: "BloodTypeId");
                });

            migrationBuilder.CreateTable(
                name: "DonationRequests",
                columns: table => new
                {
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BloodTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmountBlood = table.Column<int>(type: "int", nullable: false),
                    RequestTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContactPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Pending"),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Donation__33A8517AA5458CF1", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_DonationRequests_BloodTypes",
                        column: x => x.BloodTypeId,
                        principalTable: "BloodTypes",
                        principalColumn: "BloodTypeId");
                    table.ForeignKey(
                        name: "FK_DonationRequests_Users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "DonorInformation",
                columns: table => new
                {
                    DonorInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    MedicalStatus = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LastChecked = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DonorInf__DE4F057B17CD15BE", x => x.DonorInfoId);
                    table.ForeignKey(
                        name: "FK_DonorInformation_Users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "DonationsHistory",
                columns: table => new
                {
                    DonationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Completed"),
                    ConfirmedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Donation__C5082EFB21BC77A8", x => x.DonationId);
                    table.ForeignKey(
                        name: "FK_DonationsHistory_DonationRequests",
                        column: x => x.RequestId,
                        principalTable: "DonationRequests",
                        principalColumn: "RequestId");
                    table.ForeignKey(
                        name: "FK_DonationsHistory_Users_ConfirmedBy",
                        column: x => x.ConfirmedBy,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_DonationsHistory_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BloodStored_BloodTypeId",
                table: "BloodStored",
                column: "BloodTypeId");

            migrationBuilder.CreateIndex(
                name: "UQ_BloodTypes_Name",
                table: "BloodTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DonationRequests_BloodTypeId",
                table: "DonationRequests",
                column: "BloodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DonationRequests_UserId",
                table: "DonationRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DonationsHistory_ConfirmedBy",
                table: "DonationsHistory",
                column: "ConfirmedBy");

            migrationBuilder.CreateIndex(
                name: "IX_DonationsHistory_RequestId",
                table: "DonationsHistory",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_DonationsHistory_UserId",
                table: "DonationsHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ_DonorInformation_UserId",
                table: "DonorInformation",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BloodStored");

            migrationBuilder.DropTable(
                name: "DonationsHistory");

            migrationBuilder.DropTable(
                name: "DonorInformation");

            migrationBuilder.DropTable(
                name: "DonationRequests");

            migrationBuilder.DropTable(
                name: "BloodTypes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

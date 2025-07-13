using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BloodDonationSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Seeddatabloodtype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldDefaultValue: "Active");

            migrationBuilder.AlterColumn<string>(
                name: "MedicalStatus",
                table: "DonorInformation",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "DonationsHistory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldDefaultValue: "Completed");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "DonationRequests",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldDefaultValue: "Pending");

            migrationBuilder.InsertData(
                table: "BloodTypes",
                columns: new[] { "BloodTypeId", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("0f5f77fb-2bd4-4aeb-9bd4-bb56745c8845"), "A negative blood type", "A-" },
                    { new Guid("1479d6c3-0c85-4cb7-a2c4-894c35e21eb1"), "AB negative blood type", "AB-" },
                    { new Guid("2b0f96e4-9052-4d68-a937-9adfc9d231d1"), "A positive blood type", "A+" },
                    { new Guid("62ef305e-755a-4651-9ed7-6fc4b4061e79"), "O negative blood type (universal donor)", "O-" },
                    { new Guid("82f33bfb-7fa4-432e-8735-1c0e5c2f99f7"), "B negative blood type", "B-" },
                    { new Guid("91baf3d9-759f-4bb8-82a4-3d9d645d91b7"), "B positive blood type", "B+" },
                    { new Guid("b160fa12-dfa5-44c7-a179-6ef0f3c7c28c"), "O positive blood type (most common)", "O+" },
                    { new Guid("edc95a1c-0c3f-4a61-a104-f949109e7c0f"), "AB positive blood type (universal plasma donor)", "AB+" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_BloodTypeId",
                table: "Users",
                column: "BloodTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_BloodTypes_BloodTypeId",
                table: "Users",
                column: "BloodTypeId",
                principalTable: "BloodTypes",
                principalColumn: "BloodTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_BloodTypes_BloodTypeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_BloodTypeId",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "BloodTypes",
                keyColumn: "BloodTypeId",
                keyValue: new Guid("0f5f77fb-2bd4-4aeb-9bd4-bb56745c8845"));

            migrationBuilder.DeleteData(
                table: "BloodTypes",
                keyColumn: "BloodTypeId",
                keyValue: new Guid("1479d6c3-0c85-4cb7-a2c4-894c35e21eb1"));

            migrationBuilder.DeleteData(
                table: "BloodTypes",
                keyColumn: "BloodTypeId",
                keyValue: new Guid("2b0f96e4-9052-4d68-a937-9adfc9d231d1"));

            migrationBuilder.DeleteData(
                table: "BloodTypes",
                keyColumn: "BloodTypeId",
                keyValue: new Guid("62ef305e-755a-4651-9ed7-6fc4b4061e79"));

            migrationBuilder.DeleteData(
                table: "BloodTypes",
                keyColumn: "BloodTypeId",
                keyValue: new Guid("82f33bfb-7fa4-432e-8735-1c0e5c2f99f7"));

            migrationBuilder.DeleteData(
                table: "BloodTypes",
                keyColumn: "BloodTypeId",
                keyValue: new Guid("91baf3d9-759f-4bb8-82a4-3d9d645d91b7"));

            migrationBuilder.DeleteData(
                table: "BloodTypes",
                keyColumn: "BloodTypeId",
                keyValue: new Guid("b160fa12-dfa5-44c7-a179-6ef0f3c7c28c"));

            migrationBuilder.DeleteData(
                table: "BloodTypes",
                keyColumn: "BloodTypeId",
                keyValue: new Guid("edc95a1c-0c3f-4a61-a104-f949109e7c0f"));

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "Active",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "MedicalStatus",
                table: "DonorInformation",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "DonationsHistory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "Completed",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "DonationRequests",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "Pending",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class AddArchivatedColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorDbPolyclinicDb_doctror_DoctorId",
                table: "DoctorDbPolyclinicDb");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorDbPolyclinicDb_polyclinic_DoctorId",
                table: "DoctorDbPolyclinicDb");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorDbSpecializationDb_doctror_DoctorId",
                table: "DoctorDbSpecializationDb");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorDbSpecializationDb",
                table: "DoctorDbSpecializationDb");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorDbPolyclinicDb",
                table: "DoctorDbPolyclinicDb");

            migrationBuilder.DropColumn(
                name: "doctor_id",
                table: "polyclinic");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "DoctorDbPolyclinicDb",
                newName: "PolyclinicsPolyclinicId");

            migrationBuilder.AddColumn<bool>(
                name: "archived",
                table: "specialization",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "archived",
                table: "polyclinic",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "archived",
                table: "doctror",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "DoctorsDoctorId",
                table: "DoctorDbSpecializationDb",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DoctorsDoctorId",
                table: "DoctorDbPolyclinicDb",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "archived",
                table: "city",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorDbSpecializationDb",
                table: "DoctorDbSpecializationDb",
                columns: new[] { "DoctorId", "DoctorsDoctorId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorDbPolyclinicDb",
                table: "DoctorDbPolyclinicDb",
                columns: new[] { "DoctorsDoctorId", "PolyclinicsPolyclinicId" });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorDbSpecializationDb_DoctorsDoctorId",
                table: "DoctorDbSpecializationDb",
                column: "DoctorsDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorDbPolyclinicDb_PolyclinicsPolyclinicId",
                table: "DoctorDbPolyclinicDb",
                column: "PolyclinicsPolyclinicId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorDbPolyclinicDb_doctror_DoctorsDoctorId",
                table: "DoctorDbPolyclinicDb",
                column: "DoctorsDoctorId",
                principalTable: "doctror",
                principalColumn: "doctor_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorDbPolyclinicDb_polyclinic_PolyclinicsPolyclinicId",
                table: "DoctorDbPolyclinicDb",
                column: "PolyclinicsPolyclinicId",
                principalTable: "polyclinic",
                principalColumn: "polyclinic_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorDbSpecializationDb_doctror_DoctorsDoctorId",
                table: "DoctorDbSpecializationDb",
                column: "DoctorsDoctorId",
                principalTable: "doctror",
                principalColumn: "doctor_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorDbPolyclinicDb_doctror_DoctorsDoctorId",
                table: "DoctorDbPolyclinicDb");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorDbPolyclinicDb_polyclinic_PolyclinicsPolyclinicId",
                table: "DoctorDbPolyclinicDb");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorDbSpecializationDb_doctror_DoctorsDoctorId",
                table: "DoctorDbSpecializationDb");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorDbSpecializationDb",
                table: "DoctorDbSpecializationDb");

            migrationBuilder.DropIndex(
                name: "IX_DoctorDbSpecializationDb_DoctorsDoctorId",
                table: "DoctorDbSpecializationDb");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorDbPolyclinicDb",
                table: "DoctorDbPolyclinicDb");

            migrationBuilder.DropIndex(
                name: "IX_DoctorDbPolyclinicDb_PolyclinicsPolyclinicId",
                table: "DoctorDbPolyclinicDb");

            migrationBuilder.DropColumn(
                name: "archived",
                table: "specialization");

            migrationBuilder.DropColumn(
                name: "archived",
                table: "polyclinic");

            migrationBuilder.DropColumn(
                name: "archived",
                table: "doctror");

            migrationBuilder.DropColumn(
                name: "DoctorsDoctorId",
                table: "DoctorDbSpecializationDb");

            migrationBuilder.DropColumn(
                name: "DoctorsDoctorId",
                table: "DoctorDbPolyclinicDb");

            migrationBuilder.DropColumn(
                name: "archived",
                table: "city");

            migrationBuilder.RenameColumn(
                name: "PolyclinicsPolyclinicId",
                table: "DoctorDbPolyclinicDb",
                newName: "DoctorId");

            migrationBuilder.AddColumn<Guid>(
                name: "doctor_id",
                table: "polyclinic",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorDbSpecializationDb",
                table: "DoctorDbSpecializationDb",
                column: "DoctorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorDbPolyclinicDb",
                table: "DoctorDbPolyclinicDb",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorDbPolyclinicDb_doctror_DoctorId",
                table: "DoctorDbPolyclinicDb",
                column: "DoctorId",
                principalTable: "doctror",
                principalColumn: "doctor_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorDbPolyclinicDb_polyclinic_DoctorId",
                table: "DoctorDbPolyclinicDb",
                column: "DoctorId",
                principalTable: "polyclinic",
                principalColumn: "polyclinic_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorDbSpecializationDb_doctror_DoctorId",
                table: "DoctorDbSpecializationDb",
                column: "DoctorId",
                principalTable: "doctror",
                principalColumn: "doctor_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

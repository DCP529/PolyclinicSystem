using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class Authentication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "city",
                columns: table => new
                {
                    cityid = table.Column<Guid>(name: "city_id", type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_city", x => x.cityid);
                });

            migrationBuilder.CreateTable(
                name: "doctror",
                columns: table => new
                {
                    doctorid = table.Column<Guid>(name: "doctor_id", type: "uuid", nullable: false),
                    fio = table.Column<string>(type: "text", nullable: false),
                    admissioncost = table.Column<decimal>(name: "admission_cost", type: "numeric", nullable: false),
                    contactnumber = table.Column<int>(name: "contact_number", type: "integer", nullable: false),
                    imagepath = table.Column<string>(name: "image_path", type: "text", nullable: false),
                    shortdescription = table.Column<string>(name: "short_description", type: "text", nullable: false),
                    fulldescription = table.Column<string>(name: "full_description", type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doctror", x => x.doctorid);
                });

            migrationBuilder.CreateTable(
                name: "login",
                columns: table => new
                {
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_login", x => x.email);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "specialization",
                columns: table => new
                {
                    specializationid = table.Column<Guid>(name: "specialization_id", type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    doctorid = table.Column<Guid>(name: "doctor_id", type: "uuid", nullable: false),
                    experiencespecialization = table.Column<int>(name: "experience_specialization", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specialization", x => x.specializationid);
                });

            migrationBuilder.CreateTable(
                name: "polyclinic",
                columns: table => new
                {
                    polyclinicid = table.Column<Guid>(name: "polyclinic_id", type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    contactnumber = table.Column<int>(name: "contact_number", type: "integer", nullable: false),
                    imagepath = table.Column<string>(name: "image_path", type: "text", nullable: false),
                    cityid = table.Column<Guid>(name: "city_id", type: "uuid", nullable: false),
                    doctorid = table.Column<Guid>(name: "doctor_id", type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_polyclinic", x => x.polyclinicid);
                    table.ForeignKey(
                        name: "FK_polyclinic_city_city_id",
                        column: x => x.cityid,
                        principalTable: "city",
                        principalColumn: "city_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "account",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    roleid = table.Column<Guid>(name: "role_id", type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account", x => x.id);
                    table.ForeignKey(
                        name: "FK_account_roles_role_id",
                        column: x => x.roleid,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorDbSpecializationDb",
                columns: table => new
                {
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorDbSpecializationDb", x => x.DoctorId);
                    table.ForeignKey(
                        name: "FK_DoctorDbSpecializationDb_doctror_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "doctror",
                        principalColumn: "doctor_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorDbSpecializationDb_specialization_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "specialization",
                        principalColumn: "specialization_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorDbPolyclinicDb",
                columns: table => new
                {
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorDbPolyclinicDb", x => x.DoctorId);
                    table.ForeignKey(
                        name: "FK_DoctorDbPolyclinicDb_doctror_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "doctror",
                        principalColumn: "doctor_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorDbPolyclinicDb_polyclinic_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "polyclinic",
                        principalColumn: "polyclinic_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_account_role_id",
                table: "account",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_polyclinic_city_id",
                table: "polyclinic",
                column: "city_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "account");

            migrationBuilder.DropTable(
                name: "DoctorDbPolyclinicDb");

            migrationBuilder.DropTable(
                name: "DoctorDbSpecializationDb");

            migrationBuilder.DropTable(
                name: "login");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "polyclinic");

            migrationBuilder.DropTable(
                name: "doctror");

            migrationBuilder.DropTable(
                name: "specialization");

            migrationBuilder.DropTable(
                name: "city");
        }
    }
}

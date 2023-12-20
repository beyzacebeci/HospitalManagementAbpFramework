﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalManagement.Migrations
{
    /// <inheritdoc />
    public partial class hospitalIddeletedddoctortableaddedhddTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppDoctors_AppHospitals_HospitalId",
                table: "AppDoctors");

            migrationBuilder.DropIndex(
                name: "IX_AppDoctors_HospitalId",
                table: "AppDoctors");

            migrationBuilder.DropColumn(
                name: "HospitalId",
                table: "AppDoctors");

            migrationBuilder.CreateTable(
                name: "AppHospitalDepartmentDoctors",
                columns: table => new
                {
                    HospitalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppHospitalDepartmentDoctors", x => new { x.HospitalId, x.DepartmentId, x.DoctorId });
                    table.ForeignKey(
                        name: "FK_AppHospitalDepartmentDoctors_AppHospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "AppHospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppHospitalDepartmentDoctors");

            migrationBuilder.AddColumn<Guid>(
                name: "HospitalId",
                table: "AppDoctors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppDoctors_HospitalId",
                table: "AppDoctors",
                column: "HospitalId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppDoctors_AppHospitals_HospitalId",
                table: "AppDoctors",
                column: "HospitalId",
                principalTable: "AppHospitals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

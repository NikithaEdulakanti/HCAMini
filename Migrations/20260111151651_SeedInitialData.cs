using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HCAMiniEHR.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Healthcare",
                table: "Doctor",
                columns: new[] { "DoctorId", "CreatedAt", "Email", "FullName", "IsActive", "Phone", "Specialization" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 11, 20, 46, 51, 168, DateTimeKind.Local).AddTicks(9607), null, "Dr. Ananya Rao", true, null, "Cardiology" },
                    { 2, new DateTime(2026, 1, 11, 20, 46, 51, 168, DateTimeKind.Local).AddTicks(9619), null, "Dr. Vikram Singh", true, null, "Orthopedics" },
                    { 3, new DateTime(2026, 1, 11, 20, 46, 51, 168, DateTimeKind.Local).AddTicks(9620), null, "Dr. Meera Iyer", true, null, "Dermatology" },
                    { 4, new DateTime(2026, 1, 11, 20, 46, 51, 168, DateTimeKind.Local).AddTicks(9621), null, "Dr. Karthik N", true, null, "Neurology" },
                    { 5, new DateTime(2026, 1, 11, 20, 46, 51, 168, DateTimeKind.Local).AddTicks(9622), null, "Dr. Pooja Sharma", true, null, "General Medicine" }
                });

            migrationBuilder.InsertData(
                schema: "Healthcare",
                table: "Patient",
                columns: new[] { "PatientId", "CreatedAt", "DateOfBirth", "Email", "FullName", "Gender", "Phone" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 11, 20, 46, 51, 168, DateTimeKind.Local).AddTicks(9710), new DateTime(1995, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Ravi Kumar", "Male", null },
                    { 2, new DateTime(2026, 1, 11, 20, 46, 51, 168, DateTimeKind.Local).AddTicks(9718), new DateTime(1990, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sita Devi", "Female", null },
                    { 3, new DateTime(2026, 1, 11, 20, 46, 51, 168, DateTimeKind.Local).AddTicks(9719), new DateTime(1988, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Amit Shah", "Male", null },
                    { 4, new DateTime(2026, 1, 11, 20, 46, 51, 168, DateTimeKind.Local).AddTicks(9721), new DateTime(1997, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Neha Verma", "Female", null },
                    { 5, new DateTime(2026, 1, 11, 20, 46, 51, 168, DateTimeKind.Local).AddTicks(9723), new DateTime(1985, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Kiran Rao", "Male", null },
                    { 6, new DateTime(2026, 1, 11, 20, 46, 51, 168, DateTimeKind.Local).AddTicks(9724), null, null, "Priya Nair", "Female", null },
                    { 7, new DateTime(2026, 1, 11, 20, 46, 51, 168, DateTimeKind.Local).AddTicks(9725), new DateTime(1999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Arjun Patel", "Male", null },
                    { 8, new DateTime(2026, 1, 11, 20, 46, 51, 168, DateTimeKind.Local).AddTicks(9726), null, null, "Lakshmi Iyer", "Female", null },
                    { 9, new DateTime(2026, 1, 11, 20, 46, 51, 168, DateTimeKind.Local).AddTicks(9727), new DateTime(1978, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Suresh Babu", "Male", null },
                    { 10, new DateTime(2026, 1, 11, 20, 46, 51, 168, DateTimeKind.Local).AddTicks(9728), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Divya Reddy", "Female", null }
                });

            migrationBuilder.InsertData(
                schema: "Healthcare",
                table: "Appointment",
                columns: new[] { "AppointmentId", "AppointmentDate", "CreatedAt", "DoctorId", "PatientId", "Reason", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 12, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 11, 20, 46, 51, 168, DateTimeKind.Local).AddTicks(9747), 5, 1, "General Checkup", "Scheduled" },
                    { 2, new DateTime(2026, 1, 13, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 11, 20, 46, 51, 168, DateTimeKind.Local).AddTicks(9759), 1, 2, "Chest Pain", "Scheduled" },
                    { 3, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 11, 20, 46, 51, 168, DateTimeKind.Local).AddTicks(9761), 2, 3, "Knee Pain", "Scheduled" },
                    { 4, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 11, 20, 46, 51, 168, DateTimeKind.Local).AddTicks(9763), 3, 4, "Skin Allergy", "Scheduled" },
                    { 5, new DateTime(2026, 1, 16, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 11, 20, 46, 51, 168, DateTimeKind.Local).AddTicks(9765), 4, 5, "Headache", "Scheduled" }
                });

            migrationBuilder.InsertData(
                schema: "Healthcare",
                table: "LabOrder",
                columns: new[] { "LabOrderId", "AppointmentId", "OrderedAt", "Status", "TestName" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 1, 11, 20, 46, 51, 168, DateTimeKind.Local).AddTicks(9781), "Pending", "Blood Test" },
                    { 2, 1, new DateTime(2026, 1, 11, 20, 46, 51, 168, DateTimeKind.Local).AddTicks(9782), "Pending", "Urine Test" },
                    { 3, 1, new DateTime(2026, 1, 11, 20, 46, 51, 168, DateTimeKind.Local).AddTicks(9783), "Ordered", "Lipid Profile" },
                    { 4, 2, new DateTime(2026, 1, 11, 20, 46, 51, 168, DateTimeKind.Local).AddTicks(9784), "Completed", "ECG" },
                    { 5, 3, new DateTime(2026, 1, 11, 20, 46, 51, 168, DateTimeKind.Local).AddTicks(9785), "Pending", "X-Ray" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Healthcare",
                table: "Appointment",
                keyColumn: "AppointmentId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "Healthcare",
                table: "Appointment",
                keyColumn: "AppointmentId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "Healthcare",
                table: "LabOrder",
                keyColumn: "LabOrderId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Healthcare",
                table: "LabOrder",
                keyColumn: "LabOrderId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "Healthcare",
                table: "LabOrder",
                keyColumn: "LabOrderId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "Healthcare",
                table: "LabOrder",
                keyColumn: "LabOrderId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "Healthcare",
                table: "LabOrder",
                keyColumn: "LabOrderId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "Healthcare",
                table: "Patient",
                keyColumn: "PatientId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "Healthcare",
                table: "Patient",
                keyColumn: "PatientId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "Healthcare",
                table: "Patient",
                keyColumn: "PatientId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "Healthcare",
                table: "Patient",
                keyColumn: "PatientId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "Healthcare",
                table: "Patient",
                keyColumn: "PatientId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "Healthcare",
                table: "Appointment",
                keyColumn: "AppointmentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Healthcare",
                table: "Appointment",
                keyColumn: "AppointmentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "Healthcare",
                table: "Appointment",
                keyColumn: "AppointmentId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "Healthcare",
                table: "Doctor",
                keyColumn: "DoctorId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "Healthcare",
                table: "Doctor",
                keyColumn: "DoctorId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "Healthcare",
                table: "Patient",
                keyColumn: "PatientId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "Healthcare",
                table: "Patient",
                keyColumn: "PatientId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "Healthcare",
                table: "Doctor",
                keyColumn: "DoctorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Healthcare",
                table: "Doctor",
                keyColumn: "DoctorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "Healthcare",
                table: "Doctor",
                keyColumn: "DoctorId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "Healthcare",
                table: "Patient",
                keyColumn: "PatientId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Healthcare",
                table: "Patient",
                keyColumn: "PatientId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "Healthcare",
                table: "Patient",
                keyColumn: "PatientId",
                keyValue: 3);
        }
    }
}

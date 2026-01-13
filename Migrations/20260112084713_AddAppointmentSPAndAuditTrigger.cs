using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCAMiniEHR.Migrations
{
    public partial class AddAppointmentSPAndAuditTrigger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ===============================
            // STORED PROCEDURE
            // ===============================

            migrationBuilder.Sql("""
            IF OBJECT_ID('Healthcare.CreateAppointment', 'P') IS NOT NULL
                DROP PROCEDURE Healthcare.CreateAppointment;
            """);

            migrationBuilder.Sql("""
            CREATE PROCEDURE Healthcare.CreateAppointment
                @PatientId INT,
                @DoctorId INT,
                @AppointmentDate DATETIME,
                @Reason NVARCHAR(200)
            AS
            BEGIN
                SET NOCOUNT ON;

                INSERT INTO Healthcare.Appointment
                (
                    PatientId,
                    DoctorId,
                    AppointmentDate,
                    Reason,
                    CreatedAt
                )
                VALUES
                (
                    @PatientId,
                    @DoctorId,
                    @AppointmentDate,
                    @Reason,
                    GETDATE()
                );
            END
            """);

            // ===============================
            // AUDIT TRIGGER
            // ===============================

            migrationBuilder.Sql("""
            IF OBJECT_ID('Healthcare.trg_Appointment_Audit', 'TR') IS NOT NULL
                DROP TRIGGER Healthcare.trg_Appointment_Audit;
            """);

            migrationBuilder.Sql("""
            CREATE TRIGGER Healthcare.trg_Appointment_Audit
            ON Healthcare.Appointment
            AFTER INSERT, UPDATE, DELETE
            AS
            BEGIN
                SET NOCOUNT ON;

                DECLARE @Action NVARCHAR(20);
                DECLARE @RecordId INT;

                IF EXISTS (SELECT * FROM inserted) AND EXISTS (SELECT * FROM deleted)
                BEGIN
                    SET @Action = 'UPDATE';
                    SELECT @RecordId = AppointmentId FROM inserted;
                END
                ELSE IF EXISTS (SELECT * FROM inserted)
                BEGIN
                    SET @Action = 'INSERT';
                    SELECT @RecordId = AppointmentId FROM inserted;
                END
                ELSE
                BEGIN
                    SET @Action = 'DELETE';
                    SELECT @RecordId = AppointmentId FROM deleted;
                END

                INSERT INTO Healthcare.AuditLog
                (
                    TableName,
                    Action,
                    RecordId,
                    ChangedAt
                )
                VALUES
                (
                    'Appointment',
                    @Action,
                    @RecordId,
                    GETDATE()
                );
            END
            """);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
            DROP TRIGGER IF EXISTS Healthcare.trg_Appointment_Audit;
            """);

            migrationBuilder.Sql("""
            DROP PROCEDURE IF EXISTS Healthcare.CreateAppointment;
            """);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCAMiniEHR.Migrations
{
    public partial class AddPatientAuditTrigger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
            IF OBJECT_ID('Healthcare.trg_Patient_Audit', 'TR') IS NOT NULL
                DROP TRIGGER Healthcare.trg_Patient_Audit;
            """);

            migrationBuilder.Sql("""
            CREATE TRIGGER Healthcare.trg_Patient_Audit
            ON Healthcare.Patient
            AFTER INSERT, UPDATE, DELETE
            AS
            BEGIN
                SET NOCOUNT ON;

                DECLARE @Action NVARCHAR(20);
                DECLARE @RecordId INT;

                IF EXISTS (SELECT * FROM inserted) AND EXISTS (SELECT * FROM deleted)
                BEGIN
                    SET @Action = 'UPDATE';
                    SELECT @RecordId = PatientId FROM inserted;
                END
                ELSE IF EXISTS (SELECT * FROM inserted)
                BEGIN
                    SET @Action = 'INSERT';
                    SELECT @RecordId = PatientId FROM inserted;
                END
                ELSE
                BEGIN
                    SET @Action = 'DELETE';
                    SELECT @RecordId = PatientId FROM deleted;
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
                    'Patient',
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
            DROP TRIGGER IF EXISTS Healthcare.trg_Patient_Audit;
            """);
        }
    }
}

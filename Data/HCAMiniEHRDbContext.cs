using HCAMiniEHR.Models;
using Microsoft.EntityFrameworkCore;

namespace HCAMiniEHR.Data
{
    public class HCAMiniEHRDbContext : DbContext
    {
        public HCAMiniEHRDbContext(DbContextOptions<HCAMiniEHRDbContext> options)
            : base(options)
        {
        }

        // =========================
        // DbSets
        // =========================
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<LabOrder> LabOrders => Set<LabOrder>();
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // =========================
            // Default Schema
            // =========================
            modelBuilder.HasDefaultSchema("Healthcare");

            // =========================
            // Patient (TRIGGERS ENABLED)
            // =========================
            modelBuilder.Entity<Patient>()
                .ToTable("Patient", tb =>
                {
                    // REQUIRED when SQL triggers exist
                    tb.UseSqlOutputClause(false);

                    // Tell EF Core that triggers exist
                    tb.HasTrigger("trg_Patient_Audit");
                });


            modelBuilder.Entity<Patient>()
    .Property(p => p.DateOfBirth)
    .HasColumnType("date")
    .IsRequired()
    .HasConversion(
        d => d.ToDateTime(TimeOnly.MinValue),
        d => DateOnly.FromDateTime(d)
    );


            // =========================
            // Doctor
            // =========================
            modelBuilder.Entity<Doctor>()
                .ToTable("Doctor");

            // =========================
            // Appointment (TRIGGERS ENABLED)
            // =========================
            modelBuilder.Entity<Appointment>()
                .ToTable("Appointment", tb =>
                {
                    // 🔥 REQUIRED WHEN SQL TRIGGERS EXIST
                    tb.UseSqlOutputClause(false);
                });

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            // =========================
            // LabOrder
            // =========================
            modelBuilder.Entity<LabOrder>()
    .ToTable("LabOrder");

            modelBuilder.Entity<LabOrder>()
                .HasOne(l => l.Appointment)
                .WithMany(a => a.LabOrders)
                .HasForeignKey(l => l.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LabOrder>()
                .HasOne(l => l.Appointment)
                .WithMany(a => a.LabOrders)
                .HasForeignKey(l => l.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // =========================
            // AuditLog
            // =========================
            modelBuilder.Entity<AuditLog>()
                .ToTable("AuditLog");

            // =========================
            // Seed Data
            // =========================

            modelBuilder.Entity<Doctor>().HasData(
                new Doctor { DoctorId = 1, FullName = "Dr. Ananya Rao", Specialization = "Cardiology", IsActive = true },
                new Doctor { DoctorId = 2, FullName = "Dr. Vikram Singh", Specialization = "Orthopedics", IsActive = true },
                new Doctor { DoctorId = 3, FullName = "Dr. Meera Iyer", Specialization = "Dermatology", IsActive = true },
                new Doctor { DoctorId = 4, FullName = "Dr. Karthik N", Specialization = "Neurology", IsActive = true },
                new Doctor { DoctorId = 5, FullName = "Dr. Pooja Sharma", Specialization = "General Medicine", IsActive = true }
            );

            modelBuilder.Entity<Patient>().HasData(
    new Patient { PatientId = 1, FullName = "Ravi Kumar", Gender = "Male", DateOfBirth = new DateOnly(1995, 5, 10) },
    new Patient { PatientId = 2, FullName = "Sita Devi", Gender = "Female", DateOfBirth = new DateOnly(1990, 8, 22) },
    new Patient { PatientId = 3, FullName = "Amit Shah", Gender = "Male", DateOfBirth = new DateOnly(1988, 1, 15) },
    new Patient { PatientId = 4, FullName = "Neha Verma", Gender = "Female", DateOfBirth = new DateOnly(1997, 3, 30) },
    new Patient { PatientId = 5, FullName = "Kiran Rao", Gender = "Male", DateOfBirth = new DateOnly(1985, 11, 5) },

    // previously NULL → now safe default
    new Patient { PatientId = 6, FullName = "Priya Nair", Gender = "Female", DateOfBirth = new DateOnly(2000, 1, 1) },
    new Patient { PatientId = 7, FullName = "Arjun Patel", Gender = "Male", DateOfBirth = new DateOnly(1999, 9, 9) },
    new Patient { PatientId = 8, FullName = "Lakshmi Iyer", Gender = "Female", DateOfBirth = new DateOnly(2000, 1, 1) },
    new Patient { PatientId = 9, FullName = "Suresh Babu", Gender = "Male", DateOfBirth = new DateOnly(1978, 7, 14) },
    new Patient { PatientId = 10, FullName = "Divya Reddy", Gender = "Female", DateOfBirth = new DateOnly(2000, 2, 27) }
);


            modelBuilder.Entity<Appointment>().HasData(
                new Appointment
                {
                    AppointmentId = 1,
                    PatientId = 1,
                    DoctorId = 5,
                    AppointmentDate = DateTime.Today.AddDays(1),
                    Reason = "General Checkup",
                    Status = "Scheduled"
                },
                new Appointment
                {
                    AppointmentId = 2,
                    PatientId = 2,
                    DoctorId = 1,
                    AppointmentDate = DateTime.Today.AddDays(2),
                    Reason = "Chest Pain",
                    Status = "Scheduled"
                },
                new Appointment
                {
                    AppointmentId = 3,
                    PatientId = 3,
                    DoctorId = 2,
                    AppointmentDate = DateTime.Today.AddDays(3),
                    Reason = "Knee Pain",
                    Status = "Scheduled"
                },
                new Appointment
                {
                    AppointmentId = 4,
                    PatientId = 4,
                    DoctorId = 3,
                    AppointmentDate = DateTime.Today.AddDays(4),
                    Reason = "Skin Allergy",
                    Status = "Scheduled"
                },
                new Appointment
                {
                    AppointmentId = 5,
                    PatientId = 5,
                    DoctorId = 4,
                    AppointmentDate = DateTime.Today.AddDays(5),
                    Reason = "Headache",
                    Status = "Scheduled"
                }
            );

            modelBuilder.Entity<LabOrder>().HasData(
                new LabOrder { LabOrderId = 1, AppointmentId = 1, TestName = "Blood Test", Status = "Pending" },
                new LabOrder { LabOrderId = 2, AppointmentId = 1, TestName = "Urine Test", Status = "Pending" },
                new LabOrder { LabOrderId = 3, AppointmentId = 1, TestName = "Lipid Profile", Status = "Ordered" },
                new LabOrder { LabOrderId = 4, AppointmentId = 2, TestName = "ECG", Status = "Completed" },
                new LabOrder { LabOrderId = 5, AppointmentId = 3, TestName = "X-Ray", Status = "Pending" }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}

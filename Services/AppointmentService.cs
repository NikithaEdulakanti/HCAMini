using Microsoft.EntityFrameworkCore;
using HCAMiniEHR.Data;

namespace HCAMiniEHR.Services
{
    public class AppointmentService
    {
        private readonly HCAMiniEHRDbContext _context;

        public AppointmentService(HCAMiniEHRDbContext context)
        {
            _context = context;
        }

        public async Task CreateAppointmentAsync(
            int patientId,
            int doctorId,
            DateTime appointmentDate,
            string reason)
        {
            await _context.Database.ExecuteSqlRawAsync(
                @"EXEC Healthcare.CreateAppointment 
                    @PatientId = {0},
                    @DoctorId = {1},
                    @AppointmentDate = {2},
                    @Reason = {3}",
                patientId,
                doctorId,
                appointmentDate,
                reason
            );
        }
    }
}

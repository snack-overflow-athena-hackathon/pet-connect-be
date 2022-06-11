using Microsoft.AspNetCore.Mvc;
using pet.Repositories;

namespace pet;

public class AppointmentRepository : IAppointmentRepository
{
    private IQuery _query;

    public AppointmentRepository(IQuery query)
    {
        _query = query;
    }

    public async Task<IEnumerable<Appointment>> GetAppointmentsNew()
    {
        var sqlStatement = GetAppointmentsSqlStatement();
        var results = await _query.QueryAsync<Appointment>(sqlStatement);
        return results;
    }

    public async Task<IEnumerable<Appointment>> GetAppointments()
    {
        var returnedAppointments = new List<Appointment>
        {
            new Appointment
            {
                Id = 1,
                DateTime = "",
                OwnerId = 456,
                VisitorId = 457,
                PetId = 123,
                LocationId = 1,
                Cancelled = false,
                Attended = false
            },
            new Appointment
            {
                Id = 2,
                DateTime = "",
                OwnerId = 456,
                VisitorId = 457,
                PetId = 2,
                LocationId = 2,
                Cancelled = true,
                Attended = false
            },
            new Appointment
            {
                Id = 3,
                DateTime = "",
                OwnerId = 456,
                VisitorId = 457,
                PetId = 2,
                LocationId = 1,
                Cancelled = false,
                Attended = true
            }
        };
        
        return returnedAppointments;
    }

    private static string GetAppointmentsSqlStatement()
    {
        return $@"SELECT Id, DateTime, OwnerId, VisitorId, PetId, LocationId, Cancelled, Attended FROM Appointments";
    }
}

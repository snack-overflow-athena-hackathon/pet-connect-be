using pet.Repositories;
using Serilog;

namespace pet;

public class AppointmentRepository : IAppointmentRepository
{
    private IQuery _query;

    public AppointmentRepository(IQuery query)
    {
        _query = query;
    }

    public async Task<IEnumerable<Appointment>> GetAppointments()
    {
        var sqlStatement = GetAllAppointmentsSqlStatement();
        var appointments = await _query.QueryAsync<Appointment>(sqlStatement);

        Log.Information($"Getting all appointments, found {appointments.Count()} in database");

        return appointments;
    }

    public async Task<IEnumerable<Appointment>> GetAppointmentsByUserId(long ownerId)
    {
        var sql = GetAppointmentsByUserIdSqlStatement();
        var appointments = await _query.QueryAsync<Appointment>(sql, new {OwnerId = ownerId});

        Log.Information($"Getting all appointments for owner Id {ownerId}, found {appointments.Count()} in database for this user.");

        return appointments;
    }

    public async Task<Appointment> GetAppointmentById(long appointmentId)
    {
        var sql = GetAppointmentByIdSqlStatement();
        var appointment = await _query.QueryFirstOrDefaultAsync<Appointment>(sql, new {Id = appointmentId});

        return appointment;
    }

    private static string GetAllAppointmentsSqlStatement()
    {
        return @"
                 SELECT Id, AppointmentDateTimeUTC, OwnerId, VisitorId, PetId, LocationId, AppointmentState
                 FROM Appointments";
    }

    private static string GetAppointmentsByUserIdSqlStatement()
    {
        return @"
                 SELECT Id, AppointmentDateTimeUTC, OwnerId, VisitorId, PetId, LocationId, AppointmentState
                 FROM Appointments
                 WHERE OwnerId = @OwnerId";
    }

    private static string GetAppointmentByIdSqlStatement()
    {
        return @"
                  SELECT Id, AppointmentDateTimeUTC, OwnerId, VisitorId, PetId, LocationId, AppointmentState
                  FROM Appointments
                  WHERE Id = @Id";
    }
}

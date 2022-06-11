using pet.Exceptions;
using pet.Repositories;
using Serilog;

namespace pet;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly IQuery _query;

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

        if (appointment == null)
            throw new AppointmentNotFoundException(appointmentId);

        return appointment;
    }

    public async Task<long> AddAppointment(Appointment appointment)
    {
        var sql = AddAppointmentSqlStatement();
        var id = await _query.ExecuteScalarAsync<long>(sql, appointment);

        Log.Information($"Added new appointment for owner id {appointment.OwnerId} to database with id {id}");

        return id;
    }

    public async Task EditAppointment(Appointment appointment)
    {
        var sql = EditAppointmentSqlStatement();
        await _query.ExecuteAsync(sql, appointment);

        Log.Information($"Edited Pet {appointment.Id}.");
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

    private static string AddAppointmentSqlStatement()
    {
        return @"
                 INSERT INTO Appointments
                 (
                   AppointmentDateTimeUTC, OwnerId, VisitorId, PetId, LocationId, AppointmentState
                 )
                 VALUES
                 (
                   @AppointmentDateTimeUTC, :OwnerId, @VisitorId, @PetId, @LocationId, @AppointmentState
                 ) RETURNING Id";
    }

    private static string EditAppointmentSqlStatement()
    {
        return $@"
                  UPDATE Appointments
                  SET
                    AppointmentDateTimeUTC = @AppointmentDateTimeUTC, 
                    OwnerId = @OwnerId, 
                    VisitorId = @VisitorId, 
                    PetId = @PetId, 
                    LocationId = @LocationId, 
                    AppointmentState = @AppointmentState 
                  WHERE Id = @Id";
    }
}
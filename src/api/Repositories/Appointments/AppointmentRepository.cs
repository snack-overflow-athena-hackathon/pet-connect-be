using pet.Exceptions;
using pet.Repositories;
using pet.Models.DBEntities;
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
        var appointments = await _query.QueryAsync<AppointmentDBEntity>(sqlStatement);

        Log.Information($"Getting all appointments, found {appointments.Count()} in database");

        return appointments.Select(MapToContract);
    }

    public async Task<IEnumerable<Appointment>> GetAppointmentsByUserId(long ownerId)
    {
        var sql = GetAppointmentsByUserIdSqlStatement();
        var appointments = await _query.QueryAsync<AppointmentDBEntity>(sql, new {OwnerId = ownerId});

        Log.Information($"Getting all appointments for owner Id {ownerId}, found {appointments.Count()} in database for this user.");

        return appointments.Select(MapToContract);
    }

    public async Task<Appointment> GetAppointmentById(long appointmentId)
    {
        var sql = GetAppointmentByIdSqlStatement();
        var appointment = await _query.QueryFirstOrDefaultAsync<AppointmentDBEntity>(sql, new {Id = appointmentId});

        if (appointment == null)
            throw new AppointmentNotFoundException(appointmentId);

        return MapToContract(appointment);
    }

    private static Appointment MapToContract(AppointmentDBEntity appointmentDbEntity)
    {
        return new Appointment()
        {
            Id = appointmentDbEntity.Id,
            PetId = appointmentDbEntity.PetId,
            PetName = appointmentDbEntity.PetName,
            PetPictureUrl = appointmentDbEntity.PictureUrl,
            OwnerId = appointmentDbEntity.OwnerId,
            OwnerDisplayName = string.IsNullOrEmpty(appointmentDbEntity.OwnerPreferredName) ? appointmentDbEntity.OwnerFirstName : appointmentDbEntity.OwnerPreferredName,
            VisitorId = appointmentDbEntity.VisitorId,
            VisitorDisplayName = string.IsNullOrEmpty(appointmentDbEntity.VisitorPreferredName) ? appointmentDbEntity.VisitorFirstName : appointmentDbEntity.VisitorPreferredName,
            AppointmentDateTimeUTC = appointmentDbEntity.AppointmentDateTimeUTC,
            AppointmentState = appointmentDbEntity.AppointmentState,
            LocationId = appointmentDbEntity.LocationId
        };
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
                 SELECT a.Id, a.AppointmentDateTimeUTC, a.OwnerId, a.VisitorId, a.PetId, a.LocationId, a.AppointmentState,
                 o.FirstName AS OwnerFirstName, o.PreferredName AS OwnerPreferredName, v.FirstName AS VisitorFirstName, v.PreferredName AS VisitorPreferredName,
                 p.PetName, p.PictureUrl
                 FROM Appointments a
                 LEFT JOIN Users o ON o.ID = a.OwnerId
                 LEFT JOIN Users v ON v.ID = a.VisitorId
                 LEFT JOIN Pets p ON p.ID = a.PetId";
    }

    private static string GetAppointmentsByUserIdSqlStatement()
    {
        return @"
                 SELECT a.Id, a.AppointmentDateTimeUTC, a.OwnerId, a.VisitorId, a.PetId, a.LocationId, a.AppointmentState,
                 o.FirstName AS OwnerFirstName, o.PreferredName AS OwnerPreferredName, v.FirstName AS VisitorFirstName, v.PreferredName AS VisitorPreferredName,
                 p.PetName, p.PictureUrl
                 FROM Appointments a
                 LEFT JOIN Users o ON o.ID = a.OwnerId
                 LEFT JOIN Users v ON v.ID = a.VisitorId
                 LEFT JOIN Pets p ON p.ID = a.PetId
                 WHERE a.OwnerId = @OwnerId";
    }

    private static string GetAppointmentByIdSqlStatement()
    {
        return @"
                 SELECT a.Id, a.AppointmentDateTimeUTC, a.OwnerId, a.VisitorId, a.PetId, a.LocationId, a.AppointmentState,
                 o.FirstName AS OwnerFirstName, o.PreferredName AS OwnerPreferredName, v.FirstName AS VisitorFirstName, v.PreferredName AS VisitorPreferredName,
                 p.PetName, p.PictureUrl
                 FROM Appointments a
                 LEFT JOIN Users o ON o.ID = a.OwnerId
                 LEFT JOIN Users v ON v.ID = a.VisitorId
                 LEFT JOIN Pets p ON p.ID = a.PetId
                 WHERE a.Id = @Id";
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
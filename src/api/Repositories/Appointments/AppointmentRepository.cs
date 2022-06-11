using pet.Repositories;

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
        var results = await _query.QueryAsync<Appointment>(sqlStatement);
        return results;
    }

    public Task<IEnumerable<Appointment>> GetAppointmentsByUserId()
    {
        throw new NotImplementedException();
    }

    private static string GetAllAppointmentsSqlStatement()
    {
        return $@"SELECT Id, AppointmentDateTimeUTC, OwnerId, VisitorId, PetId, LocationId, AppointmentState FROM Appointments";
    }
}

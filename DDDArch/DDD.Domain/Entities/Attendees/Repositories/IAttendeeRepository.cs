namespace DDD.Domain.Entities.Attendees;

public interface IAttendeeRepository
{
    Task AddAsync(Attendee attendee);
}
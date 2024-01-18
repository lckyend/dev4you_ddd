using DDD.Domain.Entities.Gatherings;

namespace DDD.Domain.Entities.Attendees;

public class Attendee
{
    public Guid GatheringId { get; private set; }
    public Guid MemberId { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }

    internal Attendee(Invitation invitation)
    {
        GatheringId = invitation.GatheringId;
        MemberId = invitation.MemberId;
        CreatedOnUtc = DateTime.UtcNow;
    }
}
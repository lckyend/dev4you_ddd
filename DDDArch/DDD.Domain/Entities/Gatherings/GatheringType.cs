namespace DDD.Domain.Entities.Gatherings;

public enum GatheringType
{
    WithFixedNumberOfAttendees,
    MaximumNumberOfAttendees,
    WithExpirationForInvitations,
    InvitationsExpireAtUtc,
}
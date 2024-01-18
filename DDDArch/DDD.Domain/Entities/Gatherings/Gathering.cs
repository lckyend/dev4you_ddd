using DDD.Domain.Base;
using DDD.Domain.Entities.Attendees;
using DDD.Domain.Entities.Gatherings.Exceptions;
using DDD.Domain.Entities.Invitations;
using DDD.Domain.Entities.Invitations.DomainEvents;
using DDD.Domain.Entities.Members;

namespace DDD.Domain.Entities.Gatherings;

public sealed class Gathering : AggregateRoot
{
    private Gathering(
        Guid id,
        Member creator,
        GatheringType type,
        DateTime scheduledAtUtc,
        string name,
        string? location) : base(id)
    {
        Creator = creator;
        Type = type;
        ScheduledAtUtc = scheduledAtUtc;
        Name = name;
        Location = location;
    }

    private readonly List<Attendee> _attendees = new();
    private readonly List<Invitation> _invitations = new();

    public Member Creator { get; private set; }
    public GatheringType Type { get; private set; }
    public DateTime ScheduledAtUtc { get; private set; }
    public string Name { get; private set; }
    public string Location { get; private set; }
    public int? MaximumNumberOfAttendees { get; private set; }
    public DateTime? InvitationsExpireAtUtc { get; private set; }
    public int NumberOfAttendees { get; private set; }

    public IReadOnlyCollection<Attendee> Attendees => _attendees;

    public IReadOnlyCollection<Invitation> Invitations => _invitations;

    public static Gathering Create(
        Guid id,
        Member creator,
        GatheringType type,
        DateTime scheduledAtUtc,
        string name,
        string? location,
        int? maximumNumberOfAttendees,
        int? invitationValidBeforeHours)
    {
        var gathering = new Gathering(
            Guid.NewGuid(),
            creator,
            type,
            scheduledAtUtc,
            name,
            location);

        switch (gathering.Type)
        {
            case GatheringType.WithFixedNumberOfAttendees:
                if (maximumNumberOfAttendees is null)
                {
                    throw new GatheringMaximumNumberOfAttendeesIsNullDomainException("");
                }

                gathering.MaximumNumberOfAttendees = maximumNumberOfAttendees;
                break;
            case GatheringType.WithExpirationForInvitations:
                if (invitationValidBeforeHours is null)
                {
                    throw new GatheringMaximumNumberOfAttendeesIsNullDomainException("");
                }

                gathering.InvitationsExpireAtUtc =
                    gathering.ScheduledAtUtc.AddHours(-invitationValidBeforeHours.Value);
                break;

            default:
                throw new ArgumentNullException();
        }

        return gathering;
    }

    public Invitation SendInvitation(Member member)
    {
        if (Creator.Id == member.Id)
        {
            throw new Exception();
        }

        if (ScheduledAtUtc < DateTime.UtcNow)
        {
            throw new Exception();
        }
        
        var intivation = new Invitation(Guid.NewGuid(), member, this);
        _invitations.Add(intivation);

        return intivation;
    }

    public Attendee? AcceptInvitation(Invitation invitation)
    {
        var exprired = (Type == GatheringType.WithFixedNumberOfAttendees &&
                        NumberOfAttendees == MaximumNumberOfAttendees) ||
                       (Type == GatheringType.WithExpirationForInvitations &&
                        InvitationsExpireAtUtc < DateTime.UtcNow);

        if (exprired)
        {
            invitation.Expire();
            return null;
        }
        
        var attendee = invitation.Accept();
        
        RaiseDomainEvent(new InvitationAcceptedDomainEvent(invitation.Id, Id));
        
        _attendees.Add(attendee);
        NumberOfAttendees++;
        
        return attendee;
    }
}
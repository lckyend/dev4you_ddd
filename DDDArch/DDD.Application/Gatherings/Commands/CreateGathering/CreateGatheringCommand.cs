using DDD.Domain.Entities.Gatherings;
using MediatR;

namespace DDD.Application.Gatherings.Commands.CreateGathering;

public record CreateGatheringCommand(
    Guid MemberId,
    GatheringType Type,
    DateTime ScheduledAtUtc,
    string Name,
    string? Location,
    int? MaximumNumberOfAttendees,
    int? InvitationsValidBeforeInHours) : IRequest;

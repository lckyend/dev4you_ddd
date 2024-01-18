using MediatR;

namespace DDD.Application.Invitations.Commands.SendInvitation;

public record SendInvitationCommand(Guid MemberId, Guid GatheringId) : IRequest;
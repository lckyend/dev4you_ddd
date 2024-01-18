using MediatR;

namespace DDD.Application.Invitations.Commands.AcceptInvitation;

public sealed record AcceptInvitationCommand(Guid GatheringId ,Guid Invitationid) : IRequest;

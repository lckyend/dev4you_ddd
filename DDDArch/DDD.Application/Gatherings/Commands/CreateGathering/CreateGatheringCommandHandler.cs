using System.Security.Cryptography;
using DDD.Domain.Entities.Gatherings;
using DDD.Domain.Entities.Members.Repositories;
using MediatR;

namespace DDD.Application.Gatherings.Commands.CreateGathering;

internal sealed class CreateGatheringCommandHandler : IRequestHandler<CreateGatheringCommand>
{
    private readonly IMemberRepository _memberRepository;

    public CreateGatheringCommandHandler(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task Handle(CreateGatheringCommand request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(request.MemberId, cancellationToken);

        if (member is null)
            return;

        var gathering = Gathering.Create(Guid.NewGuid(), member, request.Type, request.ScheduledAtUtc, request.Name, request.Location, request.MaximumNumberOfAttendees, request.InvitationsValidBeforeInHours);
    }
}
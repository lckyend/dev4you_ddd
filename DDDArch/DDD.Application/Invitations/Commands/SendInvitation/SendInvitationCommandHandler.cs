using System.Data.Common;
using DDD.Application.Abstractions;
using DDD.Domain;
using DDD.Domain.Entities.Gatherings.Repositories;
using DDD.Domain.Entities.Invitations;
using DDD.Domain.Entities.Invitations.Repositories;
using DDD.Domain.Entities.Members.Repositories;
using MediatR;

namespace DDD.Application.Invitations.Commands.SendInvitation;

internal sealed class SendInvitationCommandHandler : IRequestHandler<SendInvitationCommand>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IGatheringRepository _gatheringRepository;
    private readonly IInvitationRepository _invitationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;

    public SendInvitationCommandHandler(
        IMemberRepository memberRepository, 
        IGatheringRepository gatheringRepository, 
        IInvitationRepository invitationRepository, 
        IUnitOfWork unitOfWork, 
        IEmailService emailService)
    {
        _memberRepository = memberRepository;
        _gatheringRepository = gatheringRepository;
        _invitationRepository = invitationRepository;
        _unitOfWork = unitOfWork;
        _emailService = emailService;
    }

    public async Task Handle(SendInvitationCommand request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(request.MemberId, cancellationToken);
        var gathering = await _gatheringRepository.GetByIdWithCreatorAsync(request.GatheringId, cancellationToken);

        if (member is null || gathering is null)
        {
            return;
        }

        var invitation = gathering.SendInvitation(member);
    }
}
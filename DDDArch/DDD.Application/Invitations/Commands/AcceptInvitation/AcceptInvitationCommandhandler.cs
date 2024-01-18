using DDD.Application.Abstractions;
using DDD.Domain;
using DDD.Domain.Entities.Attendees;
using DDD.Domain.Entities.Gatherings;
using DDD.Domain.Entities.Gatherings.Repositories;
using DDD.Domain.Entities.Invitations;
using DDD.Domain.Entities.Invitations.Repositories;
using DDD.Domain.Entities.Members.Repositories;
using MediatR;

namespace DDD.Application.Invitations.Commands.AcceptInvitation;

internal sealed class AcceptInvitationCommandhandler : IRequestHandler<AcceptInvitationCommand>
{
    private readonly IInvitationRepository _invitationRepository;
    private readonly IMemberRepository _memberRepository;
    private readonly IGatheringRepository _gatheringRepository;
    private readonly IAttendeeRepository _attendeeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;

    public AcceptInvitationCommandhandler(IInvitationRepository invitationRepository, IMemberRepository memberRepository, IGatheringRepository gatheringRepository, IAttendeeRepository attendeeRepository, IUnitOfWork unitOfWork, IEmailService emailService)
    {
        _invitationRepository = invitationRepository;
        _memberRepository = memberRepository;
        _gatheringRepository = gatheringRepository;
        _attendeeRepository = attendeeRepository;
        _unitOfWork = unitOfWork;
        _emailService = emailService;
    }

    public async Task Handle(AcceptInvitationCommand request, CancellationToken cancellationToken)
    {
        var gathering = await _gatheringRepository.GetByIdWithCreatorAsync(request.GatheringId, cancellationToken);

        if(gathering is null)
            return;

        var invitation = gathering.Invitations
            .FirstOrDefault(prp => prp.Id == request.Invitationid);
        
        if (invitation is null || invitation.Status != InvitationStatus.Pending)
        {
            return;
        }

        var attendee = gathering.AcceptInvitation(invitation);

        if (attendee is not null)
        {
            await _attendeeRepository.AddAsync(attendee);
        }
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
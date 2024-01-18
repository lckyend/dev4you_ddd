using DDD.Domain.Entities.Gatherings;
using DDD.Domain.Entities.Members;

namespace DDD.Application.Abstractions;

public interface IEmailService
{
    Task SendInvitationAcceptedEmailAsync(Gathering gathering, CancellationToken cancellationToken);
    Task SendWelcomeEmailAsync(Member member, CancellationToken cancellationToken);
}
using DDD.Domain;
using DDD.Domain.Entities.Members;
using DDD.Domain.Entities.Members.Repositories;
using DDD.Domain.Entities.Members.ValueObject;
using MediatR;

namespace DDD.Application.Members.CreateMember;

internal sealed class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMemberCommandHandler(
        IMemberRepository memberRepository, 
        IUnitOfWork unitOfWork)
    {
        _memberRepository = memberRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Email);

        if (!await _memberRepository.IsEmailUniqueAsync(email.Value, cancellationToken))
            throw new Exception("Email already in use");
        
        var member = Member.CreateMember(
            Guid.NewGuid(),
            email,
            FirstName.Create(request.FirstName),
            LastName.Create(request.LastName)
           );

        await _memberRepository.AddAsync(member);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
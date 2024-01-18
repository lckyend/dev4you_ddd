namespace DDD.Domain.Entities.Members.Repositories;

public interface IMemberRepository
{
    Task<Member> GetByIdAsync(Guid MemberId, CancellationToken cancellationToken);
    Task AddAsync(Member member);
    Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken);
}
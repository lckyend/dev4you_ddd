using DDD.Domain.Entities.Members.ValueObject;

namespace DDD.Domain.Entities.Members;

public sealed class Member : Entity
{
    private Member(Guid id,
        Email email,
        FirstName firstName,
        LastName lastName
        ) : base(id)
    {
        Id = id;
        LastName = lastName;
        Email = email;
        FirstName = firstName;
    }
    public Guid Id { get; private set; }
    public FirstName FirstName { get; set; }
    public LastName LastName { get; set; }
    public Email Email { get; set; }

    public static Member CreateMember(Guid id,
        Email email,
        FirstName firstName,
        LastName lastName
    )
    {
        var member = new Member(
            Guid.NewGuid(),
            email,
            firstName,
            lastName
        );

        return member;
    }
}
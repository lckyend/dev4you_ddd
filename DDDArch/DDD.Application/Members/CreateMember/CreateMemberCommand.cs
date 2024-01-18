using MediatR;

namespace DDD.Application.Members.CreateMember;

public sealed record CreateMemberCommand(string Email, string FirstName, string LastName) : IRequest;
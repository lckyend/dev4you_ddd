namespace DDD.Domain.Entities.Gatherings.Exceptions;

public sealed class GatheringMaximumNumberOfAttendeesIsNullDomainException : DomainException
{
    public GatheringMaximumNumberOfAttendeesIsNullDomainException(string message) : base(message)
    {
        
    }
}
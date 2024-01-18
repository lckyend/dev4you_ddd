namespace DDD.Domain.Entities.Gatherings.Repositories;

public interface IGatheringRepository
{
    Task<Gathering> GetByIdWithCreatorAsync(Guid gatheringId, CancellationToken cancellationToken);
}
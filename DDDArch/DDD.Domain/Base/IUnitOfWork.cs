namespace DDD.Domain;

public interface IUnitOfWork // nie chciałem robić dodatkowe foldery
{
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
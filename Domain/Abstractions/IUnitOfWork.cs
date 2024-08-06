namespace Domain.Abstractions;

public interface IUnitOfWork : IDisposable
{
    IProductRepository ProductRepository { get; }
    ITypeRepository TypeRepository { get; }
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
    Task<int> SaveChangesAsync();
}
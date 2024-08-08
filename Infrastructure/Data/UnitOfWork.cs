using Domain.Abstractions;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly TopZoneContext _context;
    private IDbContextTransaction _transaction;

    public IProductRepository ProductRepository { get; private set; }
    public ITypeRepository TypeRepository { get; private set; }
    public ITypeProductRepository TypeProductRepository { get; private set; }

    public UnitOfWork(TopZoneContext context, 
        IProductRepository productRepository, 
        ITypeRepository typeRepository,
		ITypeProductRepository typeProductRepository
		)
    {
        _context = context;
        ProductRepository = productRepository;
        TypeRepository = typeRepository;
		TypeProductRepository = typeProductRepository;
	}

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();
        }
        catch
        {
            await RollbackAsync();
            throw;
        }
        finally
        {
            _transaction.Dispose();
        }
    }

    public async Task RollbackAsync()
    {
        await _transaction.RollbackAsync();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}

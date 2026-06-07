

namespace Infrastructure.Repositories;

public class TokenRepository : RepositoryBase<RefreshToken>, ITokenRepository
{
    private readonly IUnitOfWork _unitOfWork;

    public TokenRepository(TopZoneContext topZoneContext,
        IUnitOfWork unitOfWork) 
        : base(topZoneContext)
    {
        _unitOfWork = unitOfWork;

    }

    public async Task<RefreshToken> GetByToken(string token)
    {
        return await DbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);
    }

    public async Task AddAsync(RefreshToken token)
    {
        await DbContext.RefreshTokens.AddAsync(token);
        await DbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(RefreshToken token)
    {
        DbContext.RefreshTokens.Update(token);
        await DbContext.SaveChangesAsync();
    }

    public async Task RevokeAsync(string token)
    {
        RefreshToken? tokenEntity = await DbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);
        if (tokenEntity != null && !tokenEntity.IsRevoked)
        {
            tokenEntity.IsRevoked = true;
            DbContext.RefreshTokens.Update(tokenEntity);
            await DbContext.SaveChangesAsync();
        }
    }
}

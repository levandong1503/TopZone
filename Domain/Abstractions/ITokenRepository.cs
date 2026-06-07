using Domain.Entities;

namespace Domain.Abstractions;

public interface ITokenRepository : IRepositoryBase<RefreshToken>
{
    Task<RefreshToken> GetByToken(string token);
    Task AddAsync(RefreshToken token);
    Task RevokeAsync(string token);
    Task UpdateAsync(RefreshToken token);
}

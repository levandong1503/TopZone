namespace Application.Interface;

public interface ITokenService
{
    Task<string> GenerateAccessTokenAsync(ApplicationUser user);
    Task<(string AccessToken, string RefreshToken)> GenerateTokensAsync(ApplicationUser user, string? deviceInfo = null);
    Task<bool> RevokeRefreshTokenAsync(string refreshToken);
    Task<RefreshToken?> GetRefreshTokenAsync(string refreshToken);
    Task UpdateRefreshTokenAsync(RefreshToken refreshToken);
}
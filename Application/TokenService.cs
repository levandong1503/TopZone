namespace Application;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly TopZoneContext _context;
    private readonly ITokenRepository _tokenRepository;

    public TokenService(IConfiguration configuration, TopZoneContext context, ITokenRepository tokenRepository)
    {
        _configuration = configuration;
        _context = context;
        _tokenRepository = tokenRepository;
    }

    public async Task<string> GenerateAccessTokenAsync(ApplicationUser user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
        };

        if (user.Role != null && !string.IsNullOrWhiteSpace(user.Role.Name))
        {
            claims.Add(new Claim(ClaimTypes.Role, user.Role.Name));
        }

        string? issuer = _configuration["JWT:Issuer"];
        string? audience = _configuration["JWT:Audience"];
        string? secret = _configuration["JWT:Secret"];
        int accessMinutes;
        if (!int.TryParse(_configuration["JWT:AccessTokenMinutes"], out accessMinutes))
        {
            accessMinutes = 15;
        }

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(accessMinutes),
            signingCredentials: creds
        );

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(token);
    }

    public async Task<(string AccessToken, string RefreshToken)> GenerateTokensAsync(ApplicationUser user, string? deviceInfo = null)
    {
        string accessToken = await GenerateAccessTokenAsync(user);

        // Create refresh token as JWT with claim typ=refresh
        string? issuer = _configuration["JWT:Issuer"];
        string? audience = _configuration["JWT:Audience"];
        string? secret = _configuration["JWT:Secret"];
        int refreshDays;
        if (!int.TryParse(_configuration["JWT:RefreshTokenDays"], out refreshDays))
        {
            refreshDays = 7;
        }

        List<Claim> refreshClaims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim("typ", "refresh"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        SymmetricSecurityKey refreshKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        SigningCredentials refreshCreds = new SigningCredentials(refreshKey, SecurityAlgorithms.HmacSha256Signature);

        JwtSecurityToken refreshToken = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: refreshClaims,
            expires: DateTime.UtcNow.AddDays(refreshDays),
            signingCredentials: refreshCreds
        );

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        string refreshTokenString = handler.WriteToken(refreshToken);

        // Persist refresh token in DB
        RefreshToken rtEntity = new RefreshToken
        {
            Token = refreshTokenString,
            ApplicationUserId = user.UserId,
            ExpiresAt = refreshToken.ValidTo,
            IsRevoked = false,
            DeviceInfo = deviceInfo,
            CreatedAt = DateTime.UtcNow
        };

        await _tokenRepository.AddAsync(rtEntity);
        return (accessToken, refreshTokenString);
    }

    public async Task<bool> RevokeRefreshTokenAsync(string refreshToken)
    {
        RefreshToken? tokenEntity = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == refreshToken);
        if (tokenEntity == null || tokenEntity.IsRevoked)
        {
            return false;
        }

        await _tokenRepository.RevokeAsync(refreshToken);
        return true;
    }

        public async Task<RefreshToken?> GetRefreshTokenAsync(string refreshToken)
        {
            return await _tokenRepository.GetByToken(refreshToken);
    }

    public async Task UpdateRefreshTokenAsync(RefreshToken refreshToken)
    {
        await _tokenRepository.UpdateAsync(refreshToken);
    }
}

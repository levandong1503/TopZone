namespace TopZone.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration configuration;
    private IPasswordHashService _passwordHashService;
    private readonly IUserService userService;
    private readonly ITokenService _tokenService;
    public AuthController(IConfiguration configuration, IUserService userService,
        IPasswordHashService passwordHashService,
        ITokenService tokenService)
    {
        this.configuration = configuration;
        this.userService = userService;
        _passwordHashService = passwordHashService;
        _tokenService = tokenService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult Get()
    {

        return Ok(User.Identity.Name);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        ApplicationUser? userLogin = await userService.GetByEmailAsync(request.Email);
        if (userLogin == null)
        {
            return BadRequest("Invalid user");
        }

        bool isValidPassword = _passwordHashService.VerifyPassword(request.Password, userLogin.PasswordHash);
        if (!isValidPassword)
        {
            return BadRequest("Invalid password");
        }

        (string accessToken, string refreshToken) = await _tokenService.GenerateTokensAsync(userLogin, deviceInfo: Request.Headers["User-Agent"].ToString());

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        JwtSecurityToken parsedAccess = handler.ReadJwtToken(accessToken);
        JwtSecurityToken parsedRefresh = handler.ReadJwtToken(refreshToken);

        TokenResponse response = new TokenResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            AccessTokenExpiresAt = parsedAccess.ValidTo,
            RefreshTokenExpiresAt = parsedRefresh.ValidTo
        };

        return Ok(response);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        RefreshToken? tokenEntity = await _tokenService.GetRefreshTokenAsync(request.RefreshToken);
        if (tokenEntity == null || tokenEntity.IsRevoked || tokenEntity.ExpiresAt <= DateTime.UtcNow)
        {
            return BadRequest("Invalid or expired refresh token");
        }

        // Validate refresh token JWT signature and claims
        string? secret = configuration["JWT:Secret"];
        TokenValidationParameters validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = configuration["JWT:Issuer"],
            ValidAudience = configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
            ClockSkew = TimeSpan.Zero
        };

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        try
        {
            handler.ValidateToken(request.RefreshToken, validationParameters, out SecurityToken validatedToken);
        }
        catch (Exception)
        {
            return BadRequest("Invalid refresh token");
        }

        // Get user
        ApplicationUser? user = await userService.GetByIdAsync(tokenEntity.ApplicationUserId);
        if (user == null)
        {
            return BadRequest("Invalid token owner");
        }

        // Revoke old refresh token
        tokenEntity.IsRevoked = true;
        await _tokenService.RevokeRefreshTokenAsync(request.RefreshToken);

        // Issue new pair
        (string newAccess, string newRefresh) = await _tokenService.GenerateTokensAsync(user, request.DeviceInfo);

        JwtSecurityToken parsedAccess = handler.ReadJwtToken(newAccess);
        JwtSecurityToken parsedRefresh = handler.ReadJwtToken(newRefresh);

        TokenResponse response = new TokenResponse
        {
            AccessToken = newAccess,
            RefreshToken = newRefresh,
            AccessTokenExpiresAt = parsedAccess.ValidTo,
            RefreshTokenExpiresAt = parsedRefresh.ValidTo
        };

        return Ok(response);
    }

    [HttpPost("revoke")]
    [Authorize]
    public async Task<IActionResult> Revoke([FromBody] RefreshRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        bool result = await _tokenService.RevokeRefreshTokenAsync(request.RefreshToken);
        if (!result)
        {
            return BadRequest("Token not found or already revoked");
        }

        return Ok("Revoked");
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserRequest request)
    {
        // 1. Validate model
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // 2. Check Email tồn tại
        var emailExists = await userService.ExistsEmail(request.Email);
        if (emailExists)
            return BadRequest("Email already exists");

        // 3. Hash password
        var passwordHash = _passwordHashService.HashPassword(request.Password);
        // 4. Tạo user
        var user = new ApplicationUser
        {
            PasswordHash = passwordHash,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Address = request.Address
        };
        // 5. Save DB
        await userService.Add(user);
        return Ok("Register success");
    }
}

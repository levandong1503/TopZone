using Application;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TopZone.Dtos;

namespace TopZone.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration configuration;
    private readonly TopZoneContext _context;
    private readonly string[] standardUsers = { "sa", "a", "readingUser", "admin" };
    private IPasswordHashService _passwordHashService;
    public AuthController(IConfiguration configuration, TopZoneContext topZoneContext, IPasswordHashService passwordHashService)
    {
        this.configuration = configuration;
        _context = topZoneContext;
        _passwordHashService = passwordHashService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult Get()
    {

        return Ok(User.Identity.Name);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginUserRequest user)
    {
        var userLogin = _context.Users.FirstOrDefault(u => u.Email == user.Email);
        if (userLogin == null)
        {
            return BadRequest("Invalid user");
        }

        var isValidPassword = _passwordHashService.VerifyPassword(user.Password, userLogin.PasswordHash);
        if (!isValidPassword)
        {
            return BadRequest("Invalid password");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(configuration["JWT:Secret"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            // cau hinh cho token
            Subject = new ClaimsIdentity(new Claim[]
           {
                        new Claim(ClaimTypes.Name, user.Email),
                        user.Email == "sa@gmail.com"
                        ? new Claim(ClaimTypes.Role, "Admin")
                        : new Claim(ClaimTypes.Role, "Standard"),
           }),
            // thoi gian hoat dong cua token
            Expires = DateTime.UtcNow.AddDays(1),
            // thuat toan ma hoa
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Audience = configuration["JWT:Audience"],
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        return Ok(new { Token = tokenString });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserRequest request)
    {
        // 1. Validate model
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // 2. Check UserName tồn tại
        //var userNameExists = await _context.Users
        //    .AnyAsync(x => x.UserName == request.UserName);

        //if (userNameExists)
        //{
        //    return BadRequest("UserName already exists");
        //}

        // 3. Check Email tồn tại
        var emailExists = await _context.Users
            .AnyAsync(x => x.Email == request.Email);

        if (emailExists)
        {
            return BadRequest("Email already exists");
        }

        // 4. Hash password
        var passwordHash = _passwordHashService.HashPassword(request.Password);

        // 5. Tạo user
        var user = new ApplicationUser
        {
            PasswordHash = passwordHash,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Address = request.Address
        };

        // 6. Save DB
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok("Register success");
    }
}

namespace Application.Interface;

public interface IUserService
{
    Task<ApplicationUser> GetByEmailAsync(string email);
    Task<ApplicationUser> GetByIdAsync(Guid id);
    Task<bool> Add(ApplicationUser user);
    Task<bool> ExistsEmail(string email);
}

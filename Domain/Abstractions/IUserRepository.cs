using Domain.Entities;

namespace Application.Interface;

public interface IUserRepository
{
    Task<bool> Add(ApplicationUser user);
    Task<ApplicationUser> GetByEmailAsync(string email);
    Task<bool> ExistsEmail(string email);
    Task<ApplicationUser> GetById(Guid id);
    Task<Role> GetRoleOfUser(Guid id);
}

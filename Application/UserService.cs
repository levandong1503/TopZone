namespace Application;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<ApplicationUser> GetByEmailAsync(string email)
    {
        return await userRepository.GetByEmailAsync(email);
    }

    public async Task<ApplicationUser> GetByIdAsync(Guid id)
    {
        return await userRepository.GetById(id);
    }
    
    public async Task<bool> Add(ApplicationUser user)
    {
        return await userRepository.Add(user);
    }

    public async Task<bool> ExistsEmail(string email)
    {
        return await userRepository.ExistsEmail(email);
    }

    public async Task<string> GetRoleOfUser(string email)
    {
        var user = await userRepository.GetByEmailAsync(email);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        return user.Role.Name;
    }
}

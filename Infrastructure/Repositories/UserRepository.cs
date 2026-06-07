using Application.Interface;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Infrastructure.Repositories;

public class UserRepository : RepositoryBase<ApplicationUser>, IUserRepository
{
    private readonly TopZoneContext context;

    public UserRepository(TopZoneContext _context) : base(_context)
    {
        context = _context;
    }

    public async Task<bool> Add(ApplicationUser user)
    {
        var entityEntry = await context.Users.AddAsync(user);
        var changeNumber = await context.SaveChangesAsync();
        return entityEntry != null && changeNumber > 0;
    }

    public async Task<ApplicationUser> GetByEmailAsync(string email)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<bool> ExistsEmail(string email)
    {
        return await context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<ApplicationUser> GetById(Guid id)
    {
        return await context.Users.FindAsync(id);
    }

    public Role GetRoleOfUser()
    {
        
        // Implement logic to get the role of the user
        throw new NotImplementedException();
    }

    public Task<Role> GetRoleOfUser(Guid id)
    {
        //_context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
        return null;
    }
}

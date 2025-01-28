using Microsoft.EntityFrameworkCore;
using TechInterviewTask.Entities;

namespace TechInterviewTask.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User> AddAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users
            .FromSqlRaw("SELECT * FROM users WHERE id = {0}", id)
            .FirstOrDefaultAsync();
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users
            .FromSqlRaw("SELECT * FROM users")
            .ToListAsync();
    }
    public async Task<bool> UpdateAsync(User user)
    {
        if (!_context.Users.Local.Any(u => u.Id == user.Id))
        {
            _context.Users.Attach(user);
        }

        _context.Entry(user).State = EntityState.Modified;
        var changes = await _context.SaveChangesAsync();
        return changes > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existingUser = await _context.Users.FindAsync(id);
        if (existingUser == null)
            return false;

        _context.Users.Remove(existingUser);
        var changes = await _context.SaveChangesAsync();
        return changes > 0;
    }
}
using TechInterviewTask.Entities;

namespace TechInterviewTask.Repositories;

public interface IUserRepository
{
    Task<User> AddAsync(User user);
    Task<User?> GetByIdAsync(int id);
    Task<List<User>> GetAllAsync();
    Task<bool> UpdateAsync(User user);
    Task<bool> DeleteAsync(int id);
}
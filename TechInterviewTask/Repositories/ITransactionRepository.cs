using TechInterviewTask.Entities;

namespace TechInterviewTask.Repositories;

public interface ITransactionRepository
{
    Task<Transaction> AddAsync(Transaction transaction);
    Task<Transaction?> GetByIdAsync(int id);
    Task<List<Transaction>> GetAllAsync();
    Task<bool> UpdateAsync(Transaction transaction);
    Task<bool> DeleteAsync(int id);
}
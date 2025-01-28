using Microsoft.EntityFrameworkCore;
using TechInterviewTask.Entities;

namespace TechInterviewTask.Repositories.Implementations;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Transaction> AddAsync(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }

    public async Task<Transaction?> GetByIdAsync(int id)
    {
        return await _context.Transactions
            .FromSqlRaw("SELECT * FROM transactions WHERE id = {0}", id)
            .FirstOrDefaultAsync();
    }
    
    public async Task<List<Transaction>> GetAllAsync()
    {
        return await _context.Transactions
            .FromSqlRaw("SELECT * FROM transactions")
            .ToListAsync();
    }

    public async Task<bool> UpdateAsync(Transaction transaction)
    {
        if (!_context.Transactions.Local.Any(t => t.Id == transaction.Id))
        {
            _context.Transactions.Attach(transaction);
        }

        _context.Entry(transaction).State = EntityState.Modified;
        var changes = await _context.SaveChangesAsync();
        return changes > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existingTransaction = await _context.Transactions.FindAsync(id);
        if (existingTransaction == null)
            return false;

        _context.Transactions.Remove(existingTransaction);
        var changes = await _context.SaveChangesAsync();
        return changes > 0;
    }
}
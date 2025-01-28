using TechInterviewTask.DTOs;
using TechInterviewTask.Repositories;
using Microsoft.AspNetCore.Mvc;
using TechInterviewTask.Entities;

namespace TechInterviewTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITransactionRepository _transactionRepository;

        public WalletController(IUserRepository userRepository, ITransactionRepository transactionRepository)
        {
            _userRepository = userRepository;
            _transactionRepository = transactionRepository;
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferRequest request)
        {
            if (request.FromUserId == request.ToUserId)
            {
                return BadRequest("Cannot transfer to the same user.");
            }

            var fromUser = await _userRepository.GetByIdAsync(request.FromUserId);
            if (fromUser == null)
            {
                return NotFound($"User with ID {request.FromUserId} not found.");
            }

            var toUser = await _userRepository.GetByIdAsync(request.ToUserId);
            if (toUser == null)
            {
                return NotFound($"User with ID {request.ToUserId} not found.");
            }

            var transaction = new Transaction
            {
                FromUserId = request.FromUserId,
                ToUserId = request.ToUserId,
                Amount = request.Amount,
                Currency = request.Currency
            };

            await _transactionRepository.AddAsync(transaction);

            return Ok(new
            {
                Message = "Transfer completed successfully.",
                TransactionId = transaction.Id
            });
        }

        [HttpGet("{userId}/balance")]
        public async Task<IActionResult> GetUserBalance(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"User with ID {userId} not found.");
            }

            var allTransactions = await _transactionRepository.GetAllAsync();

            var totalIncoming = allTransactions
                .Where(t => t.ToUserId == userId)
                .Sum(t => t.Amount);

            var totalOutgoing = allTransactions
                .Where(t => t.FromUserId == userId)
                .Sum(t => t.Amount);

            var balance = totalIncoming - totalOutgoing;

            return Ok(new { UserId = userId, Balance = balance });
        }
    }
}
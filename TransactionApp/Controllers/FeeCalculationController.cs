using Domain.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;

namespace TransactionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeeCalculationController : ControllerBase
    {
        private readonly IFeeCalculationService _feeCalculationService;

        public FeeCalculationController(IFeeCalculationService feeCalculationService)
        {
            _feeCalculationService = feeCalculationService;
        }

       
        [HttpPost("calculate")]
        public async Task<IActionResult> CalculateFee([FromBody] TransactionDTO transaction)
        {
            if (transaction == null)
                return BadRequest("Transaction data is required.");

            var result = await _feeCalculationService.CalculateFeeAsync(transaction);
            return Ok(result);
        }

        
        [HttpPost("calculate-batch")]
        public async Task<IActionResult> CalculateFeesBatch([FromBody] IEnumerable<TransactionDTO> transactions)
        {
            if (transactions == null || !transactions.Any())
                return BadRequest("At least one transaction is required.");

            var results = await _feeCalculationService.CalculateFeesBatchAsync(transactions);
            return Ok(results);
        }

       
        [HttpGet("logs")]
        public async Task<IActionResult> GetAllFeeLogs()
        {
            var logs = await _feeCalculationService.GetFeeLogs();
            return Ok(logs);
        }
    }
}

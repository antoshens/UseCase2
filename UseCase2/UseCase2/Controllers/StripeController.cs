using Microsoft.AspNetCore.Mvc;
using Stripe;
using UseCase2.Models;
using UseCase2.Services.Interfaces;

namespace UseCase2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeController : ControllerBase
    {
        private readonly IBalanceService _balanceService;
        private readonly IBalanceTransactionService _balanceTransactionService;

        public StripeController(StripeConfigurationOptions stripeConfig, IBalanceService balanceService, IBalanceTransactionService balanceTransactionService)
        {
            StripeConfiguration.ApiKey = stripeConfig.ApiKey;
            _balanceService = balanceService;
            _balanceTransactionService = balanceTransactionService;
        }

        [HttpGet("Balance")]
        public ActionResult<Balance> GetBalance()
        {
            try
            {
                var balance = _balanceService.Get();

                if (balance != null)
                {
                    return Ok(balance);
                }

                return NotFound();
            }
            catch (StripeException ex)
            {
                return StatusCode((int)ex.HttpStatusCode, ex.Message);
            }
        }

        [HttpGet("BalanceTransactions")]
        public ActionResult<StripeBalanceResponse> GetBalanceTransactions(string startingAfter, int? take)
        {
            try
            {
                var options = new BalanceTransactionListOptions
                {
                    Limit = take ?? 10,
                    StartingAfter = startingAfter
                };

                var balanceTransactions = _balanceTransactionService.List(options);

                return new StripeBalanceResponse
                {
                    Status = "Success",
                    Message = "Balance Retrieved",
                    Data = balanceTransactions
                };
            }
            catch (StripeException e)
            {
                return new StripeBalanceResponse
                {
                    Status = "Error",
                    Message = e.Message
                };
            }
        }
    }
}

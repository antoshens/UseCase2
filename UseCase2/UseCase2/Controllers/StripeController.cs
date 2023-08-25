using Microsoft.AspNetCore.Mvc;
using Stripe;
using UseCase2.Models;

namespace UseCase2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeController : ControllerBase
    {
        public StripeController(StripeConfigurationOptions stripeConfig)
        {
            StripeConfiguration.ApiKey = stripeConfig.ApiKey;
        }

        [HttpGet("Balance")]
        public IActionResult GetBalance()
        {
            try
            {
                var balanceService = new BalanceService();
                var balance = balanceService.Get();

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
        public ActionResult<StripeBalanceResponse> GetBalance(string startingAfter, int? take)
        {
            try
            {
                var balanceTransactionService = new BalanceTransactionService();
                var options = new BalanceTransactionListOptions
                {
                    Limit = take ?? 10,
                    StartingAfter = startingAfter
                };

                var balanceTransactions = balanceTransactionService.List(options);

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

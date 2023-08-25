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

        [HttpGet("balance")]
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
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}

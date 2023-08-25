using Stripe;
using UseCase2.Services.Interfaces;

namespace UseCase2.Services
{
    public class BalanceService : IBalanceService
    {
        public Balance Get(RequestOptions requestOptions = null)
        {
            var balanceService = new Stripe.BalanceService();
            var balance = balanceService.Get();

            return balance;
        }
    }
}

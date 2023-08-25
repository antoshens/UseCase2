using Stripe;
using UseCase2.Services.Interfaces;

namespace UseCase2.Services
{
    public class BalanceTransactionService : IBalanceTransactionService
    {
        public StripeList<BalanceTransaction> List(BalanceTransactionListOptions options = null, RequestOptions requestOptions = null)
        {
            var balanceTransactionService = new Stripe.BalanceTransactionService();
            var balanceTransactions = balanceTransactionService.List(options);

            return balanceTransactions;
        }
    }
}

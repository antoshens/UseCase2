using Stripe;

namespace UseCase2.Services.Interfaces
{
    public interface IBalanceTransactionService
    {
        StripeList<BalanceTransaction> List(BalanceTransactionListOptions options = null, RequestOptions requestOptions = null);
    }
}

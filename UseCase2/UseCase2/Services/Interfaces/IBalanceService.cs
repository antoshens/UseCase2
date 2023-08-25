using Stripe;

namespace UseCase2.Services.Interfaces
{
    public interface IBalanceService
    {
        Balance Get(RequestOptions requestOptions = null);
    }
}

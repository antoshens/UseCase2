using Moq;
using Stripe;
using UseCase2.Models;
using UseCase2.Services.Interfaces;

namespace UseCase2.Tests.StripeController
{
    public class StripeController_BaseTest
    {
        protected Mock<IBalanceService> balanceServiceMock;
        protected Mock<IBalanceTransactionService> balanceTransactionServiceMock;
        protected StripeConfigurationOptions stripeConfig;

        public StripeController_BaseTest()
        {
            balanceServiceMock = new Mock<IBalanceService>();
            balanceTransactionServiceMock = new Mock<IBalanceTransactionService>();

            stripeConfig = new StripeConfigurationOptions {
                ApiKey = "ApiKey"
            };
        }

        protected void ConfigureStripeServicesWithSuccessResponse()
        {
            balanceServiceMock
               .Setup(_ => _.Get(It.IsAny<RequestOptions>()))
               .Returns(new Balance
               {
                   Livemode = true
               });

            balanceTransactionServiceMock
                .Setup(_ => _.List(It.IsAny<BalanceTransactionListOptions>(), It.IsAny<RequestOptions>()))
                .Returns(() =>
                {
                    var list = new StripeList<BalanceTransaction>();
                    list.Data = new List<BalanceTransaction>
                    {
                        new BalanceTransaction
                        {
                            Id = "1",
                            Object = "Object",
                            Amount = 10,
                            Currency = "USD",
                            Description = "Description"
                        }
                    };

                    return list;
                });
        }

        protected void ConfigureStripeServicesWithEmptyResponse()
        {
            balanceServiceMock
              .Setup(_ => _.Get(It.IsAny<RequestOptions>()))
              .Returns((Balance)null);
        }

        protected void ConfigureStripeServicesWithFailedResponse()
        {
            balanceServiceMock
                .Setup(_ => _.Get(It.IsAny<RequestOptions>()))
                .Throws(new StripeException("This is a stripe error!"));

            balanceTransactionServiceMock
                .Setup(_ => _.List(It.IsAny<BalanceTransactionListOptions>(), It.IsAny<RequestOptions>()))
                .Throws(new StripeException("This is a stripe error!"));
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using UseCase2.Models;

namespace UseCase2.Tests.StripeController
{
    public sealed class GetBalanceTransactions_Tests : StripeController_BaseTest
    {
        public GetBalanceTransactions_Tests() : base()
        {
        }

        [Fact]
        public void Should_Return_Success_Status()
        {
            // Arrange
            ConfigureStripeServicesWithSuccessResponse();

            var controller = new Controllers.StripeController(stripeConfig, balanceServiceMock.Object, balanceTransactionServiceMock.Object);

            // Act
            var result = controller.GetBalanceTransactions("after", 10);

            // Assert
            Assert.Equal("Success", result.Value.Status);
        }

        [Fact]
        public void Should_Return_Balance_Transactions_Response()
        {
            // Arrange
            ConfigureStripeServicesWithSuccessResponse();

            var controller = new Controllers.StripeController(stripeConfig, balanceServiceMock.Object, balanceTransactionServiceMock.Object);

            // Act
            var result = controller.GetBalanceTransactions("after", 10);

            // Assert
            Assert.NotNull(result.Value.Data);
        }

        [Fact]
        public void Should_Return_Error_Status_If_Stripe_Error_Occured()
        {
            // Arrange
            ConfigureStripeServicesWithFailedResponse();

            var controller = new Controllers.StripeController(stripeConfig, balanceServiceMock.Object, balanceTransactionServiceMock.Object);

            // Act
            var result = controller.GetBalanceTransactions("after", 10);

            // Assert
            Assert.Equal("Error", result.Value.Status);
        }

        [Fact]
        public void Should_Return_Message_If_Stripe_Error_Occured()
        {
            // Arrange
            ConfigureStripeServicesWithFailedResponse();

            var controller = new Controllers.StripeController(stripeConfig, balanceServiceMock.Object, balanceTransactionServiceMock.Object);

            // Act
            var result = controller.GetBalanceTransactions("after", 10);

            // Assert
            Assert.Equal("This is a stripe error!", result.Value.Message);
        }
    }
}

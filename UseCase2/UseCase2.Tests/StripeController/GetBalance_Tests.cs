using Microsoft.AspNetCore.Mvc;

namespace UseCase2.Tests.StripeController
{
    public sealed class GetBalance_Tests : StripeController_BaseTest
    {
        public GetBalance_Tests() : base()
        {
        }

        [Fact]
        public void Should_Return_200_Code()
        {
            // Arrange
            ConfigureStripeServicesWithSuccessResponse();

            var controller = new Controllers.StripeController(stripeConfig, balanceServiceMock.Object, balanceTransactionServiceMock.Object);

            // Act
            var result = controller.GetBalance();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void Should_Return_Balance()
        {
            // Arrange
            ConfigureStripeServicesWithSuccessResponse();

            var controller = new Controllers.StripeController(stripeConfig, balanceServiceMock.Object, balanceTransactionServiceMock.Object);

            // Act
            var result = controller.GetBalance();

            // Assert
            Assert.NotNull((result.Result as OkObjectResult).Value);
        }

        [Fact]
        public void Should_Return_404_Code_If_No_Balances()
        {
            // Arrange
            ConfigureStripeServicesWithEmptyResponse();

            var controller = new Controllers.StripeController(stripeConfig, balanceServiceMock.Object, balanceTransactionServiceMock.Object);

            // Act
            var result = controller.GetBalance();

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Should_Return_500_Code_If_Stripe_Error_Occured()
        {
            // Arrange
            ConfigureStripeServicesWithFailedResponse();

            var controller = new Controllers.StripeController(stripeConfig, balanceServiceMock.Object, balanceTransactionServiceMock.Object);

            // Act
            var result = controller.GetBalance();

            // Assert
            Assert.IsType<ObjectResult>(result.Result);
        }

        [Fact]
        public void Should_Return_Message_If_Stripe_Error_Occured()
        {
            // Arrange
            ConfigureStripeServicesWithFailedResponse();

            var controller = new Controllers.StripeController(stripeConfig, balanceServiceMock.Object, balanceTransactionServiceMock.Object);

            // Act
            var result = controller.GetBalance();

            // Assert
            Assert.Equal("This is a stripe error!", (result.Result as ObjectResult).Value);
        }
    }
}

### Application Description
The application integrates the Stripe Payment Gateway with an ASP .NET 6 backend, allowing users to access balance details and transaction histories tied to their Stripe accounts. It has been built with robustness in mind, encapsulating the business logic in service classes and using dependency injection for maintainability and testability. The provided endpoints offer access to account balance and transaction lists with adjustable parameters for more specific queries. Exception handling is efficiently implemented to cater to any Stripe-specific errors that might arise during the operation.

This application also ensures that code quality and functionality remain paramount by incorporating Unit Tests. These tests use the XUnit framework in conjunction with the Moq library, ensuring that all possible use cases and edge scenarios are well-covered, providing a safety net against potential bugs and regressions.

### Running the Application Locally
Prerequisites: Ensure you have the following installed:

.NET 6 SDK
A suitable IDE like Visual Studio or VS Code

Clone the Repository:
``` bash
git clone [repository-url]
```

Navigate to the Project Directory:
``` bash
cd path/to/project-directory
```

Restore Dependencies:

``` bash
dotnet restore
```

Run the Application:
``` bash
dotnet run
```

Your application should now be running locally at https://localhost:7263 (or the port mentioned in the terminal).

### Using the Endpoints
*Fetch Balance*:
**URL**: http://localhost:5000/api/Stripe/Balance
**Method**: GET
**Description**: This endpoint returns the balance details tied to the Stripe account.

*Fetch Balance Transactions*:
**URL**: http://localhost:5000/api/Stripe/BalanceTransactions?startingAfter=txn_123456&take=5
**Method**: GET
**Parameters**:
	*startingAfter*: The ID of the last transaction on the previous page (used for pagination).
	*take*: Number of transactions to fetch. If not specified, defaults to 10.
**Description*: This endpoint returns a list of balance transactions with optional parameters for pagination and the number of records.

Always remember to check the application's documentation or codebase for any additional configuration or endpoints. Happy coding!

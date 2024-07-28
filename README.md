# StockAPI

This project is a simple API for managing and retrieving stock data. It's built using .NET Core and it connects to a SQL Server database.

_This README file is currently a placeholder and is subject to future updates._

## Configuration

The configuration for the project is located in the `appsettings.json` file. Here's a brief overview of the settings:

- `ConnectionStrings`: This section contains the connection string for the database. The `DefaultConnection` string includes the data source (IP and port), the initial catalog (database name), user ID, password, and other connection parameters.

- `Logging`: This section controls the logging level for the application. The `Default` level is set to `Information`, and the `Microsoft.AspNetCore` level is set to `Warning`.

- `AllowedHosts`: This setting controls the hosts that are allowed to connect to the application. It's currently set to `*`, which means any host can connect.

## Usage

To use the API, you'll need to send HTTP requests to the appropriate endpoints. The exact endpoints and their usage will depend on the specific functionality of the API.

## Development

To work on the project, you'll need a .NET Core development environment and access to a SQL Server database. You can then clone the repository, update the `appsettings.json` file with your database connection details, and start the application.

Please note that this is a basic overview. For more detailed information, please refer to the project's documentation or contact the project maintainers.

# WeatherForecasting

A .NET 8-based Weather Forecasting API that fetches real-time and forecasted weather data using the OpenWeatherMap API. The application supports caching, input validation, clean architecture principles, and a layered architecture.

## Features
- Get current weather for any city
- Get 3-hour interval weather forecasts for a selected date up to 5 days in the future
- Authentication Simulation: Includes a mock login endpoint (identity/token) that mimics a real-world authentication flow. It validates credentials (only email address) and returns a JWT token, which can then be used in the Authorization header (Bearer {token}) for secured endpoints.
- Resilience: External API requests are wrapped in retry and fallback policies to handle external faults gracefully.
- Caching with `IMemoryCache` (abstracted via `IAppCache`) in order to reduce external API calls and improve performance.
- Input validation using FluentValidation
- Clean separation of concerns using CQRS (via MediatR)
- Unit tested handlers and validators
- Structured error responses

/swagger
Here you can explore and test all available endpoints interactively using Swagger UI.

Note: This project uses a test API key from OpenWeather, which allows a limited number of requests per day. Since this is not a production-ready application, the API key is stored in appsettings.json. In a real-world scenario, sensitive data like API keys should be stored securely — e.g., using Azure Key Vault

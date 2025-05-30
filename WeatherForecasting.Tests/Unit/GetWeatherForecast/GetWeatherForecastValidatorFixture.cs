// --------------------------------------------------------------------------------------------------------------------
// file="GetWeatherForecastValidatorFixture.cs">
// --------------------------------------------------------------------------------------------------------------------

using WeatherForecasting.Application.Common.Constants;
using WeatherForecasting.Application.Features.Weather.Queries.GetWeatherForecast;
using WeatherForecasting.Contracts.Requests.Weather;

namespace WeatherForecasting.Tests.Unit.GetWeatherForecast;

public class GetWeatherForecastValidatorFixture
{
    public static IEnumerable<object[]> ValidForecastData =>
        new List<object[]>
        {
            new object[] { "New York", DateOnly.FromDateTime(DateTime.Today.AddDays(1)) },
            new object[] { "Amsterdam", DateOnly.FromDateTime(DateTime.Today.AddDays(2)) },
            new object[] { "Fake city with a valid name", DateOnly.FromDateTime(DateTime.Today.AddDays(3)) }
        };
    
    public static IEnumerable<object[]> InvalidForecastData =>
        new List<object[]>
        {
            new object[] { "", DateOnly.FromDateTime(DateTime.Today.AddDays(1)) },
            new object[] { " ", DateOnly.FromDateTime(DateTime.Today.AddDays(1)) },
            new object[] { "X", DateOnly.FromDateTime(DateTime.Today.AddDays(1)) },
            new object[] { "Lond0n", DateOnly.FromDateTime(DateTime.Today.AddDays(1)) },
            new object[] { "New York C1ty", DateOnly.FromDateTime(DateTime.Today.AddDays(1)) },
            new object[] { "123%$", DateOnly.FromDateTime(DateTime.Today.AddDays(1)) },
            new object[] { "London", DateOnly.FromDateTime(DateTime.Today.AddYears(-100)) },
            new object[] { "London", DateOnly.FromDateTime(DateTime.Today.AddDays(-1)) },
            new object[] { "London", DateOnly.FromDateTime(DateTime.Today.AddDays(6)) }
        };
    
    [Theory]
    [MemberData(nameof(ValidForecastData))]
    public async Task ValidateAsync_ValidRequest_ReturnsTrue(string cityName, DateOnly forecastDate)
    {
        //Arrange
        var validRequest = new GetWeatherForecastRequest(cityName, forecastDate, Constants.DefaultTemperatureUnit, Constants.DefaultLanguageCode);
        var validQuery = new GetWeatherForecastQuery(validRequest);

        var validator = new GetWeatherForecastQueryValidator();
        
        //Act
        var validationResult = await validator.ValidateAsync(validQuery);
        
        //Assert
        Assert.True(validationResult.IsValid);
    }
    
    
    [Theory]
    [MemberData(nameof(InvalidForecastData))]
    public async Task ValidateAsync_InvalidRequest_ReturnsFalse(string cityName, DateOnly forecastDate)
    {
        // Arrange
        var request = new GetWeatherForecastRequest(cityName, forecastDate, Constants.DefaultTemperatureUnit, Constants.DefaultLanguageCode);
        var query = new GetWeatherForecastQuery(request);

        var validator = new GetWeatherForecastQueryValidator();

        // Act
        var validationResult = await validator.ValidateAsync(query);

        // Assert
        Assert.NotEmpty(validationResult.Errors);
        Assert.False(validationResult.IsValid);
    }
}
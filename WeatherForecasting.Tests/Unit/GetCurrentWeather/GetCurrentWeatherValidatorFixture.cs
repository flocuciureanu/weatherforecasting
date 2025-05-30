using WeatherForecasting.Application.Common.Constants;
using WeatherForecasting.Application.Features.Weather.Queries.GetCurrentWeather;
using WeatherForecasting.Contracts.Requests.Weather;

namespace WeatherForecasting.Tests.Unit.GetCurrentWeather;

public class GetCurrentWeatherValidatorFixture
{
    [Theory]
    [InlineData(Constants.ExampleCityName)]
    [InlineData("LoNDon")]
    [InlineData("London  ")]
    [InlineData("New York")]
    [InlineData("Amsterdam")]
    [InlineData("Fake city with a valid name")]
    public async Task ValidateAsync_ValidRequest_ReturnsTrue(string cityName)
    {
        //Arrange
        var validRequest = new GetCurrentWeatherRequest(cityName, Constants.DefaultTemperatureUnit, Constants.DefaultLanguageCode);
        var validQuery = new GetCurrentWeatherQuery(validRequest);

        var validator = new GetCurrentWeatherQueryValidator();
            
        //Act
        var validationResult = await validator.ValidateAsync(validQuery);
            
        //Assert
        Assert.True(validationResult.IsValid);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("X")]
    [InlineData("Lond0n")]
    [InlineData("New York C1ty")]
    [InlineData("123%$")]
    public async Task ValidateAsync_InvalidRequest_ReturnsFalseAndNumberOfErrors(string cityName)
    {
        //Arrange
        var invalidRequest = new GetCurrentWeatherRequest(cityName, Constants.DefaultTemperatureUnit, Constants.DefaultLanguageCode);
        var invalidQuery = new GetCurrentWeatherQuery(invalidRequest);

        var validator = new GetCurrentWeatherQueryValidator();
        
        //Act
        var validationResult = await validator.ValidateAsync(invalidQuery);
        
        //Assert
        Assert.NotEmpty(validationResult.Errors);
        Assert.False(validationResult.IsValid);
    }
}
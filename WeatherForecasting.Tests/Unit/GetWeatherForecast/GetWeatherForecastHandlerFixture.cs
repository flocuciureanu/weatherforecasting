// --------------------------------------------------------------------------------------------------------------------
// file="GetWeatherForecastHandlerFixture.cs">
// --------------------------------------------------------------------------------------------------------------------

using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using WeatherForecasting.Application.Common.Constants;
using WeatherForecasting.Application.Common.ExtensionMethods;
using WeatherForecasting.Application.Common.Infrastructure.Caching.MemoryCaching;
using WeatherForecasting.Application.Common.Infrastructure.Serialization;
using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Responses.Common;
using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Responses.GetWeatherForecastData;
using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Services.GetWeatherForecastData;
using WeatherForecasting.Application.Features.Weather;
using WeatherForecasting.Application.Features.Weather.Mappers;
using WeatherForecasting.Application.Features.Weather.Queries.GetWeatherForecast;
using WeatherForecasting.Contracts.Requests.Weather;
using WeatherForecasting.Contracts.Responses.Weather;

namespace WeatherForecasting.Tests.Unit.GetWeatherForecast;

public class GetWeatherForecastHandlerFixture
{
    private readonly Mock<IGetWeatherForecastDataService> _weatherServiceMock = new();
    private readonly Mock<ILogger<GetWeatherForecastQueryHandler>> _loggerMock = new();
    private readonly Mock<IAppCache> _cacheMock = new();
    private readonly Mock<IAppJsonSerializer> _serializerMock = new();

    private readonly GetWeatherForecastQueryHandler _sut;
    
    public GetWeatherForecastHandlerFixture()
    {
        _sut = new GetWeatherForecastQueryHandler(
            _weatherServiceMock.Object,
            _cacheMock.Object,
            _serializerMock.Object,
            _loggerMock.Object);
    }
    
    [Fact]
    public async Task Handle_ReturnsFromCache_WhenItExists()
    {
        // Arrange
        var forecastDate = DateOnly.FromDateTime(DateTime.Today.AddDays(1));
        var request = new GetWeatherForecastRequest(Constants.ExampleCityName, forecastDate, Constants.DefaultTemperatureUnit, Constants.DefaultLanguageCode);
        var query = new GetWeatherForecastQuery(request);

        var weatherSummary = new WeatherSummary(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
        var weatherSummaryWithDate = new WeatherSummaryWithDate(weatherSummary, It.IsAny<string>());
        var expectedCachedResponse = new GetWeatherForecastResponse(Constants.ExampleCityName, forecastDate, [weatherSummaryWithDate]);
        var cacheKey = WeatherCacheKeys.Forecast(Constants.ExampleCityName, forecastDate, Constants.DefaultTemperatureUnit, Constants.DefaultLanguageCode);

        _cacheMock
            .Setup(c => c.Get<GetWeatherForecastResponse>(cacheKey))
            .Returns(expectedCachedResponse);
        
        // Act
        var result = await _sut.Handle(query, CancellationToken.None);

        // Assert
        result.Success.Should().Be(true);
        result.Value.Should().BeEquivalentTo(expectedCachedResponse);
        _weatherServiceMock.Verify(x => x.GetWeatherForecastDataAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
        _cacheMock.Verify(c => c.Get<GetWeatherForecastResponse>(It.IsAny<string>()), Times.Once);
        _cacheMock.Verify(c => c.Set(It.IsAny<string>(), It.IsAny<GetWeatherForecastResponse>(), It.IsAny<TimeSpan>()), Times.Never);
    }
    
    [Fact]
    public async Task Handle_CallsServiceAndReturnsResponse_WhenNotCached()
    {
        // Arrange
        var forecastDate = DateOnly.FromDateTime(DateTime.Today.AddDays(1));
        var request = new GetWeatherForecastRequest(Constants.ExampleCityName, forecastDate, Constants.DefaultTemperatureUnit, Constants.DefaultLanguageCode);
        var query = new GetWeatherForecastQuery(request);

        var serviceResult = new GetWeatherForecastDataResponse
        {
            List =
            [
                new WeatherForecastItemResponse
                {
                    Weather = [],
                    Main = new MainResponse(),
                    Wind = new WindResponse()
                }
            ],
            City = new CityResponse { Name = Constants.ExampleCityName },
            Code = StatusCodes.Status200OK
        };
        _weatherServiceMock
            .Setup(x => x.GetWeatherForecastDataAsync(Constants.ExampleCityName, Constants.DefaultTemperatureUnit.ToOpenWeatherTemperatureUnits(), Constants.DefaultLanguageCode, It.IsAny<CancellationToken>()))
            .ReturnsAsync(serviceResult);
        
        var expectedResponse = serviceResult.ToWeatherForecastResponse(Constants.DefaultTemperatureUnit, forecastDate);
        
        _cacheMock.Setup(c => c.Set(It.IsAny<string>(), It.IsAny<GetWeatherForecastResponse>(), It.IsAny<TimeSpan>()));
        
        // Act
        var result = await _sut.Handle(query, It.IsAny<CancellationToken>());

        // Assert
        result.Success.Should().Be(true);
        result.Value.Should().BeEquivalentTo(expectedResponse);
        _weatherServiceMock.Verify(x => x.GetWeatherForecastDataAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        _cacheMock.Verify(c => c.Get<GetWeatherForecastResponse>(It.IsAny<string>()), Times.Once);
        _cacheMock.Verify(c => c.Set(It.IsAny<string>(), It.IsAny<GetWeatherForecastResponse>(), It.IsAny<TimeSpan>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ReturnsError_WhenServiceFails()
    {
        // Arrange
        var forecastDate = DateOnly.FromDateTime(DateTime.Today.AddDays(1));
        var request = new GetWeatherForecastRequest(Constants.ExampleCityName, forecastDate, Constants.DefaultTemperatureUnit, Constants.DefaultLanguageCode);
        var query = new GetWeatherForecastQuery(request);

        var serviceResult = new GetWeatherForecastDataResponse { Code = StatusCodes.Status400BadRequest };
        _weatherServiceMock
            .Setup(x => x.GetWeatherForecastDataAsync(Constants.ExampleCityName, Constants.DefaultTemperatureUnit.ToOpenWeatherTemperatureUnits(), Constants.DefaultLanguageCode, It.IsAny<CancellationToken>()))
            .ReturnsAsync(serviceResult);

        // Act
        var result = await _sut.Handle(query, It.IsAny<CancellationToken>());

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeFalse();
        result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        result.Errors.Should().NotBeNull();
        result.Value.Should().BeNull();
        _serializerMock.Verify(x => x.Serialize(query.Request), Times.Once);
        _weatherServiceMock.Verify(x => x.GetWeatherForecastDataAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        _cacheMock.Verify(c => c.Get<GetWeatherForecastResponse>(It.IsAny<string>()), Times.Once);
        _cacheMock.Verify(c => c.Set(It.IsAny<string>(), It.IsAny<GetWeatherForecastResponse>(), It.IsAny<TimeSpan>()), Times.Never);
    }
}
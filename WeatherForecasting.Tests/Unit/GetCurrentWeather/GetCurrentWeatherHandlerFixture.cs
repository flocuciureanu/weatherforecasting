// --------------------------------------------------------------------------------------------------------------------
// file="GetCurrentWeatherHandlerFixture.cs">
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
using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Responses.GetCurrentWeatherData;
using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Services.GetCurrentWeatherData;
using WeatherForecasting.Application.Features.Weather;
using WeatherForecasting.Application.Features.Weather.Mappers;
using WeatherForecasting.Application.Features.Weather.Queries.GetCurrentWeather;
using WeatherForecasting.Contracts.Requests.Weather;
using WeatherForecasting.Contracts.Responses.Weather;

namespace WeatherForecasting.Tests.Unit.GetCurrentWeather;

public class GetCurrentWeatherHandlerFixture
{
    private readonly Mock<IGetCurrentWeatherDataService> _weatherServiceMock = new();
    private readonly Mock<ILogger<GetCurrentWeatherQueryHandler>> _loggerMock = new();
    private readonly Mock<IAppCache> _cacheMock = new();
    private readonly Mock<IAppJsonSerializer> _serializerMock = new();

    private readonly GetCurrentWeatherQueryHandler _sut;
    
    public GetCurrentWeatherHandlerFixture()
    {
        _sut = new GetCurrentWeatherQueryHandler(
            _weatherServiceMock.Object,
            _loggerMock.Object,
            _cacheMock.Object,
            _serializerMock.Object);
    }
    
    [Fact]
    public async Task Handle_ReturnsFromCache_WhenItExists()
    {
        // Arrange
        var request = new GetCurrentWeatherRequest(Constants.ExampleCityName, Constants.DefaultTemperatureUnit, Constants.DefaultLanguageCode);
        var query = new GetCurrentWeatherQuery(request);
        
        var weatherSummary = new WeatherSummary(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
        var expectedCachedResponse = new GetCurrentWeatherResponse(Constants.ExampleCityName, weatherSummary);
        var cacheKey = WeatherCacheKeys.Current(Constants.ExampleCityName, Constants.DefaultTemperatureUnit, Constants.DefaultLanguageCode);

        _cacheMock
            .Setup(c => c.Get<GetCurrentWeatherResponse>(cacheKey))
            .Returns((GetCurrentWeatherResponse?)expectedCachedResponse);
        
        // Act
        var result = await _sut.Handle(query, It.IsAny<CancellationToken>());

        // Assert
        result.Success.Should().Be(true);
        result.Value.Should().BeEquivalentTo(expectedCachedResponse);
        _serializerMock.Verify(x => x.Serialize(query.Request), Times.Never);
        _weatherServiceMock.Verify(x => x.GetCurrentWeatherDataAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
        _cacheMock.Verify(c => c.Get<GetCurrentWeatherResponse>(It.IsAny<string>()), Times.Once);
        _cacheMock.Verify(c => c.Set(It.IsAny<string>(), It.IsAny<GetCurrentWeatherResponse>(), It.IsAny<TimeSpan>()), Times.Never);
    }
    
    [Fact]
    public async Task Handle_CallsServiceAndReturnsResponse_WhenNotCached()
    {
        // Arrange
        var request = new GetCurrentWeatherRequest(Constants.ExampleCityName, Constants.DefaultTemperatureUnit, Constants.DefaultLanguageCode);
        var query = new GetCurrentWeatherQuery(request);

        var serviceResult = new GetCurrentWeatherDataResponse
        {
            Name = Constants.ExampleCityName,
            Weather = [new WeatherResponse()],
            Main = new MainResponse(),
            Wind = new WindResponse(),
            Code = StatusCodes.Status200OK
        };
        _weatherServiceMock
            .Setup(x => x.GetCurrentWeatherDataAsync(Constants.ExampleCityName, Constants.DefaultTemperatureUnit.ToOpenWeatherTemperatureUnits(), Constants.DefaultLanguageCode, It.IsAny<CancellationToken>()))
            .ReturnsAsync(serviceResult);

        var expectedResponse = serviceResult.ToCurrentWeatherResponse(Constants.DefaultTemperatureUnit);
        _cacheMock.Setup(c => c.Set(It.IsAny<string>(), It.IsAny<GetCurrentWeatherResponse>(), It.IsAny<TimeSpan>()));
        
        // Act
        var result = await _sut.Handle(query, It.IsAny<CancellationToken>());

        // Assert
        result.Success.Should().Be(true);
        result.Value.Should().BeEquivalentTo(expectedResponse);
        _serializerMock.Verify(x => x.Serialize(query.Request), Times.Never);
        _weatherServiceMock.Verify(x => x.GetCurrentWeatherDataAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        _cacheMock.Verify(c => c.Get<GetCurrentWeatherResponse>(It.IsAny<string>()), Times.Once);
        _cacheMock.Verify(c => c.Set(It.IsAny<string>(), It.IsAny<GetCurrentWeatherResponse>(), It.IsAny<TimeSpan>()), Times.Once);
    }
    
    [Fact]
    public async Task Handle_ReturnsError_WhenServiceFails()
    {
        // Arrange
        var request = new GetCurrentWeatherRequest(Constants.ExampleCityName, Constants.DefaultTemperatureUnit, Constants.DefaultLanguageCode);
        var query = new GetCurrentWeatherQuery(request);

        var serviceResult = new GetCurrentWeatherDataResponse { Code = StatusCodes.Status400BadRequest };
        _weatherServiceMock
            .Setup(x => x.GetCurrentWeatherDataAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
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
        _weatherServiceMock.Verify(x => x.GetCurrentWeatherDataAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        _cacheMock.Verify(c => c.Get<GetCurrentWeatherResponse>(It.IsAny<string>()), Times.Once);
        _cacheMock.Verify(c => c.Set(It.IsAny<string>(), It.IsAny<GetCurrentWeatherResponse>(), It.IsAny<TimeSpan>()), Times.Never);
    }
}
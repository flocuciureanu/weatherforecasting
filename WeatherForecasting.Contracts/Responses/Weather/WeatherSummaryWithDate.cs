// --------------------------------------------------------------------------------------------------------------------
// file="WeatherSummaryWithDate.cs">
// --------------------------------------------------------------------------------------------------------------------

namespace WeatherForecasting.Contracts.Responses.Weather;

public record WeatherSummaryWithDate : WeatherSummary
{
    public string Date { get; init; }

    public WeatherSummaryWithDate(WeatherSummary baseSummary, string date)
        : base(baseSummary.Description, baseSummary.Temperature, baseSummary.FeelsLike,
            baseSummary.TemperatureRange, baseSummary.Humidity, baseSummary.Wind)
    {
        Date = date;
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Bondai" file="GetCurrentWeatherResponse.cs">
// Copyright (c) Bondai.  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WeatherForecasting.Contracts.Responses.Weather;

public record GetCurrentWeatherResponse(string? CityName, WeatherSummary? WeatherSummary);

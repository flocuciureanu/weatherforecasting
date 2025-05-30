// --------------------------------------------------------------------------------------------------------------------
// file="Constants.cs">
// --------------------------------------------------------------------------------------------------------------------

using WeatherForecasting.Contracts.Enums;

namespace WeatherForecasting.Application.Common.Constants;

public static class Constants
{
    public const string DefaultLanguageCode = "en";
    public const TemperatureUnit DefaultTemperatureUnit = TemperatureUnit.Celsius;
    public const string ExampleCityName = "London";
    public const string ExampleEmailAddress = "test@example.com";
    
    public static class HttClientOptions
    {
        public const string ResiliencePipelineName = "MyResiliencePipeline";
    }

    public static class Urls
    {
        public const string OpenWeatherMapBaseUrl = "https://api.openweathermap.org/data/2.5";
    }

    public static class ErrorMessages
    {
        //CommandResult custom messages
        public const string ValidationFailureMessage = "One or more validation errors occurred.";
        public const string NotFound = "The requested resource was not found.";
        public const string Unauthorized = "You are not authorized to access this resource.";
        public const string BadRequest = "Invalid input. Please check your data and try again.";
        public const string ServerError = "A server error occured. Please try again.";
        public const string TooManyRequestsError = "Too many requests. Please try again later.";
        
        //CommandsAndQueries
        //Weather
        public const string OpenWeatherMapInvalidCityName = "Could not find the city. Please check the spelling and try again.";
        public const string OpenWeatherMapServiceUnavailable = "There was an error while fetching the weather data. Please try again.";
        
        //Identity
        public const string TokenCreationFailure = "There was an error while creating the token. Please try again.";

        //Validators custom messages
        //Weather
        public const string InvalidCityName = "'CityName' must be between 2 and 100 characters, and contain only letters, spaces, hyphens, or apostrophes.";
        public const string InvalidTemperatureUnits = "'Units' must have a value from one of the following: Celsius, Fahrenheit or Kelvin (case insensitive).";
        public const string InvalidForecastDate = "'ForecastDate' must be today or within the next 5 days and cannot be in the past.";
        //Identity
        public const string InvalidEmailAddress = "Please ensure that the 'EmailAddress' field is provided and is in a valid format.";

        
        //QueryString params error messages
        //OpenWeatherMap
        public const string NullCityName = "CityName must be provided.";
        public const string NullApiKey = "ApiKey must be provided.";
    }

    public static class QueryStringKeys
    {
        public const string ApiKeyQueryKey = "appid";
        public const string CityNameQueryKey = "q";
        public const string LanguageQueryKey = "lang";
        public const string UnitsQueryKey = "units";
    }
}
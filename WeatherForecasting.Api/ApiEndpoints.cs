// --------------------------------------------------------------------------------------------------------------------
// file="ApiEndpoints.cs">
// --------------------------------------------------------------------------------------------------------------------

namespace WeatherForecasting.Api;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class V1
    {
        private const string VersionBase = $"{ApiBase}/v1";
        
        public static class Weather
        {
            private const string Base = $"{VersionBase}/weather";
            
            public const string Current = $"{Base}/current";
            public const string Forecast = $"{Base}/forecast";
        }

        public static class Metadata
        {
            private const string Base = $"{VersionBase}/metadata";
            
            public const string GetTemperatureUnits = $"{Base}/temperature-units";
            public const string GetLanguageCodes = $"{Base}/language-codes";
        }

        public static class Identity
        {
            private const string Base = $"{VersionBase}/identity";
            
            public const string GenerateToken = $"{Base}/token";
        }
    }
}
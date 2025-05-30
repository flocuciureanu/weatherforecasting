// --------------------------------------------------------------------------------------------------------------------
// file="CultureInfoExtensions.cs">
// --------------------------------------------------------------------------------------------------------------------

using System.Globalization;

namespace WeatherForecasting.Application.Common.ExtensionMethods;

public static class CultureInfoExtensions
{
    public static bool TryGetCultureInfo(string code, out CultureInfo? culture)
    {
        try
        {
            culture = CultureInfo.GetCultureInfo(code);
            return true;
        }
        catch (CultureNotFoundException)
        {
            culture = null;
            return false;
        }
    }
}
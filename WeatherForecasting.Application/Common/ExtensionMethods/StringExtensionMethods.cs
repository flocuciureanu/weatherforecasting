// --------------------------------------------------------------------------------------------------------------------
// file="StringExtensionMethods.cs">
// --------------------------------------------------------------------------------------------------------------------

using System.Text.RegularExpressions;

namespace WeatherForecasting.Application.Common.ExtensionMethods;

public static class StringExtensionMethods
{
    public static bool IsValidEmailAddress(this string emailAddress)
    {
        if (string.IsNullOrEmpty(emailAddress))
            return false;

        var regex = new Regex(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-]{2,}$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        return regex.IsMatch(emailAddress.Trim());
    }
}
// --------------------------------------------------------------------------------------------------------------------
// file="EnumExtensionMethods.cs">
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel;

namespace WeatherForecasting.Application.Common.ExtensionMethods;

public static class EnumExtensionMethods
{
    public static string GetDescription(this Enum value)
    {
        var type = value.GetType();
        var name = Enum.GetName(type, value);

        if (name == null)
            return value.ToString();

        var field = type.GetField(name);

        if (field == null)
            return value.ToString();

        if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attr)
        {
            return attr.Description;
        }

        return value.ToString();
    }
}
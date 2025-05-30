// --------------------------------------------------------------------------------------------------------------------
// file="AppJsonSerializer.cs">
// --------------------------------------------------------------------------------------------------------------------

using System.Text;
using System.Text.Json;

namespace WeatherForecasting.Application.Common.Infrastructure.Serialization;

public class AppJsonSerializer : IAppJsonSerializer
{
    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true
    };

    public T Deserialize<T>(string source)
    {
        return JsonSerializer.Deserialize<T>(source, _options)!;
    }

    public string Serialize(object source)
    {
        return JsonSerializer.Serialize(source, _options);
    }

    public string SerializeAndEncode(object source)
    {
        var json = Serialize(source);
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
    }

    public T DecodeAndDeserialize<T>(string base64)
    {
        var bytes = Convert.FromBase64String(base64);
        var json = Encoding.UTF8.GetString(bytes);
        return Deserialize<T>(json);
    }
}
// --------------------------------------------------------------------------------------------------------------------
// file="IAppJsonSerializer.cs">
// --------------------------------------------------------------------------------------------------------------------

namespace WeatherForecasting.Application.Common.Infrastructure.Serialization;

public interface IAppJsonSerializer
{
    T Deserialize<T>(string source);
    string Serialize(object source);
    string SerializeAndEncode(object source);
    T DecodeAndDeserialize<T>(string source);
}
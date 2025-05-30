// --------------------------------------------------------------------------------------------------------------------
// file="GetMetadataResponse.cs">
// --------------------------------------------------------------------------------------------------------------------

namespace WeatherForecasting.Contracts.Responses.Metadata;

public record GetMetadataResponse(IReadOnlyList<MetadataItemResponse> Values);

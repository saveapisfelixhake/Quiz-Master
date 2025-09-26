using Backend.Domains.Bar.Domain.Models.Dtos.BarAddress;

namespace Backend.Domains.Bar.Domain.Models.Dtos.Bar;

public class BarGetDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required BarAddressGetDto Address { get; init; }
}
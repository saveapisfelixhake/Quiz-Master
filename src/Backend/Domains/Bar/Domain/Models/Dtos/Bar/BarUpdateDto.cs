using Backend.Domains.Bar.Domain.Models.Dtos.BarAddress;

namespace Backend.Domains.Bar.Domain.Models.Dtos.Bar;

public class BarUpdateDto
{
    public required string Name { get; init; }
    public required bool IsActive { get; init; }
    public required BarAddressUpdateDto Address { get; init; }
}
using Backend.Domains.Bar.Domain.Models.Dtos.BarAddress;

namespace Backend.Domains.Bar.Domain.Models.Dtos.Bar;

public class BarCreateDto
{
    public required string Name { get; init; }
    public required bool IsActive { get; init; }
    public required BarAddressCreateDto Address { get; init; }
}
namespace Backend.Domains.Bar.Domain.Models.Dtos.BarAddress;

public class BarAddressCreateDto
{
    public required string Street { get; init; }
    public required string Number { get; init; }
}
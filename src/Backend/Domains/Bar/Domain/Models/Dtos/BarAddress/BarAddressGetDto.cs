namespace Backend.Domains.Bar.Domain.Models.Dtos.BarAddress;

public class BarAddressGetDto
{
    public required Guid Id { get; init; }
    public required string Street { get; init; }
    public required string Number { get; init; }
}
using Backend.Domains.Bar.Domain.Models.Dtos.Bar;
using Backend.Domains.Bar.Domain.Models.Dtos.BarAddress;
using Backend.Domains.Bar.Domain.Models.Entities;

namespace Backend.Domains.Bar.Application.Mapping;

public class BarMapper : IBarMapper
{
    public BarGetDto MapBarToGetDto(BarEntity barEntity)
    {
        return new BarGetDto
        {
            Id = barEntity.Id,
            Name = barEntity.Name,
            Address = MapBarAddressToGetDto(barEntity.Address)
        };
    }

    public BarAddressGetDto MapBarAddressToGetDto(BarAddressEntity barEntity)
    {
        return new BarAddressGetDto
        {
            Id = barEntity.Id,
            Street = barEntity.Street,
            Number = barEntity.Number
        };
    }
}
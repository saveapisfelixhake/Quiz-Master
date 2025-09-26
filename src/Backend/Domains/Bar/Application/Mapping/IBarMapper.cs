using Backend.Domains.Bar.Domain.Models.Dtos.Bar;
using Backend.Domains.Bar.Domain.Models.Dtos.BarAddress;
using Backend.Domains.Bar.Domain.Models.Entities;

namespace Backend.Domains.Bar.Application.Mapping;

public interface IBarMapper
{
    BarGetDto MapBarToGetDto(BarEntity barEntity);
    BarAddressGetDto MapBarAddressToGetDto(BarAddressEntity barEntity);
}
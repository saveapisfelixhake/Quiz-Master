using Backend.Domains.Bar.Domain.Models.Dtos.Bar;

namespace Backend.Domains.Bar.Infrastructure.Services;

public interface IBarService
{
    Task<List<BarGetDto>> GetAllAsync();
    Task<BarGetDto?> GetByIdAsync(Guid id);
    Task<Guid?> CreateAsync(BarCreateDto dto);
    Task<Guid?> UpdateAsync(Guid id, BarUpdateDto dto);
    Task DeleteAsync(Guid id);
}
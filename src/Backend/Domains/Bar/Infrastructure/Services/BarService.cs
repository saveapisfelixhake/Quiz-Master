using Backend.Domains.Bar.Application.Mapping;
using Backend.Domains.Bar.Domain.Models.Dtos.Bar;
using Backend.Domains.Bar.Domain.Models.Entities;
using Backend.Domains.Quiz.Persistence.Sql.Context;
using Microsoft.EntityFrameworkCore;

namespace Backend.Domains.Bar.Infrastructure.Services;

public class BarService(IDbContextFactory<DataContext> factory, IBarMapper mapper) : IBarService
{
    public async Task<List<BarGetDto>> GetAllAsync()
    {
        await using var context = await factory.CreateDbContextAsync().ConfigureAwait(false);

        var bars = await context.Bars
            .Include(x => x.Address)
            .ToListAsync()
            .ConfigureAwait(false);

        return bars.Select(mapper.MapBarToGetDto).ToList();
    }

    public async Task<BarGetDto?> GetByIdAsync(Guid id)
    {
        await using var context = await factory.CreateDbContextAsync().ConfigureAwait(false);

        var bar = await context.Bars
            .Include(x => x.Address)
            .SingleOrDefaultAsync(x => x.Id == id)
            .ConfigureAwait(false);

        return bar == null
            ? null
            : mapper.MapBarToGetDto(bar);
    }

    public async Task<Guid?> CreateAsync(BarCreateDto dto)
    {
        await using var context = await factory.CreateDbContextAsync().ConfigureAwait(false);
        var existingBar = await context.Bars
            .SingleOrDefaultAsync(x => x.Name == dto.Name)
            .ConfigureAwait(false);
        if (existingBar != null)
        {
            return null;
        }

        var address = await context.BarAddresses
            .SingleOrDefaultAsync(x => x.Street == dto.Address.Street && x.Number == dto.Address.Number)
            .ConfigureAwait(false) ?? BarAddressEntity.Create(dto.Address.Street, dto.Address.Number);
        var bar = BarEntity.Create(dto.Name, dto.IsActive, address.Id);
        context.BarAddresses.Add(address);
        context.Bars.Add(bar);
        await context.SaveChangesAsync().ConfigureAwait(false);

        return bar.Id;
    }

    public async Task<Guid?> UpdateAsync(Guid id, BarUpdateDto dto)
    {
        await using var context = await factory.CreateDbContextAsync().ConfigureAwait(false);
        var existingBar = await context.Bars
            .Include(x => x.Address)
            .SingleOrDefaultAsync(x => x.Id == id)
            .ConfigureAwait(false);
        if (existingBar == null)
        {
            return null;
        }

        if (!existingBar.HasChanges(dto.Name, dto.Address.Street, dto.Address.Number))
        {
            return existingBar.Id;
        }

        existingBar.Update(dto.Name, dto.IsActive, dto.Address.Street, dto.Address.Number);
        await context.SaveChangesAsync().ConfigureAwait(false);

        return existingBar.Id;
    }

    public async Task DeleteAsync(Guid id)
    {
        await using var context = await factory.CreateDbContextAsync().ConfigureAwait(false);
        var existingBar = await context.Bars
            .Include(x => x.Address)
            .SingleOrDefaultAsync(x => x.Id == id)
            .ConfigureAwait(false);
        if (existingBar == null)
        {
            return;
        }

        context.Bars.Remove(existingBar);
        await context.SaveChangesAsync().ConfigureAwait(false);
    }
}
using Backend.Domains.Common.Persistence.Sql.Context;
using Backend.Domains.Player.Entites;
using Backend.Domains.Player.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Domains.Player.Service.PlayerService;

public class PlayerService(IDbContextFactory<DataContext> dataContext) : IPlayerService
{
    
    public List<PlayerDto> GetPlayer()
    { 
        using var playerContext = dataContext.CreateDbContext();
        var playerList = playerContext.Players.Select(x => new PlayerDto(x.Name , x.BarName , x.PlayerId)).ToList();
        return playerList;
    }

    public void CreatePlayer(CreatePlayerDto playerDto)
    {
        using var playerContext = dataContext.CreateDbContext();
        var  player = playerContext.Players.Add(new PlayerEntity(playerDto.Name , playerDto.BarName  , Guid.NewGuid()));
        playerContext.SaveChanges();
    }

    public void UpdatePlayer(Guid id, UpdatePlayerDto playerDto)
    {
        using var playerContext = dataContext.CreateDbContext();
        var player = playerContext.Players.Find(id);
        if (player == null)
            return;
        player.Name = playerDto.Name;
        player.BarName = playerDto.BarName;
        playerContext.SaveChanges();
    }
}
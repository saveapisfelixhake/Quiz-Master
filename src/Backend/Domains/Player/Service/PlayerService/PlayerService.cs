using Backend.Domains.Player.Entities;
using Backend.Domains.Player.Mapper.PlayerMapper;
using Backend.Domains.Quiz.Persistence.Sql.Context;
using Backend.Domains.Player.Models.Player;
using Microsoft.EntityFrameworkCore;

namespace Backend.Domains.Player.Service.PlayerService;

public class PlayerService(IDbContextFactory<DataContext> dataContext , IPlayerMapper playerMapper) : IPlayerService
{
    
    public List<PlayerDto> GetPlayer()
    { 
        using var playerContext = dataContext.CreateDbContext();
        var playerList = playerContext.Players.Select(x => playerMapper.PlayerToPlayerDto(x)).ToList();
        return playerList;
    }

    public void CreatePlayer(CreatePlayerDto playerDto)
    {
        using var playerContext = dataContext.CreateDbContext(); 
        playerContext.Players.Add(new PlayerEntity(playerDto.Name , playerDto.GroupName  , Guid.NewGuid()));
        playerContext.SaveChanges();
    }

    public void UpdatePlayer(Guid id, UpdatePlayerDto playerDto)
    {
        using var playerContext = dataContext.CreateDbContext();
        var player = playerContext.Players.Find(id);
        if (player == null)
            return;
        player.Name = playerDto.Name;
        player.GroupName = playerDto.GroupName;
        playerContext.SaveChanges();
    }

    public PlayerDto GetPlayerByGuid(Guid id)
    {
        
        using var playerContext = dataContext.CreateDbContext();
        var player = playerContext.Players.FirstOrDefault(x => x.PlayerId == id);
        if (player == null)
            throw new Exception($"Player not found, by Id: {id}");
        return playerMapper.PlayerToPlayerDto(player);
    }
}
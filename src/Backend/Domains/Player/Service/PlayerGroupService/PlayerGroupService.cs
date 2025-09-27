using Backend.Domains.Common.Persistence.Sql.Context;
using Backend.Domains.Player.Entities;
using Backend.Domains.Player.Mapper.PlayerGroupMapper;
using Backend.Domains.Player.Models;
using Backend.Domains.Player.Models.PlayerGroup;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Backend.Domains.Player.Service.PlayerGroupService;

public class PlayerGroupService(IDbContextFactory<DataContext> dataContext, IPlayerGroupMapper playerGroupMapper)
    : IPlayerGroupService
{
    public List<PlayerGroupDto> GetPlayerGroup()
    {
        using var playerGroupContext = dataContext.CreateDbContext();
        var playerGroupList = playerGroupContext.PlayerGroups
            .Select(x => playerGroupMapper.PlayerGroupToPlayerGroupDto(x)).ToList();
        return playerGroupList;
    }

    public PlayerGroupDto GetPlayerGroupById(Guid playerGroupId)
    {
        using var playerGroupContext = dataContext.CreateDbContext();
        var player = playerGroupContext.PlayerGroups.FirstOrDefault(x => x.GroupId == playerGroupId);
        if (player == null)
            throw new Exception($"Player not found, by Id: {playerGroupId}");
        return playerGroupMapper.PlayerGroupToPlayerGroupDto(player);
    }

    public void CreatePlayerGroup(CreatePlayerGroupDto playerGroup)
    {
        using var playerCroupContext = dataContext.CreateDbContext();
        playerCroupContext.PlayerGroups.Add(new PlayerGroupEntity(playerGroup.GroupName, playerGroup.BarName,Guid.NewGuid()));
        playerCroupContext.SaveChanges();
    }

    public void UpdatePlayerGroup(Guid playerGroupId, UpdatePlayerGroupDto playerGroupDto)
    {
        using var  playerCroupContext = dataContext.CreateDbContext();
        var playerGroup = playerCroupContext.PlayerGroups.Find(playerGroupId);
        if (playerGroup == null)
            throw new Exception($"Player not found, by Id: {playerGroupId}");
        playerGroup.GroupName = playerGroupDto.GroupName;
        playerGroup.BarName = playerGroupDto.BarName;
        playerCroupContext.SaveChanges();
    }
}
    
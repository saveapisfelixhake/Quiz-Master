using Backend.Domains.Player.Models;
using Backend.Domains.Player.Models.PlayerGroup;

namespace Backend.Domains.Player.Service.PlayerGroupService;

public interface IPlayerGroupService
{
    List<PlayerGroupDto> GetPlayerGroup();
    PlayerGroupDto GetPlayerGroupById(Guid playerGroupId);
    void CreatePlayerGroup(CreatePlayerGroupDto  playerGroup);
    void UpdatePlayerGroup(Guid playerGroupId ,UpdatePlayerGroupDto  playerGroup);
}
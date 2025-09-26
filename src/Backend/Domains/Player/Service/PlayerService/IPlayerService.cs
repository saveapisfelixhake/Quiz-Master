using Backend.Domains.Player.Models;
using Backend.Domains.Player.Models.Player;

namespace Backend.Domains.Player.Service.PlayerService;

public interface IPlayerService
{
    public List<PlayerDto>  GetPlayer();
    void CreatePlayer(CreatePlayerDto playerDto);
    void UpdatePlayer(Guid id, UpdatePlayerDto playerDto);
    PlayerDto GetPlayerByGuid(Guid id);
}
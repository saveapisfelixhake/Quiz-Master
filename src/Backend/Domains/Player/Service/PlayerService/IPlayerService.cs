using Backend.Domains.Player.Entites;
using Backend.Domains.Player.Models;

namespace Backend.Domains.Player.Service.PlayerService;

public interface IPlayerService
{
    public List<PlayerDto>  GetPlayer();
    void CreatePlayer(CreatePlayerDto playerDto);
}
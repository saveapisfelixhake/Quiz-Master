using Backend.Domains.Player.Entities;
using Backend.Domains.Player.Models;
using Backend.Domains.Player.Models.Player;

namespace Backend.Domains.Player.Mapper.PlayerMapper;

public interface IPlayerMapper
{ 
    PlayerDto PlayerToPlayerDto(PlayerEntity playerEntity);
}
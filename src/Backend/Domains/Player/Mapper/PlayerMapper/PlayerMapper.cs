using Backend.Domains.Player.Entities;
using Backend.Domains.Player.Models;
using Backend.Domains.Player.Models.Player;
using Riok.Mapperly.Abstractions;

namespace Backend.Domains.Player.Mapper.PlayerMapper;
[Mapper]
public partial class PlayerMapper : IPlayerMapper
{
    public partial PlayerDto PlayerToPlayerDto(PlayerEntity playerEntity);
}
using Backend.Domains.Player.Entities;
using Backend.Domains.Player.Models;
using Backend.Domains.Player.Models.PlayerGroup;
using Riok.Mapperly.Abstractions;

namespace Backend.Domains.Player.Mapper.PlayerGroupMapper;
[Mapper]
public partial class PlayerGroupMapper : IPlayerGroupMapper
{
    public partial PlayerGroupDto PlayerGroupToPlayerGroupDto(PlayerGroupEntity player);
}
using Backend.Domains.Player.Entities;
using Backend.Domains.Player.Models;
using Backend.Domains.Player.Models.PlayerGroup;

namespace Backend.Domains.Player.Mapper.PlayerGroupMapper;

public interface IPlayerGroupMapper
{
     PlayerGroupDto PlayerGroupToPlayerGroupDto(PlayerGroupEntity playerGroupEntity);
}
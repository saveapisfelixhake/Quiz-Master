using Backend.Domains.Player.Models;
using Backend.Domains.Player.Models.Player;
using Backend.Domains.Player.Models.PlayerGroup;
using Backend.Domains.Player.Service.PlayerGroupService;
using Backend.Domains.Player.Service.PlayerService;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Domains.Common.Application.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayerController(IPlayerService playerService , IPlayerGroupService playerGroupService) : ControllerBase
{  
    private readonly IPlayerService _playerService =  playerService;
    private readonly IPlayerGroupService _playerGroupService =  playerGroupService;

    [HttpGet("GetPlayer")]
    public ActionResult<List<PlayerDto>> GetPlayer()
    {
       return Ok( _playerService.GetPlayer());
    }

    [HttpGet("GetPlayerByGuid")]
    public ActionResult<PlayerDto> GetPlayerByGuid(Guid guid)
    {
        return Ok(_playerService.GetPlayerByGuid(guid));
    }

    [HttpPost("CreatePlayer")]
    public ActionResult CreatePlayer(CreatePlayerDto playerDto)
    {
        _playerService.CreatePlayer(playerDto);
        return Ok();
    }

    [HttpPatch("UpdatePlayer")]
    public ActionResult UpdatePlayer(Guid Id,UpdatePlayerDto  playerDto)
    {
        _playerService.UpdatePlayer(Id ,playerDto);
        return Ok();
    }

    [HttpGet("GetPlayerGroup")]
    public ActionResult<List<PlayerGroupDto>> GetPlayerGroup()
    {
        return Ok(_playerGroupService.GetPlayerGroup);
    }

    [HttpGet("GetPlayerGroupByGuid")]
    public ActionResult<PlayerGroupDto> GetPlayerGroupByGuid(Guid guid)
    {
        return Ok(playerGroupService.GetPlayerGroupById(guid));
    }

    [HttpPost("CreatePlayerGroup")]
    public ActionResult CreatePlayerGroup(CreatePlayerGroupDto  playerGroupDto)
    {
        _playerGroupService.CreatePlayerGroup(playerGroupDto);
        return Ok();
    }

    [HttpPatch("UpdatePlayerGroup")]
    public ActionResult UpdatePlayerGroup(Guid guid, UpdatePlayerGroupDto playerGroupDto)
    {
        _playerGroupService.UpdatePlayerGroup(guid ,playerGroupDto);
        return Ok();
    }
    
    
}
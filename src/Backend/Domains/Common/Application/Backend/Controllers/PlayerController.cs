using Backend.Domains.Player.Entites;
using Backend.Domains.Player.Models;
using Backend.Domains.Player.Service.PlayerService;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Domains.Common.Application.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayerController(IPlayerService playerService) : ControllerBase
{
    private readonly IPlayerService _playerService =  playerService;

    [HttpGet("GetPlayer")]
    public ActionResult<List<PlayerDto>> GetPlayer()
    {
       return Ok( _playerService.GetPlayer());
    }

    [HttpPost("CreatePlayer")]
    public ActionResult CreatePlayer(CreatePlayerDto playerDto)
    {
        _playerService.CreatePlayer(playerDto);
        return Ok();
    }

    [HttpPost("UpdatePlayer")]
    public ActionResult UpdatePlayer(UpdatePlayerDto)
    
}
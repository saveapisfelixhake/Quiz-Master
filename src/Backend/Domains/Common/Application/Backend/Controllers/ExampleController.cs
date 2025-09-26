using Microsoft.AspNetCore.Mvc;

namespace Backend.Domains.Common.Application.Backend.Controllers;

[ApiController]
[Route("example")]
public class ExampleController : ControllerBase
{
    [HttpGet]
    public IActionResult GetWithoutRoute()
    {
        return Ok("Endpoint without route");
    }

    [HttpGet("specific")]
    public IActionResult GetWithRoute()
    {
        return Ok("Endpoint with specific route");
    }

    [HttpPost("create")]
    public IActionResult Create()
    {
        return Ok("Endpoint for creation");
    }

    [HttpPut("update/full")]
    public IActionResult UpdateFull()
    {
        return Ok("Endpoint for update full");
    }

    [HttpPatch("update/partial1")]
    public IActionResult UpdatePartial1()
    {
        return Ok("Endpoint for partial update 1");
    }

    [HttpPatch("update/partial2")]
    public IActionResult UpdatePartial2()
    {
        return Ok("Endpoint for partial update 2");
    }

    [HttpDelete("delete")]
    public IActionResult Delete()
    {
        return Ok("Endpoint for delete");
    }
}
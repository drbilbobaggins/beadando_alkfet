using Microsoft.AspNetCore.Mvc;

namespace BoardGame.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
  [HttpGet]
  public IActionResult Get()
  {
    return Ok(new { message = "API is working" });
  }
}
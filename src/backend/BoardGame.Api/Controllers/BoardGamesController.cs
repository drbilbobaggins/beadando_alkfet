using BoardGame.Api.Models;
using BoardGame.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BoardGame.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BoardGamesController : ControllerBase
{
    private readonly IBoardGameService _boardGameService;

    public BoardGamesController(IBoardGameService boardGameService)
    {
        _boardGameService = boardGameService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var boardGames = await _boardGameService.GetAllAsync();
        return Ok(boardGames);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        try
        {
            var boardGame = await _boardGameService.GetByIdAsync(id);

            if (boardGame is null)
            {
                return NotFound();
            }

            return Ok(boardGame);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBoardGameRequest request)
    {
        try
        {
            var createdBoardGame = await _boardGameService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = createdBoardGame.Id }, createdBoardGame);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateBoardGameRequest request)
    {
        try
        {
            var updated = await _boardGameService.UpdateAsync(id, request);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            var deleted = await _boardGameService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}

using System.ComponentModel.DataAnnotations;
using BackendTestTask.Business.Interfaces;
using BackendTestTask.Business.Models;
using BackendTestTask.Core;
using Microsoft.AspNetCore.Mvc;

namespace BackendTestTask.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class JournalController : ControllerBase
{
    private readonly IJournalService _journalService;

    public JournalController(IJournalService journalService)
    {
        _journalService = journalService;
    }
    
    [HttpGet("getSingle")]
    [ProducesResponseType(typeof(Journal), 200)]
    public async Task<IActionResult> GetSingle([Required]long id)
    {
        var journal = await _journalService.GetSingleAsync(id);
        return Ok(journal);
    }
    
    [HttpGet("getRange")]
    [ProducesResponseType(typeof(List<Journal>), 200)]
    public async Task<IActionResult> GetRange([Required]int skip, [Required]int take, [Required][FromQuery]JournalFilter journalFilter)
    {
        var campaigns = await _journalService.GetRangeAsync(skip, take, journalFilter);
        return Ok(campaigns);
    }
}
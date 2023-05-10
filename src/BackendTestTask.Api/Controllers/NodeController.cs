using System.ComponentModel.DataAnnotations;
using BackendTestTask.Business.Interfaces;
using BackendTestTask.Business.Models;
using BackendTestTask.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendTestTask.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class NodeController : ControllerBase
{
    private readonly ILogger<NodeController> _logger;
    private readonly INodeService _nodeService;
    
    public NodeController(ILogger<NodeController> logger, INodeService nodeService)
    {
        _logger = logger;
        _nodeService = nodeService;
    }
    
    [HttpGet("get")]
    [ProducesResponseType(typeof(NodeModel), 200)]
    public async Task<IActionResult> GetTree([Required]string treeName)
    {
        var node = await _nodeService.GetTreeByNameAsync(treeName);
        return Ok(node);
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateNodeRequest request)
    {
        await _nodeService.CreateNodeAsync(request);
        return Ok();
    }
    
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([Required]string treeName, [Required]long nodeId)
    {
        await _nodeService.DeleteNodeAsync(treeName, nodeId);
        return Ok();
    }
    
    [HttpPut("rename")]
    public async Task<IActionResult> RenameNode(RenameNodeRequest model)
    {
        await _nodeService.RenameNodeAsync(model);
        return Ok();
    }
}
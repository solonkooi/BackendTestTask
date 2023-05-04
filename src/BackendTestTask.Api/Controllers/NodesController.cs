using Microsoft.AspNetCore.Mvc;

namespace BackendTestTask.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class NodesController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public NodesController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }
    
    
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.Context;
using ProjectAPI.Model;
namespace ProjectAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly ILogger<TodoController> _logger;
    private readonly ToDoDB _toDoDB;
    public TodoController(ILogger<TodoController> logger, ToDoDB toDoDB)
    {
        _logger = logger;
        _toDoDB = toDoDB;
    }

    [HttpGet(Name = "GetToDo")]
    public async Task<ActionResult<IEnumerable<Todo>>> Get()
    {
        var todos = await _toDoDB.Todos.ToListAsync();
        return Ok(todos);
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.Application.Services.Abstractions;
using ProjectAPI.Context;
using ProjectAPI.Library.Extensions;
using ProjectAPI.Model;
namespace ProjectAPI.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly ILogger<TodoController> _logger;
    private IConfiguration _configuration;
    private ITodoService _todoService;
    public TodoController(ILogger<TodoController> logger,ITodoService todoService, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        _todoService = todoService;
    }

    [HttpGet(Name = "GetToDo")]
    public async Task<IResult> Get()
    {
        var todos = await _todoService.GetAllTodos();
        _logger.DebugNew("Test bug");

        return TypedResults.Ok(todos.ToList());
    } 
}

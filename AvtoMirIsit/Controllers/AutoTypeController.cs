using AvtoMirIsit.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AvtoMirIsit.Controllers;

[ApiController]
[Route("[controller]")]
public class AutoTypeController : ControllerBase
{
    private readonly IAutoTypeService _service;
    public AutoTypeController(IAutoTypeService service)
    {
        _service = service;
    }

    [HttpGet("getAll")]
    public IActionResult GetAll()
    {
        var rez = _service.GetAll().FromSql($"SELECT * FROM ТипАвтомобиля").ToList();
        return Ok(rez);
    }
}
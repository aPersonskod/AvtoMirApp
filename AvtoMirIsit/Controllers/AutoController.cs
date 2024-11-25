using AvtoMirIsit.Services;
using AvtoMirModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AvtoMirIsit.Controllers;

[ApiController]
[Route("[controller]")]
public class AutoController : ControllerBase
{
    private readonly IAvtoService _service;
    public AutoController(IAvtoService service)
    {
        _service = service;
    }

    [HttpGet("getAllEntity")]
    public IActionResult GetAll()
    {
        var rez = _service.GetAll();
        return Ok(rez);
    }

    [HttpGet("getAll")]
    public IActionResult GetAll2()
    {
        var rez = _service.GetAll().FromSql($"SELECT * FROM Автомобиль").ToList();
        //var rez = _service.GetAll();
        return Ok(rez);
    }
    
    [HttpPost("create")]
    public Auto Create(Auto auto)
    {
        return _service.Create(auto);
    }
}
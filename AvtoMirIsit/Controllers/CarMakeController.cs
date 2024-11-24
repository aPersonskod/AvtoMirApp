using AvtoMirIsit.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AvtoMirIsit.Controllers;

[ApiController]
[Route("[controller]")]
public class CarMakeController : ControllerBase
{
    private readonly ICarMakeService _service;
    public CarMakeController(ICarMakeService service)
    {
        _service = service;
    }

    [HttpGet("getAll")]
    public IActionResult GetAll()
    {
        var rez = _service.GetAll().FromSql($"SELECT * FROM МаркаАвтомобиля").ToList();
        return Ok(rez);
    }
}
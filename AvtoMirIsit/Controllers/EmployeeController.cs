using AvtoMirIsit.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AvtoMirIsit.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _service;
    public EmployeeController(IEmployeeService service)
    {
        _service = service;
    }

    [HttpGet("getAll")]
    public IActionResult GetAll()
    {
        var rez = _service.GetAll().FromSql($"SELECT * FROM Сотрудник").ToList();
        return Ok(rez);
    }
}
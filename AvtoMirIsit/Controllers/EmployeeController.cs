using AvtoMirIsit.Services;
using AvtoMirModel;
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
        var rez = _service.GetAll().FromSql($"SELECT * FROM Сотрудник").ToList()?.OrderBy(x => x.Id);
        return Ok(rez);
    }
    
    [HttpPost("create")]
    public Employee Create(Employee employee)
    {
        return _service.Create(employee);
    }
    
    [HttpPatch("update")]
    public Employee Update(Employee employee)
    {
        return _service.Update(employee);
    }
    
    [HttpDelete("delete/{id:int}")]
    public IActionResult Delete(int id)
    {
        _service.Delete(id);
        return Ok();
    }
}
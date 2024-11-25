using AvtoMirIsit.Services;
using AvtoMirModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AvtoMirIsit.Controllers;

[ApiController]
[Route("[controller]")]
public class DogovorController : ControllerBase
{
    private readonly IDogovorService _service;
    public DogovorController(IDogovorService service)
    {
        _service = service;
    }

    [HttpGet("getAll")]
    public IActionResult GetAll()
    {
        var rez = _service.GetAll().FromSql($"SELECT * FROM Договор").ToList();
        return Ok(rez);
    }
    
    [HttpPost("create")]
    public Dogovor Create(Dogovor dogovor)
    {
        return _service.Create(dogovor);
    }
}
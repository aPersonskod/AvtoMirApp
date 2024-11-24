using AvtoMirIsit.Services;
using AvtoMirModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AvtoMirIsit.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _service;
    public ClientController(IClientService service)
    {
        _service = service;
    }

    [HttpGet("getAll")]
    public IActionResult GetAll()
    {
        var rez = _service.GetAll().FromSql($"SELECT * FROM Клиент").ToList()?.OrderBy(x => x.Id);
        return Ok(rez);
    }
    
    [HttpPost("create")]
    public Client Create(Client client)
    {
        return _service.Create(client);
    }
    
    [HttpPatch("update")]
    public Client Update(Client client)
    {
        return _service.Update(client);
    }
    
    [HttpDelete("delete/{id:int}")]
    public IActionResult Delete(int id)
    {
        _service.Delete(id);
        return Ok();
    }
}
using AvtoMirIsit.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AvtoMirIsit.Controllers;

[ApiController]
[Route("[controller]")]
public class ShopController : ControllerBase
{
    private readonly IShopService _service;
    public ShopController(IShopService service)
    {
        _service = service;
    }

    [HttpGet("getAll")]
    public IActionResult GetAll()
    {
        var rez = _service.GetAll().FromSql($"SELECT * FROM Магазин").ToList();
        return Ok(rez);
    }
}
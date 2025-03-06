using Buisness.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class WarehousesController : ControllerBase
{
    private readonly IWarehouseService _warehouseService;

    public WarehousesController(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }

    [HttpPost("Add")]
    public IActionResult Add(Warehouse warehouse)
    {
        var result = _warehouseService.Add(warehouse);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpPost("Update")]
    public IActionResult Update(Warehouse warehouse, string id)
    {
        var result = _warehouseService.Update(warehouse, id);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpGet("Delete")]
    public IActionResult Delete(string id)
    {
        var result = _warehouseService.Delete(id);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpGet("GetById")]
    public IActionResult GetById(string id)
    {
        var result = _warehouseService.GetById(id);
        if (result.Success)
        {
            return Ok(result);
        }
        return NotFound(result);
    }

    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        var result = _warehouseService.GetAll();
        return Ok(result);
    }
}

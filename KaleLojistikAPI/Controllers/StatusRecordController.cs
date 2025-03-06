using Buisness.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class StatusRecordController : ControllerBase
{
    private readonly IStatusRecordService _statusRecordService;

    public StatusRecordController(IStatusRecordService statusRecordService)
    {
        _statusRecordService = statusRecordService;
    }

    [HttpPost("Add")]
    public IActionResult Add(StatusRecord statusRecord)
    {
        var result = _statusRecordService.Add(statusRecord);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

}

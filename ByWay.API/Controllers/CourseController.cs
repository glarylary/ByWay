using ByWay.Application.DTO;
using ByWay.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _svc;
    public CoursesController(ICourseService svc) { _svc = svc; }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] CourseFilterDTO filter)
    {
        var list = await _svc.GetCoursesFilteredAsync(filter);
        return Ok(list);
    }

    [HttpGet("count")]
    public async Task<IActionResult> Count() => Ok(await _svc.GetCourseCountAsync());
}


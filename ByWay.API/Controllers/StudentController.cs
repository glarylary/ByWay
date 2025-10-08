using ByWay.Application.DTO;
using ByWay.Application.Interfaces;
using ByWay.Application.services;
using Microsoft.AspNetCore.Mvc;

namespace ByWay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(StudentCreateDTO dto)
        {
            var result = await _studentService.Register(dto);
            if (!result.State)
                return BadRequest(result.Masssege);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(StudentLoginDTO dto)
        {
            var result = await _studentService.Login(dto);
            if (!result.State)
                return Unauthorized(result.Masssege);

            return Ok(result);
        }

        [HttpPost("purchase")]
        public async Task<IActionResult> PurchaseCourse(CreatePurchaseDTO dto)
        {
            var result = await _studentService.PurchaseCourseAsync(dto);
            if (!result.IsSuccessful)
                return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpGet("GetAllStudents")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }
    }
}

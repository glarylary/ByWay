using Microsoft.AspNetCore.Mvc;
using ByWay.Application.Services;
using ByWay.Application.DTO;

namespace ByWay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _adminService;

        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpPost("login")]
        // POST: api/admin/login
        public async Task<IActionResult> Login([FromBody] AdminLoginDTO dto)
        {
            var result = await _adminService.LoginAsync(dto);
            if (!result.State)
                return Unauthorized(result.Masssege);
            return Ok(result);
        }
        [HttpGet("number-of-tutors")]
        public async Task<IActionResult> GetNumberOfTutors()
        {
            var result = await _adminService.NumberOfTutors();
            return Ok(result);
        }
        [HttpGet("number-of-courses")]
        public async Task<IActionResult> GetNumberOfCourses()
        {
            var result = await _adminService.NumberOfCourses();
            return Ok(result);
        }
        [HttpGet("number-of-subjects")]
        public async Task<IActionResult> GetNumberOfSubjects()
        {
            var result = await _adminService.NumberOfSubjects();
            return Ok(result);
        }
        // Course Management
        [HttpGet("GetAllcourses")]
        public async Task<IActionResult> GetAllCoursesPaginated(int page = 1, int pageSize = 5, string search = null)
        {
            var result = await _adminService.GetAllCoursesPaginated(page, pageSize, search);
            return Ok(result);
        }
        [HttpGet("GetCourseByID/{courseId}")]
        public async Task<IActionResult> GetCourseByID(int courseId)
        {
            var result = await _adminService.GetCourseByID(courseId);
            return Ok(result);
        }
        [HttpPost("AddNewCourse")]
        public async Task<IActionResult> AddNewCourse([FromBody] CourseCreateDTO courseCreateDTO)
        {
            var result = await _adminService.AddNewCourse(courseCreateDTO);
            if (!result.IsCreated)
                return BadRequest(result.Masssege);
            return Ok(result);
        }
        [HttpPut("EditCourse/{id}")]
        public async Task<IActionResult> EditCourse(int id, [FromBody] CourseCreateDTO courseCreateDTO)
        {
            var result = await _adminService.EditCourse(id, courseCreateDTO);
            if (!result.IsCreated)
                return BadRequest(result.Masssege);
            return Ok(result);
        }
        [HttpDelete("DeleteCourse/{courseId}")]
        public async Task<IActionResult> DeleteCourse(int courseId)
        {
            var result = await _adminService.DeleteCourse(courseId);
            if (!result.IsCreated)
                return BadRequest(result.Masssege);
            return Ok(result);
        }
        // Tutor Management
        [HttpGet("GetAllTutors")]
        public async Task<IActionResult> GetAllTutorsPaginated(int page = 1, int pageSize = 5, string search = null)
        {
            var result = await _adminService.GetAllTutorsPaginated(page, pageSize, search);
            return Ok(result);
        }
        [HttpGet("GetTutorByID/{tutorId}")]
        public async Task<IActionResult> GetTutorByID(int tutorId)
        {
            var result = await _adminService.GetTutorByID(tutorId);
            return Ok(result);
        }
        [HttpPost("AddNewTutor")]
        public async Task<IActionResult> AddNewTutor([FromBody] TutorCreateDTO tutorCreateDTO)
        {
            var result = await _adminService.AddNewTutor(tutorCreateDTO);
            if (!result.IsCreated)
                return BadRequest(result.Masssege);
            return Ok(result);
        }
        [HttpPut("EditTutor")]
        public async Task<IActionResult> EditTutor([FromBody] TutorCreateDTO tutorCreateDTO)
        {
            var result = await _adminService.EditTutor(tutorCreateDTO);
            if (!result.IsCreated)
                return BadRequest(result.Masssege);
            return Ok(result);
        }
        [HttpDelete("DeleteTutor/{tutorId}")]
        public async Task<IActionResult> DeleteTutor(int tutorId)
        {
            var result = await _adminService.DeleteTutor(tutorId);
            if (!result.IsCreated)
                return BadRequest(result.Masssege);
            return Ok(result);
        }

    }
}


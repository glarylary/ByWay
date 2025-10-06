using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ByWay.Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ByWay.Application.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseResponseDTO>> GetAllCoursesAsync();
        Task<IEnumerable<CourseResponseDTO>> GetCoursesFilteredAsync(CourseFilterDTO filter);
        Task<CourseResponseDTO> GetCourseByNameAsync(string title);
        Task<int> GetCourseCountAsync();
    }
}

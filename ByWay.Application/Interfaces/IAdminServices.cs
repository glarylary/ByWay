using ByWay.Application.DTO;
using ByWay.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByWay.Application.services
{
    public interface IAdminService
    {
        Task<AdminResponseDTO?> LoginAsync(AdminLoginDTO dto);
        Task<object> NumberOfTutors();
        Task<object> NumberOfCourses();
        Task<object> NumberOfSubjects();

        //Course Management
        Task<IEnumerable<object>> GetAllCoursesPaginated(int page = 1, int pageSize = 5, string search = null);
        Task<object> GetCourseByID(int courseId);
        Task<CourseVerifyModel> AddNewCourse(CourseCreateDTO courseCreateDTO);
        Task<CourseVerifyModel> EditCourse(int id, CourseCreateDTO courseCreateDTO);
        Task<CourseVerifyModel> DeleteCourse(int courseId);

        //Tutor Management
        Task<IEnumerable<object>> GetAllTutorsPaginated(int page = 1, int pageSize = 5, string search = null);
        Task<object> GetTutorByID(int tutorId);
        Task<TutorVerifyModel> AddNewTutor(TutorCreateDTO tutorCreateDTO);
        Task<TutorVerifyModel> EditTutor(TutorCreateDTO tutorCreateDTO);
        Task<TutorVerifyModel> DeleteTutor(int tutorId);
    }
}

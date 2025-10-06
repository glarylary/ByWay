using ByWay.Application.DTO;
using ByWay.Application.services;
using ByWay.Domain;
using ByWay.Infrastructure.Interfaces;
using ByWay.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ByWay.Application.Services
{
    public class AdminService: IAdminServices
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AdminService(
            IUnitOfWork unitOfWork,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _UnitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //This is the dashboard for admin

        public async Task<object> NumberOfTutors()
        {
            var NumberOfTutors = await _UnitOfWork.Tutors.Count();
            return new { tutors = NumberOfTutors };
        }

        public async Task<object> NumberOfCourses()
        {
            var NumberOfCourses = await _UnitOfWork.Courses.Count();
            return new { courses = NumberOfCourses };
        }
        public async Task<object> NumberOfSubjects()
        {
            var NumberOfSubjects = await _UnitOfWork.Subjects.Count();
            return new { subjects = NumberOfSubjects };
        }
        //course management
        public async Task<IEnumerable<object>> GetAllCoursesPaginated(int page = 1, int pageSize = 5, string search = null)
        {
            var query = _UnitOfWork.Courses.GetAllQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.Title.Contains(search));
            }

            var coursesPaginated = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var CourseList = coursesPaginated.Select(c => new
            {
                c.Id,
                c.Title,
                c.Description,
                c.Price,
                TutorName = c.Tutor != null ? c.Tutor.UserName : "No Tutor Assigned",
                SubjectName = c.Subject != null ? c.Subject.Name : "No Subject Assigned"
            });

            return await Task.FromResult(CourseList);
        }

        public async Task<object> GetCourseByID(int courseId)
        {
            var course = await _UnitOfWork.Courses.GetByIdAsync(courseId);
            if (course == null)
            {
                return new { Message = "Course not found" };
            }
            var courseDetails = new
            {
                ID = course.Id,
                Title = course.Title,
                Description = course.Description,
                Price = course.Price,
                Tutor = course.Tutor != null ? course.Tutor.UserName : "No Tutor Assigned",
                SubjectName = course.Subject != null ? course.Subject.Name : "No Subject Assigned"
            };
            return courseDetails;
        }

        public async Task<CourseVerifyModel> AddNewCourse(CourseCreateDTO courseCreateDTO)
        {
            var existingCourse = await _UnitOfWork.Courses.GetByTitleAsync(courseCreateDTO.Title);

            if (existingCourse != null)
            {
                return new CourseVerifyModel
                {
                    IsCreated = false,
                    Masssege = "Course with the same title already exists."
                };
            }

            var newCourse = new Course
            {
                Title = courseCreateDTO.Title,
                Description = courseCreateDTO.Description,
                SubjectId = courseCreateDTO.SubjectId,
                TutorId = courseCreateDTO.TutorId,
                Price = courseCreateDTO.Price
            };

            await _UnitOfWork.Courses.AddAsync(newCourse);
            _UnitOfWork.Complete();

            return new CourseVerifyModel
            {
                IsCreated = true,
                Masssege = "Course created successfully."
            };
        }

        public async Task<CourseVerifyModel> EditCourse(int id, CourseCreateDTO courseCreateDTO)
        {
            var CourseToUpdate = await _UnitOfWork.Courses.GetByIdAsync(id);
            if (CourseToUpdate == null)
                return new CourseVerifyModel { Masssege = "Course Doesn't exist" };

            CourseToUpdate.Title = courseCreateDTO.Title;
            CourseToUpdate.Description = courseCreateDTO.Description;
            CourseToUpdate.SubjectId = courseCreateDTO.SubjectId;
            CourseToUpdate.TutorId = courseCreateDTO.TutorId;
            CourseToUpdate.Price = courseCreateDTO.Price;

            _UnitOfWork.Complete();
            return new CourseVerifyModel { IsCreated = true, Masssege = "Course Updated Successfully" };
        }

        public async Task<CourseVerifyModel> DeleteCourse(int courseId)
        {
            var course = await _UnitOfWork.Courses.GetByIdAsync(courseId);
            if (course == null)
            {
                return new CourseVerifyModel
                {
                    IsCreated = false,
                    Masssege = "Course not found."
                };
            }
            if (course.Purchases != null && course.PurchasedStudents.Any())
            {
                return new CourseVerifyModel
                {
                    IsCreated = false,
                    Masssege = "Cannot delete course with active enrollments."
                };
            }

            var result = await _UnitOfWork.Courses.DeleteAsync(course);
            if (!result)
            {
                return new CourseVerifyModel
                {
                    IsCreated = false,
                    Masssege = "Failed to delete the course."
                };
            }

            return new CourseVerifyModel { Masssege = "Course Deleted Successfully!", IsCreated = true };
        }
        //Tutor Management
        public async Task<IEnumerable<object>> GetAllTutorsPaginated(int page = 1, int pageSize = 5, string search = null)
        {
            var query = _UnitOfWork.Tutors.GetAllQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(t => t.UserName.Contains(search));
            }
            var tutorsPaginated = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            var TutorList = tutorsPaginated.Select(t => new
            {
                t.Id,
                t.UserName,
                t.Email,
                SubjectExpertise = t.Subject != null ? t.Subject.Name : "No Subjects Assigned"
            });
            return await Task.FromResult(TutorList);
        }
        public async Task<object> GetTutorByID(int tutorId)
        {
            var tutor = await _UnitOfWork.Tutors.GetByIdAsync(tutorId);
            if (tutor == null)
            {
                return new { Message = "Tutor not found" };
            }
            var tutorDetails = new
            {
                ID = tutor.Id,
                UserName = tutor.UserName,
                Email = tutor.Email,
                SubjectExpertise = tutor.Subject != null ? tutor.Subject.Name : "No Subjects Assigned"
            };
            return tutorDetails;
        } 
        public async Task<TutorVerifyModel> AddNewTutor(TutorCreateDTO tutorCreateDTO)
        {
            var existingTutor = await _UnitOfWork.Tutors.GetByEmailAsync(tutorCreateDTO.Email);
            if (existingTutor != null)
            {
                return new TutorVerifyModel
                {
                    IsCreated = false,
                    Masssege = "Tutor with the same email already exists."
                };
            }
            var newTutor = new Tutor
            {
                UserName = tutorCreateDTO.FullName,
                Email = tutorCreateDTO.Email,
                SubjectId = tutorCreateDTO.SubjectId
            };
            await _UnitOfWork.Tutors.AddAsync(newTutor);
            _UnitOfWork.Complete();
            return new TutorVerifyModel
            {
                IsCreated = true,
                Masssege = "Tutor created successfully."
            };
        }
        public async Task<TutorVerifyModel> EditTutor(TutorCreateDTO tutorCreateDTO)
        {
            var TutorToUpdate = await _UnitOfWork.Tutors.GetByEmailAsync(tutorCreateDTO.Email);
            if (TutorToUpdate == null)
                return new TutorVerifyModel { Masssege = "Tutor Doesn't exist" };
            TutorToUpdate.UserName = tutorCreateDTO.FullName;
            TutorToUpdate.Email = tutorCreateDTO.Email;
            TutorToUpdate.SubjectId = tutorCreateDTO.SubjectId;
            _UnitOfWork.Complete();
            return new TutorVerifyModel { IsCreated = true, Masssege = "Tutor Updated Successfully" };
        }
        public async Task<TutorVerifyModel> DeleteTutor(int tutorId)
        {
            var tutor = await _UnitOfWork.Tutors.GetByIdAsync(tutorId);
            if (tutor == null)
            {
                return new TutorVerifyModel
                {
                    IsCreated = false,
                    Masssege = "Tutor not found."
                };
            }
            var coursesTaught = await _UnitOfWork.Courses.GetAllQueryable()
                .Where(c => c.TutorId == tutorId)
                .ToListAsync();
            if (coursesTaught.Any())
            {
                return new TutorVerifyModel
                {
                    IsCreated = false,
                    Masssege = "Cannot delete tutor assigned to courses."
                };
            }
            var result = await _UnitOfWork.Tutors.DeleteAsync(tutor.Id);
            if (!result)
            {
                return new TutorVerifyModel
                {
                    IsCreated = false,
                    Masssege = "Failed to delete the tutor."
                };
            }
            return new TutorVerifyModel { Masssege = "Tutor Deleted Successfully!", IsCreated = true };
        }
    }
}

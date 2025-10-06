using ByWay.Application.DTO;
using ByWay.Application.Interfaces;
using ByWay.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;

namespace ByWay.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _uow;
        public CourseService(IUnitOfWork uow) { _uow = uow; }

        public async Task<IEnumerable<CourseResponseDTO>> GetAllCoursesAsync()
        {
            var list = await _uow.Courses.GetAllAsync();
            return list.Select(c => new CourseResponseDTO
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Price = c.Price,
                Rating = c.Rating,
                NumberOfLectures = c.NumberOfLectures,
                SubjectId = c.SubjectId,
                SubjectName = c.Subject?.Name,
                TutorId = c.TutorId,
                TutorName = c.Tutor?.UserId 
            }).ToList();
        }

        public async Task<IEnumerable<CourseResponseDTO>> GetCoursesFilteredAsync(CourseFilterDTO filter)
        {
            var query = _uow.Courses.GetAllQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Search))
                query = query.Where(c => c.Title.Contains(filter.Search) || c.Description.Contains(filter.Search));

            if (filter.CategoryId.HasValue)
                query = query.Where(c => c.SubjectId == filter.CategoryId.Value);

            if (filter.MinLectures.HasValue)
                query = query.Where(c => c.NumberOfLectures >= filter.MinLectures.Value);

            if (filter.MinRating.HasValue)
                query = query.Where(c => c.Rating >= filter.MinRating.Value);

            if (filter.MaxPrice.HasValue)
                query = query.Where(c => c.Price <= filter.MaxPrice.Value);

            query = query.Skip((filter.Page - 1) * filter.PageSize).Take(filter.PageSize);

            var courses = await query.ToListAsync();
            return courses.Select(c => new CourseResponseDTO
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Price = c.Price,
                Rating = c.Rating,
                NumberOfLectures = c.NumberOfLectures,
                SubjectId = c.SubjectId,
                SubjectName = c.Subject?.Name,
                TutorId = c.TutorId,
                TutorName = c.Tutor?.UserId
            }).ToList();
        }

        public async Task<CourseResponseDTO> GetCourseByNameAsync(string title)
        {
            var c = (await _uow.Courses.GetAllAsync()).FirstOrDefault(x => x.Title == title);
            if (c == null) return null;
            return new CourseResponseDTO
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Price = c.Price,
                Rating = c.Rating,
                NumberOfLectures = c.NumberOfLectures,
                SubjectId = c.SubjectId,
                SubjectName = c.Subject?.Name,
                TutorId = c.TutorId,
                TutorName = c.Tutor?.UserId
            };
        }

        public async Task<int> GetCourseCountAsync()
        {
            var all = await _uow.Courses.GetAllAsync();
            return all.Count();
        }
    }
}

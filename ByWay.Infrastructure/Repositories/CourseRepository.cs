using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ByWay.Domain;
using ByWay.Infrastructure.Data;
using ByWay.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ByWay.Infrastructure.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        private readonly AppDbContext _context;
        public CourseRepository(AppDbContext context) : base(context)
        {
            this._context = context;
        }
        public async Task<int> Count()
        {
            return await Task.FromResult(_context.Courses.Count());
        }
        public async Task<IEnumerable<Course>> GetCOurseBySubjectAsync(string subjectExpertise)
        {
            return await Task.FromResult(_context.Courses.Where(c => c.Subject.Name == subjectExpertise).AsEnumerable());
        }
        public async Task<Course> GetByTitleAsync(string title)
        {
            return await _context.Courses
                .FirstOrDefaultAsync(c => c.Title == title);
        }
        public async Task<bool> DeleteAsync(Course course)
        {
            _context.Courses.Remove(course);
            return await Task.FromResult(true);
        }
        public IQueryable<Course> GetAllQueryable()
        {
            return _context.Courses
                .Include(c => c.Subject)
                .Include(c => c.Tutor)
                .AsQueryable();
        }
    }
}


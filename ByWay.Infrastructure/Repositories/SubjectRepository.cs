using ByWay.Domain;
using ByWay.Infrastructure.Data;
using ByWay.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace ByWay.Infrastructure.Repositories
{
    public class SubjectRepository: GenericRepository<Subject>, ISubjectRepository
    {
        private readonly AppDbContext _context;
        public SubjectRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Course>> GetCourses(int subjectID)
        {
            var subject = await _context.Subjects.FindAsync(subjectID);
            if (subject == null)
            {
                return new List<Course>();
            }
            var courses = await _context.Courses
                .Where(c => c.SubjectId == subjectID)
                .ToListAsync();
            return courses;
        }
    }
}

using ByWay.Domain;
using ByWay.Infrastructure.Data;
using ByWay.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByWay.Infrastructure.Repositories
{
    public class TutorRepository: GenericRepository<Tutor>, ITutorRepositery
    {
        private readonly AppDbContext _context;
        public TutorRepository(AppDbContext context) : base(context)
        {
            this._context = context;
        }
        public async Task<int> Count()
        {
            return await Task.FromResult(_context.Tutors.Count());
        }
        public async Task<List<Tutor>> GetTutorBySubjectAsync(int subjectID)
        {
            return await Task.FromResult(_context.Tutors.Where(t => t.SubjectId == subjectID).ToList());
        }
    }
}

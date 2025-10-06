using ByWay.Domain;
using ByWay.Infrastructure.Data;
using ByWay.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByWay.Infrastructure.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(AppDbContext context) : base(context) { }

        public async Task<int> Count()
        {
            var NumberOfStudents = _context.Students.Count();
            return await Task.FromResult(NumberOfStudents);
        }
    }
}

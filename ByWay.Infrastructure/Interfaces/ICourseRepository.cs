using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ByWay.Domain;

namespace ByWay.Infrastructure.Interfaces
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        Task<int> Count();
        IQueryable<Course> GetAllQueryable();
        Task<IEnumerable<Course>> GetCOurseBySubjectAsync(string subjectExpertise);
        Task<Course> GetByTitleAsync(string title);
        Task<bool> DeleteAsync(Course course);
    }
}


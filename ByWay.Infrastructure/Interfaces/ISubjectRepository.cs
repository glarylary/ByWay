using ByWay.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByWay.Infrastructure.Interfaces
{
    public interface ISubjectRepository: IGenericRepository<Subject>
    {
        Task<List<Course>> GetCourses(int courseID);
    }
}

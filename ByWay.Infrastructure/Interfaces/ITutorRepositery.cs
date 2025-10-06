using ByWay.Domain;
using ByWay.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByWay.Infrastructure.Interfaces
{
    public interface ITutorRepositery: IGenericRepository<Tutor>
    {
        Task<int> Count();
        Task<List<Tutor>> GetTutorBySubjectAsync(int subjectID);
        Task<Tutor> GetByEmailAsync(string Email);

    }
}

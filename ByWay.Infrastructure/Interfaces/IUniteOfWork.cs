using ByWay.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByWay.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        IAdminRepository Admins { get; }
        IGenericRepository<Student> Students { get; }
        ICourseRepository Courses { get; }
        IGenericRepository<Tutor> Tutors { get; }
        IGenericRepository<Purchase> Purchases { get; }
        IGenericRepository<Subject> Subjects { get; }
        Task<int> Complete();
    }
}


using ByWay.Domain;
using ByWay.Infrastructure.Data;
using ByWay.Infrastructure.Interfaces;
using System.Threading.Tasks;

namespace ByWay.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context, ITutorRepositery Tutors, IStudentRepository Students, ICourseRepository Courses, ISubjectRepository Subjects, IPurchaseRepository Purchases)
        {
            _context = context;
            Courses = new CourseRepository(_context);
            Students = new StudentRepository(_context);
            Tutors = new TutorRepository(_context);
            Subjects = new SubjectRepository(_context);
            Purchases = new PurchaseRepository(_context);
        }
        public IGenericRepository<Student> Students { get; private set; }
        public ICourseRepository Courses { get; private set; }
        public IGenericRepository<Tutor> Tutors { get; private set; }
        public IGenericRepository<Purchase> Purchases { get; private set; }
        public IGenericRepository<Subject> Subjects { get; private set; }

        public async Task<int> Complete() => await _context.SaveChangesAsync();
    }
}


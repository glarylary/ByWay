using ByWay.Domain;
using ByWay.Infrastructure.Data;
using ByWay.Infrastructure.Interfaces;
using System.Threading.Tasks;

namespace ByWay.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IGenericRepository<Student> Students { get; private set; }
        public ICourseRepository Courses { get; private set; }
        public IGenericRepository<Tutor> Tutors { get; private set; }
        public IGenericRepository<Purchase> Purchases { get; private set; }
        public IGenericRepository<Subject> Subjects { get; private set; }

        public UnitOfWork(
            AppDbContext context,
            ITutorRepositery tutorRepository,
            IStudentRepository studentRepository,
            ICourseRepository courseRepository,
            ISubjectRepository subjectRepository,
            IPurchaseRepository purchaseRepository)
        {
            _context = context;

            Courses = courseRepository;
            Students = studentRepository;
            Tutors = tutorRepository;
            Subjects = subjectRepository;
            Purchases = purchaseRepository;
        }

        public async Task<int> Complete() => await _context.SaveChangesAsync();
    }

}


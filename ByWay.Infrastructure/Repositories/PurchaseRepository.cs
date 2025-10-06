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
    public class PurchaseRepository : GenericRepository<Purchase>, IPurchaseRepository
    {
        public readonly AppDbContext _context;

        public PurchaseRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Purchase> GetPurchaseByIdAsync(int id)
        {
            return await _context.Purchases.FindAsync(id);
        }
        public async Task<Purchase> GetPurchaseByUserIdAndCourseIdAsync(int userId, int courseId)
        {
            return await _context.Purchases
                .FirstOrDefaultAsync(p => p.StudentId == userId && p.CourseId == courseId);
        }
    }
}

using ByWay.Domain;
using ByWay.Infrastructure.Data;
using ByWay.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ByWay.Infrastructure.Repositories
{
    public class AdminRepository : GenericRepository<Admin>, IAdminRepository
    {
        public AdminRepository(AppDbContext context) : base(context) { }

        public async Task<Admin> GetAdminByEmailAsync(string email)
        {
            return await _context.Admins.FirstOrDefaultAsync(a => a.Email == email);
        }
    }
}

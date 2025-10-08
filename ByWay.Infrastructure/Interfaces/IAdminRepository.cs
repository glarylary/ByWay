using ByWay.Domain;
using ByWay.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByWay.Infrastructure.Interfaces
{
    public interface IAdminRepository
    {
        // Add method signatures specific to Admin entity if needed
        public Task<Admin> GetAdminByEmailAsync(string email);
    }

}

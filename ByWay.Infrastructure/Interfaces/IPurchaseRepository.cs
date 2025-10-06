using ByWay.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByWay.Infrastructure.Interfaces
{
    public interface IPurchaseRepository: IGenericRepository<Purchase>
    {
        Task<Purchase> GetPurchaseByIdAsync(int id);
        Task<Purchase> GetPurchaseByUserIdAndCourseIdAsync(int userId, int courseId);
    }
}

using ByWay.Application.DTO;
using ByWay.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByWay.Application.services
{
    public interface IStudentService
    {
        Task<StudentVerifyModel> Register(StudentCreateDTO studentCreateDto);
        Task<StudentVerifyModel> Login(StudentLoginDTO studentLoginDto);
        Task<VerifyPurchaseDTO> PurchaseCourseAsync(CreatePurchaseDTO purchaseDto);
        Task<IEnumerable<StudentResponseDTO>> GetAllStudentsAsync();
    }
}

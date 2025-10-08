using ByWay.Application.DTO;
using ByWay.Application.Interfaces;
using ByWay.Application.services;
using ByWay.Domain;
using ByWay.Infrastructure.Interfaces;
using ByWay.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ByWay.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public StudentService(
            IUnitOfWork unitOfWork,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<StudentVerifyModel> Register(StudentCreateDTO studentCreateDto)
        {
            var existingUser = await _userManager.FindByEmailAsync(studentCreateDto.Email);
            if (existingUser != null)
            {
                return new StudentVerifyModel
                {
                    State = false,
                    Masssege = "Email already exists."
                };
            }

            var newUser = new IdentityUser
            {
                UserName = studentCreateDto.Email,
                Email = studentCreateDto.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(newUser, studentCreateDto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new StudentVerifyModel
                {
                    State = false,
                    Masssege = $"Failed to create user: {errors}"
                };
            }

            await _userManager.AddToRoleAsync(newUser, "Student");

            // Create a Student entity in the domain
            var student = new Student
            {
                UserId = newUser.Id,
                Name = studentCreateDto.Name
            };

            await _unitOfWork.Students.AddAsync(student);
            await _unitOfWork.Complete();

            return new StudentVerifyModel
            {
                State = true,
                Masssege = "Student registered successfully."
            };
        }
        public async Task<StudentVerifyModel> Login(StudentLoginDTO studentLoginDto)
        {
            var user = await _userManager.FindByEmailAsync(studentLoginDto.Email);
            if (user == null)
            {
                return new StudentVerifyModel
                {
                    State = false,
                    Masssege = "Invalid email or password."
                };
            }

            var result = await _signInManager.PasswordSignInAsync(user, studentLoginDto.Password, false, false);
            if (!result.Succeeded)
            {
                return new StudentVerifyModel
                {
                    State = false,
                    Masssege = "Invalid email or password."
                };
            }

            return new StudentVerifyModel
            {
                State = true,
                Masssege = "Login successful."
            };
        }
        public async Task<VerifyPurchaseDTO> PurchaseCourseAsync(CreatePurchaseDTO purchaseDto)
        {

            var student = await _unitOfWork.Students.GetByIdAsync(purchaseDto.StudentId);
            if (student == null)
            {
                return new VerifyPurchaseDTO
                {
                    IsSuccessful = false,
                    Message = "Student not found."
                };
            }


            var course = await _unitOfWork.Courses.GetByIdAsync(purchaseDto.CourseId);
            if (course == null)
            {
                return new VerifyPurchaseDTO
                {
                    IsSuccessful = false,
                    Message = "Course not found."
                };
            }


            var existing = await _unitOfWork.Purchases
                .GetAllQueryable()
                .FirstOrDefaultAsync(p => p.StudentId == student.Id && p.CourseId == course.Id);

            if (existing != null)
            {
                return new VerifyPurchaseDTO
                {
                    IsSuccessful = false,
                    Message = "You already purchased this course."
                };
            }


            var newPurchase = new Purchase
            {
                StudentId = student.Id,
                CourseId = course.Id,
                Price = purchaseDto.AmountPaid == 0 ? course.Price : purchaseDto.AmountPaid,
            };

            await _unitOfWork.Purchases.AddAsync(newPurchase);
            await _unitOfWork.Complete();

            // 5️⃣ Return response
            return new VerifyPurchaseDTO
            {
                IsSuccessful = true,
                Message = "Course purchased successfully."
            };
        }
        public async Task<IEnumerable<StudentResponseDTO>> GetAllStudentsAsync()
        {
            var students = await _unitOfWork.Students.GetAllAsync();

            return students.Select(s => new StudentResponseDTO
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email
            });
        }
    }
}

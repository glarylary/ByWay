using ByWay.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByWay.Infrastructure.Data
{
    public static class SeedingData
    {
        // Seed Subjects
        public static void SeedSubjects(ModelBuilder builder)
        {
            builder.Entity<Subject>().HasData(
                new Subject { Id = 1, Name = "React" },
                new Subject { Id = 2, Name = "Java" },
                new Subject { Id = 3, Name = "C++" },
                new Subject { Id = 4, Name = "JavaScript" },
                new Subject { Id = 5, Name = "ASP.NET" },
                new Subject { Id = 6, Name = "UI/UX" },
                new Subject { Id = 7, Name = "Python" },
                new Subject { Id = 8, Name = "C#" },
                new Subject { Id = 9, Name = "Embedded Systems" },
                new Subject { Id = 10, Name = "Cyber Security" }
            );
        }

        // Seed Roles (Admin, Tutor, Student)
        public static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "role-admin", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "role-tutor", Name = "Tutor", NormalizedName = "TUTOR" },
                new IdentityRole { Id = "role-student", Name = "Student", NormalizedName = "STUDENT" }
            );
        }

        // Seed Tutors and Students as IdentityUsers + Domain entities
        public static void SeedUsers(ModelBuilder builder)
        {
            var Admin = new IdentityUser
            {
                Id = "user-admin",
                UserName = "Admin1",
                Email = "admin@Byway.com",
                PasswordHash = "AQAAAAEAACcQAAAAEBHCtKqj/4AyA7TtM6uBnZ1uGm1RkIYfVqRrFWqLqIuEuwfbnE0jMoVqq5R0OqJp1g==", // Password: Admin@123
            };

            var tutor1 = new IdentityUser
            {
                Id = "user-tutor1",
                UserName = "tutor1@gmail.com",
                NormalizedUserName = "TUTOR1@GMAIL.COM",
                Email = "tutor1@gmail.com",
                NormalizedEmail = "TUTOR1@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEBHCtKqj/4AyA7TtM6uBnZ1uGm1RkIYfVqRrFWqLqIuEuwfbnE0jMoVqq5R0OqJp1g=="
            };

            var tutor2 = new IdentityUser
            {
                Id = "user-tutor2",
                UserName = "tutor2@gmail.com",
                NormalizedUserName = "TUTOR2@GMAIL.COM",
                Email = "tutor2@gmail.com",
                NormalizedEmail = "TUTOR2@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEBHCtKqj/4AyA7TtM6uBnZ1uGm1RkIYfVqRrFWqLqIuEuwfbnE0jMoVqq5R0OqJp1g=="
            };

            var student1 = new IdentityUser
            {
                Id = "user-student1",
                UserName = "student1@gmail.com",
                NormalizedUserName = "STUDENT1@GMAIL.COM",
                Email = "student1@gmail.com",
                NormalizedEmail = "STUDENT1@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEBHCtKqj/4AyA7TtM6uBnZ1uGm1RkIYfVqRrFWqLqIuEuwfbnE0jMoVqq5R0OqJp1g=="
            };


            builder.Entity<IdentityUser>().HasData(tutor1, tutor2, student1, Admin);

            // assign roles
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = "role-tutor", UserId = "user-tutor1" },
                new IdentityUserRole<string> { RoleId = "role-tutor", UserId = "user-tutor2" },
                new IdentityUserRole<string> { RoleId = "role-student", UserId = "user-student1" },
                new IdentityUserRole<string> { RoleId = "role-admin", UserId = "user-admin" }
            );

            // Domain Tutor entities
            builder.Entity<Tutor>().HasData(
                new Tutor { Id = 1, UserId = "user-tutor1", Bio = "Experienced React developer", ImageURL = "0", SubjectId = 1},
                new Tutor { Id = 2, UserId = "user-tutor2", Bio = "Java educator and backend expert", ImageURL = "2", SubjectId = 3 }
            );

            // Domain Student entity
            builder.Entity<Student>().HasData(
                new Student { Id = 1, UserId = "user-student1", Name = "Student One",  }
            );
        }

        // Seed Courses
        public static void SeedCourses(ModelBuilder builder)
        {
            builder.Entity<Course>().HasData(
                new Course {ThumbnailUrl = "Thumbnail1.Png", Id = 1, Title = "React for Beginners", Description = "Learn React basics", Price = 99.99m, Rating = 4.7, SubjectId = 1 , TutorId = 1, NumberOfLectures = 10 },
                new Course {ThumbnailUrl = "Thumbnail1.Png", Id = 2, Title = "Advanced Java", Description = "Deep dive into Java programming", Price = 149.99m, Rating = 4.8, SubjectId = 2, TutorId = 2, NumberOfLectures = 12 },
                new Course {ThumbnailUrl = "Thumbnail1.Png", Id = 3, Title = "JavaScript Fundamentals", Description = "Core JavaScript concepts", Price = 79.99m, Rating = 4.5, SubjectId = 4, TutorId = 1, NumberOfLectures = 8 }
            );
        }

        // Entry point — called from OnModelCreating
        public static void SeedAll(ModelBuilder builder)
        {
            SeedRoles(builder);
            SeedSubjects(builder);
            SeedCourses(builder);
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByWay.Domain
{
    public class Course
    {
        public string ThumbnailUrl { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public double Rating { get; set; }
        public int NumberOfLectures { get; set; }

        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }

        public int TutorId { get; set; }
        public virtual Tutor Tutor { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<Student> PurchasedStudents { get; set; }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByWay.Application.DTO
{
    public class CourseResponseDTO
    {
        public string ThumbnailURL { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public double Rating { get; set; }
        public int NumberOfLectures { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int TutorId { get; set; }
        public string TutorName { get; set; }
    }
}


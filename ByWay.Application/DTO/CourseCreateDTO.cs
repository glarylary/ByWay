using System.ComponentModel.DataAnnotations;

namespace ByWay.Application.DTO
{
    public class CourseCreateDTO
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
        [Required]
        public int SubjectId { get; set; }
        [Required]
        public int TutorId { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}

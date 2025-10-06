using ByWay.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByWay.Application.DTO
{
    public class StudentResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public virtual List<Course>? Courses { get; set; }
        public virtual List<Tutor>? FollowedTutors { get; set; }
    }
}

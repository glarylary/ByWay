using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByWay.Application.DTO
{
    public class CourseFilterDTO
    {
        public string Search { get; set; }
        public int? CategoryId { get; set; }
        public int? MinLectures { get; set; }
        public double? MinRating { get; set; }
        public decimal? MaxPrice { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}


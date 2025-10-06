using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByWay.Application.DTO
{
    public class CreatePurchaseDTO
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public decimal AmountPaid { get; set; }

        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByWay.Application.DTO
{
    public class StudentLoginDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

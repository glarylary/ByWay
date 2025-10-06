using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByWay.Application.DTO
{
    public class StudentCreateDTO
    {
        [Required]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(8)]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Required, MinLength(8)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public String ConfirmPassword { get; set; }

    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace ByWay.Domain
    {
        public class Student: IdentityUser
        {
            public int Id { get; set; }
            public string UserId { get; set; } 
            public string Name { get; set; }

        }
    }


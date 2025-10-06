using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace ByWay.Domain
    {
        public class Tutor: IdentityUser
    {
            public int Id { get; set; }
            public string UserId { get; set; } // identity user id
            public string Bio { get; set; }
            public string ImageURL { get; set; }
            public int SubjectId { get; set; }
            public virtual Subject Subject { get; set; }
        }
    }


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByWay.Application.Interfaces
{
    public class AdminResponseDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool State { get; set; }
        public string Masssege { get; set; }

    }
}

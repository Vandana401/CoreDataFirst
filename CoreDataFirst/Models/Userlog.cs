using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDataFirst.Models
{
    public class Userlog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int? Password { get; set; }
    }
}

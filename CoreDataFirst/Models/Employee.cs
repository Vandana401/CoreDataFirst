using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDataFirst.Models
{
    public class Employee
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Mobile { get; set; }
        public string Department { get; set; }
        public int Salary { get; set; }
    }
}

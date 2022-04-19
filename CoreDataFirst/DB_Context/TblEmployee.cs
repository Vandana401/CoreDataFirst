using System;
using System.Collections.Generic;

#nullable disable

namespace CoreDataFirst.DB_Context
{
    public partial class TblEmployee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Mobile { get; set; }
        public string Department { get; set; }
        public int Salary { get; set; }
    }
}

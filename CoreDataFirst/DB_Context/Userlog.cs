using System;
using System.Collections.Generic;

#nullable disable

namespace CoreDataFirst.DB_Context
{
    public partial class Userlog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int? Password { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models.Entity.User
{
    public class User
    {
        public int UserId { get; set; }
        // Chung minh thu nhan dan
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public string Email { get; set; }
    }
}

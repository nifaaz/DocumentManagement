using DocumentManagement.Models.Entity.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models.Entity.User
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdatedBy {get; set;}
        public DateTime UpdatedDate { get; set; }
        public int Status { get; set; }
        public Token Token { get; set; }
        public int RoleID { get; set; }
        public string UserRole { get; set; }
    }
}

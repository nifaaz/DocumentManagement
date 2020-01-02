using Common.Common;
using DocumentManagement.Common;
using DocumentManagement.DAL;
using DocumentManagement.Models.Entity.Account;
using DocumentManagement.Models.Entity.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.BUS
{
    public class RoleBUS
    {
        private RoleDAL _roleDAL;
        private RoleDAL RoleDAL
        {
            get
            {
                _roleDAL = new RoleDAL();
                return _roleDAL;
            }
        }
        public ReturnResult<Role> GetPaging(BaseCondition<Role> condition)
        {
            return RoleDAL.GetPaging(condition);
        }
        public ReturnResult<Role> CreateRole(Role role)
        {
            return RoleDAL.CreateRole(role);
        }

        public ReturnResult<Role> DeleteRole(Role role)
        {
            return RoleDAL.DeleteRole(role);
        }

        public ReturnResult<Role> EditRole(Role role)
        {
            return RoleDAL.EditRole(role);
        }

        public ReturnResult<Role> GetRolesByUserId(Account account)
        {
            return RoleDAL.GetRolesByUserId(account);
        }
    }
}

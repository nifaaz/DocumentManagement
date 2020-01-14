using DocumentManagement.Common;
using DocumentManagement.DAL;
using DocumentManagement.Models.Entity.Role;
using DocumentManagement.Models.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.BUS
{
    public class MenuBUS
    {
        private MenuDAL _menuDAL;
        private MenuDAL MenuDAL
        {
            get
            {
                _menuDAL = new MenuDAL();
                return _menuDAL;
            }
        }
        public ReturnResult<Menu> GetMenuByRoleId(Role role)
        {
            var result = MenuDAL.GetMenuByRoleId(role);
            return result;
        }
    }
}


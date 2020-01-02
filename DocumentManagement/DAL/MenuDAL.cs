using DocumentManagement.Common;
using DocumentManagement.Model;
using DocumentManagement.Models.Entity.Role;
using DocumentManagement.Models.Menu;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.DAL
{
    public class MenuDAL
    {
        public ReturnResult<Menu> GetMenuByRoleId(Role role)
        {
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            var result = new List<Menu>();
            dbProvider.SetQuery("MENU_GET_MENU_BY_ROLE_ID", CommandType.StoredProcedure)
                .SetParameter("RoleId", SqlDbType.Int, role.RoleId, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<Menu>(out result)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Menu>()
            {
                ItemList = result,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
            };
        }
    }
}

using Common.Common;
using DocumentManagement.Common;
using DocumentManagement.DAL;
using DocumentManagement.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.BUS
{
    public class CoQuanBUS
    {
        private CoQuanDAL coQuanDAL = CoQuanDAL.GetCoQuanDALInstance();
        private CoQuanBUS()
        {

        }
        private static CoQuanBUS _instance;
        public static CoQuanBUS GetCoQuanBusInstance()
        {
            if (_instance == null)
            {
                _instance = new CoQuanBUS();
            }
            return _instance;
        }

        public ReturnResult<CoQuan> GetCoQuanWithPaging(BaseCondition<CoQuan> condition)
        {
            return coQuanDAL.GetCoQuanWithPaging(condition);
        }

        public ReturnResult<CoQuan> GetCoQuanById(int id)
        {
            return coQuanDAL.GetCoQuanById(id);
        }
    }
}

using Common.Common;
using DocumentManagement.Common;
using DocumentManagement.DAL;
using DocumentManagement.Model.Entity.OrganType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.BUS
{
    public class LoaiCoQuanBUS
    {
        private OrganTypeDAL loaiCoQuanDAL = OrganTypeDAL.GetOrganTypeDALInstance;
        private LoaiCoQuanBUS()
        {

        }
        private static LoaiCoQuanBUS _instance;
        public static LoaiCoQuanBUS GetLoaiCoQuanBUSInstance()
        {
            if (_instance == null)
            {
                _instance = new LoaiCoQuanBUS();
            }
            return _instance;
        }
        public ReturnResult<OrganType> GetALLLoaiCoQuan()
        {
            return loaiCoQuanDAL.GetAllOrganType();
        }
    }
}

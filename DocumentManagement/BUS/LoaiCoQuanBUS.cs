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
        public ReturnResult<OrganType> GetAllOrganType()
        {
            return loaiCoQuanDAL.GetAllOrganType();
        }

        public ReturnResult<OrganType> OrganTypeGetSearchWithPaging(BaseCondition<OrganType> condi)
        {
            return loaiCoQuanDAL.OrganTypeGetSearchWithPaging(condi);
        }
        public ReturnResult<OrganType> CreateOrganType(OrganType organType)
        {
            return loaiCoQuanDAL.CreateOrganType(organType);
        }

        public ReturnResult<OrganType> GetOrganTypeByID(int id)
        {
            var rs = loaiCoQuanDAL.GetOrganTypeByID(id);
            return rs;
        }

        public ReturnResult<OrganType> DeleteOrganType(int id)
        {
            return loaiCoQuanDAL.DeleteOrganType(id);
        }

        public ReturnResult<OrganType> EditOrganType(OrganType organType)
        {
            return loaiCoQuanDAL.EditOrganType(organType);
        }
    }
}

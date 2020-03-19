using Common.Common;
using DocumentManagement.Common;
using DocumentManagement.DAL;
using DocumentManagement.Models.Entity.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.BUS
{
    public class LoaiVanBanBUS
    {
        private LoaiVanBanDAL loaiVanBanDAL = LoaiVanBanDAL.GetLoaiVanBanDALInstance;
        private LoaiVanBanBUS()
        {

        }
        private static LoaiVanBanBUS _instance;
        public static LoaiVanBanBUS GetLoaiVanBanBUSInstance()
        {
            if (_instance == null)
            {
                _instance = new LoaiVanBanBUS();
            }
            return _instance;
        }
        public ReturnResult<LoaiVanBan> LoaiVanBanGetSearchWithPaging(BaseCondition<LoaiVanBan> condi)
        {
            return loaiVanBanDAL.LoaiVanBanGetSearchWithPaging(condi);
        }

        public ReturnResult<LoaiVanBan> CreateLoaiVanBan(LoaiVanBan LoaiVanBan)
        {
            return loaiVanBanDAL.CreateLoaiVanBan(LoaiVanBan);
        }

        public ReturnResult<LoaiVanBan> GetLoaiVanBanByID(int id)
        {
            var rs = loaiVanBanDAL.GetLoaiVanBanByID(id);
            return rs;
        }

        public ReturnResult<LoaiVanBan> DeleteLoaiVanBan(int id)
        {
            return loaiVanBanDAL.DeleteLoaiVanBan(id);
        }

        public ReturnResult<LoaiVanBan> EditLoaiVanBan(LoaiVanBan LoaiVanBan)
        {
            return loaiVanBanDAL.EditLoaiVanBan(LoaiVanBan);
        }
    }
}

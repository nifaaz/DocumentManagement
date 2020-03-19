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
    public class TinhTrangVatLyBUS
    {
        private TinhTrangVatLyDAL tinhTranVatLyDAL = TinhTrangVatLyDAL.GetTinhTrangVatLyDALInstance;
        private TinhTrangVatLyBUS()
        {

        }
        private static TinhTrangVatLyBUS _instance;
        public static TinhTrangVatLyBUS GetTinhTrangVatLyBUSInstance()
        {
            if (_instance == null)
            {
                _instance = new TinhTrangVatLyBUS();
            }
            return _instance;
        }
        public ReturnResult<TinhTrangVatLy> TinhTrangVatLyGetSearchWithPaging(BaseCondition<TinhTrangVatLy> condi)
        {
            return tinhTranVatLyDAL.TinhTrangVatLyGetSearchWithPaging(condi);
        }

        public ReturnResult<TinhTrangVatLy> CreateTinhTrangVatLy(TinhTrangVatLy TinhTrangVatLy)
        {
            return tinhTranVatLyDAL.CreateTinhTrangVatLy(TinhTrangVatLy);
        }

        public ReturnResult<TinhTrangVatLy> GetTinhTrangVatLyByID(int id)
        {
            var rs = tinhTranVatLyDAL.GetTinhTrangVatLyByID(id);
            return rs;
        }

        public ReturnResult<TinhTrangVatLy> DeleteTinhTrangVatLy(int id)
        {
            return tinhTranVatLyDAL.DeleteTinhTrangVatLy(id);
        }

        public ReturnResult<TinhTrangVatLy> EditTinhTrangVatLy(TinhTrangVatLy TinhTrangVatLy)
        {
            return tinhTranVatLyDAL.EditTinhTrangVatLy(TinhTrangVatLy);
        }
    }
}

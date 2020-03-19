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
    public class NgonNguBUS
    {
        private NgonNguDAL ngonNguDAL = NgonNguDAL.GetNgonNguDALInstance;
        private NgonNguBUS()
        {

        }
        private static NgonNguBUS _instance;
        public static NgonNguBUS GetNgonNguBUSInstance()
        {
            if (_instance == null)
            {
                _instance = new NgonNguBUS();
            }
            return _instance;
        }
        public ReturnResult<NgonNgu> NgonNguGetSearchWithPaging(BaseCondition<NgonNgu> condi)
        {
            return ngonNguDAL.NgonNguGetSearchWithPaging(condi);
        }

        public ReturnResult<NgonNgu> CreateNgonNgu(NgonNgu NgonNgu)
        {
            return ngonNguDAL.CreateNgonNgu(NgonNgu);
        }

        public ReturnResult<NgonNgu> GetNgonNguByID(int id)
        {
            var rs = ngonNguDAL.GetNgonNguByID(id);
            return rs;
        }

        public ReturnResult<NgonNgu> DeleteNgonNgu(int id)
        {
            return ngonNguDAL.DeleteNgonNgu(id);
        }

        public ReturnResult<NgonNgu> EditNgonNgu(NgonNgu NgonNgu)
        {
            return ngonNguDAL.EditNgonNgu(NgonNgu);
        }
    }
}

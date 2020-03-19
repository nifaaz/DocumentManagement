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
    public class LoaiHoSoBUS
    {
        private LoaiHoSoDAL loaiHoSoDAL = LoaiHoSoDAL.GetLoaiHoSoDALInstance;
        private LoaiHoSoBUS()
        {

        }
        private static LoaiHoSoBUS _instance;
        public static LoaiHoSoBUS GetLoaiHoSoBUSInstance()
        {
            if (_instance == null)
            {
                _instance = new LoaiHoSoBUS();
            }
            return _instance;
        }
        public ReturnResult<LoaiHoSo> LoaiHoSoGetSearchWithPaging(BaseCondition<LoaiHoSo> condi)
        {
            return loaiHoSoDAL.LoaiHoSoGetSearchWithPaging(condi);
        }

        public ReturnResult<LoaiHoSo> CreateLoaiHoSo(LoaiHoSo LoaiHoSo)
        {
            return loaiHoSoDAL.CreateLoaiHoSo(LoaiHoSo);
        }

        public ReturnResult<LoaiHoSo> GetLoaiHoSoByID(int id)
        {
            var rs = loaiHoSoDAL.GetLoaiHoSoByID(id);
            return rs;
        }

        public ReturnResult<LoaiHoSo> DeleteLoaiHoSo(int id)
        {
            return loaiHoSoDAL.DeleteLoaiHoSo(id);
        }

        public ReturnResult<LoaiHoSo> EditLoaiHoSo(LoaiHoSo LoaiHoSo)
        {
            return loaiHoSoDAL.EditLoaiHoSo(LoaiHoSo);
        }
    }
}

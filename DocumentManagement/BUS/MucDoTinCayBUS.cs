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
    public class MucDoTinCayBUS
    {
        private MucDoTinCayDAL mucDoTinCayDAL = MucDoTinCayDAL.GetMucDoTinCayDALInstance;
        private MucDoTinCayBUS()
        {

        }
        private static MucDoTinCayBUS _instance;
        public static MucDoTinCayBUS GetMucDoTinCayBUSInstance()
        {
            if (_instance == null)
            {
                _instance = new MucDoTinCayBUS();
            }
            return _instance;
        }
        public ReturnResult<MucDoTinCay> MucDoTinCayGetSearchWithPaging(BaseCondition<MucDoTinCay> condi)
        {
            return mucDoTinCayDAL.MucDoTinCayGetSearchWithPaging(condi);
        }

        public ReturnResult<MucDoTinCay> CreateMucDoTinCay(MucDoTinCay MucDoTinCay)
        {
            return mucDoTinCayDAL.CreateMucDoTinCay(MucDoTinCay);
        }

        public ReturnResult<MucDoTinCay> GetMucDoTinCayByID(int id)
        {
            var rs = mucDoTinCayDAL.GetMucDoTinCayByID(id);
            return rs;
        }

        public ReturnResult<MucDoTinCay> DeleteMucDoTinCay(int id)
        {
            return mucDoTinCayDAL.DeleteMucDoTinCay(id);
        }

        public ReturnResult<MucDoTinCay> EditMucDoTinCay(MucDoTinCay MucDoTinCay)
        {
            return mucDoTinCayDAL.EditMucDoTinCay(MucDoTinCay);
        }
    }
}

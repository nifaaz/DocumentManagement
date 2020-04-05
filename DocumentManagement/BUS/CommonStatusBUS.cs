using DocumentManagement.Common;
using DocumentManagement.DAL;
using DocumentManagement.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.BUS
{
    public class CommonStatusBUS
    {
        private static CommonStatusDAL commonStatusDAL = CommonStatusDAL.GetCommonStatusDALInstance;
        private CommonStatusBUS() { }

        private static volatile CommonStatusBUS _instance;

        static object key = new object();

        public static CommonStatusBUS GetCommonStatusBUSInstance
        {
            get
            {
                lock (key)
                {
                    if (_instance == null)
                    {
                        _instance = new CommonStatusBUS();
                    }
                }

                return _instance;
            }

            private set
            {
                _instance = value;
            }
        }
        public ReturnResult<CommonStatusDTO> GetAllStatus()
        {
            var result = commonStatusDAL.GetAllStatus();
            return result;
        }

    }
}

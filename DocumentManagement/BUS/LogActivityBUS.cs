using Common.Common;
using DocumentManagement.Common;
using DocumentManagement.DAL;
using DocumentManagement.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.BUS
{
    public class LogActivityBUS
    {
        LogActivityDAL logActivityDAL = LogActivityDAL.GetLogActivityDALInstance;
        private LogActivityBUS() { }

        private static volatile LogActivityBUS _instance;
        static object key = new object();
        public static LogActivityBUS GetLogActivityBUSInstance
        {
            get
            {
                lock (key)
                {
                    if (_instance == null)
                    {
                        _instance = new LogActivityBUS();
                    }
                }

                return _instance;
            }

            private set
            {
                _instance = value;
            }

        }

        public ReturnResult<LogActivityDTO> GetSearchLogWithPaging(BaseCondition<LogActivityDTO> condition)
        {
            return logActivityDAL.GetSearchLogWithPaging(condition);
        }

    }
}

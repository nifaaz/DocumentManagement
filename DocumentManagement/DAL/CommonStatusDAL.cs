using DocumentManagement.Common;
using DocumentManagement.Model;
using DocumentManagement.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.DAL
{
    public class CommonStatusDAL
    {
        private CommonStatusDAL() { }

        private static volatile CommonStatusDAL _instance;

        static object key = new object();

        public static CommonStatusDAL GetCommonStatusDALInstance
        {
            get
            {
                lock (key)
                {
                    if (_instance == null)
                    {
                        _instance = new CommonStatusDAL();
                    }
                }

                return _instance;
            }

            private set
            {
                _instance = value;
            }
        }
        public async Task<ReturnResult<CommonStatusDTO>> GetAllStatus()
        {
            List<CommonStatusDTO> statuss = new List<CommonStatusDTO>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            try
            {
                dbProvider.SetQuery("STATUS_GET_ALL", CommandType.StoredProcedure)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<CommonStatusDTO>(out statuss)
                .Complete();
            }
            catch (Exception)
            {

                throw;
            }
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<CommonStatusDTO>()
            {
                ItemList = statuss,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }

    }
}

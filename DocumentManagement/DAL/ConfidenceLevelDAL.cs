using DocumentManagement.Common;
using DocumentManagement.Model;
using DocumentManagement.Models.Entity.ConfidenceLevel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ConfidenceLevelManagement.DAL
{
    public class ConfidenceLevelDAL
    {
        public ReturnResult<ConfidenceLevel> GetAllConfidenceLevel()
        {
            List<ConfidenceLevel> documentList = new List<ConfidenceLevel>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("CONFIDENCE_LEVEL_GET_ALL", CommandType.StoredProcedure)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 255, ParameterDirection.Output)
                .GetList<ConfidenceLevel>(out documentList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<ConfidenceLevel>()
            {
                ItemList = documentList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
    }
}

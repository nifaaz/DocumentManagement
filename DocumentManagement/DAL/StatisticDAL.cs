using DocumentManagement.Common;
using DocumentManagement.Model;
using DocumentManagement.Models.Entity.Statistic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.DAL
{
    public class StatisticDAL
    {
        public ReturnResult<Statistic> GetStatisticByNumberOfFiles()
        {
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            List<Statistic> resultList = new List<Statistic>();
            dbProvider.SetQuery("STATISTIC_GET_NUMBER_OF_FILES", CommandType.StoredProcedure)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<Statistic>(out resultList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Statistic>()
            {
                ItemList = resultList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
            };
        }
    }
}

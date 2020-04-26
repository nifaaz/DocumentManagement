using Common.Common;
using DocumentManagement.Common;
using DocumentManagement.Model;
using DocumentManagement.Model.Entity;
using DocumentManagement.Models.DTO;
using DocumentManagement.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.DAL
{
    public class ExportDAL
    {
        private ExportDAL() { }

        private static volatile ExportDAL _instance;

        static object key = new object();

        public static ExportDAL GetExportDALInstance
        {
            get
            {
                lock (key)
                {
                    if (_instance == null)
                    {
                        _instance = new ExportDAL();
                    }
                }

                return _instance;
            }

            private set
            {
                _instance = value;
            }
        }
        public async Task<ReturnResult<Export>> GetPagingWithSearchResults(BaseCondition<Export> condition)
        {
            DbProvider provider = new DbProvider();
            List<Export> list = new List<Export>();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            string totalRecords = String.Empty;
            var result = new ReturnResult<Export>();
            try
            {
                provider.SetQuery("EXPORT_GET_PAGING", System.Data.CommandType.StoredProcedure)
                    .SetParameter("InWhere", System.Data.SqlDbType.NVarChar, condition.IN_WHERE ?? String.Empty)
                    .SetParameter("InSort", System.Data.SqlDbType.NVarChar, condition.IN_SORT ?? String.Empty)
                    .SetParameter("StartRow", System.Data.SqlDbType.Int, condition.PageIndex)
                    .SetParameter("PageSize", System.Data.SqlDbType.Int, condition.PageSize)
                    .SetParameter("TotalRecords", System.Data.SqlDbType.Int, DBNull.Value, System.Data.ParameterDirection.Output)
                    .SetParameter("ErrorCode", System.Data.SqlDbType.NVarChar, DBNull.Value, 100, System.Data.ParameterDirection.Output)
                    .SetParameter("ErrorMessage", System.Data.SqlDbType.NVarChar, DBNull.Value, 4000, System.Data.ParameterDirection.Output).GetList<Export>(out list).Complete();

                if (list.Count > 0)
                {
                    result.ItemList = list;
                }
                provider.GetOutValue("ErrorCode", out outCode)
                           .GetOutValue("ErrorMessage", out outMessage)
                           .GetOutValue("TotalRecords", out string totalRows);

                if (outCode != "0")
                {
                    result.ErrorCode = outCode;
                    result.ErrorMessage = outMessage;
                }
                else
                {
                    result.ErrorCode = "";
                    result.ErrorMessage = "";
                    result.TotalRows = int.Parse(totalRows);
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }
            return result;
        }


        public async Task<ReturnResult<DataStatisticsDTO>> GetDataStatisticsPagingWithSearchResults(BaseCondition<FilterDTO> condition)
        {
            DbProvider provider = new DbProvider();
            List<DataStatisticsDTO> dataStatisticsDTOs = new List<DataStatisticsDTO>();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            string totalRecords = String.Empty;
            var result = new ReturnResult<DataStatisticsDTO>();
            try
            {
                provider.SetQuery("GET_DATA_STATISTICS", System.Data.CommandType.StoredProcedure)
                    .SetParameter("InWhere", System.Data.SqlDbType.NVarChar, condition.IN_WHERE ?? String.Empty)
                    .SetParameter("InSort", System.Data.SqlDbType.NVarChar, condition.IN_SORT ?? String.Empty)
                    .SetParameter("StartRow", System.Data.SqlDbType.Int, condition.PageIndex)
                    .SetParameter("PageSize", System.Data.SqlDbType.Int, condition.PageSize)
                    .SetParameter("TotalRecords", System.Data.SqlDbType.Int, DBNull.Value, System.Data.ParameterDirection.Output)
                    .SetParameter("ErrorCode", System.Data.SqlDbType.NVarChar, DBNull.Value, 100, System.Data.ParameterDirection.Output)
                    .SetParameter("ErrorMessage", System.Data.SqlDbType.NVarChar, DBNull.Value, 4000, System.Data.ParameterDirection.Output).GetList<DataStatisticsDTO>(out dataStatisticsDTOs).Complete();

                if (dataStatisticsDTOs.Count > 0)
                {
                    result.ItemList = dataStatisticsDTOs;
                }
                provider.GetOutValue("ErrorCode", out outCode)
                           .GetOutValue("ErrorMessage", out outMessage)
                           .GetOutValue("TotalRecords", out string totalRows);

                if (outCode != "0")
                {
                    result.ErrorCode = outCode;
                    result.ErrorMessage = outMessage;
                }
                else
                {
                    result.ErrorCode = "";
                    result.ErrorMessage = "";
                    result.TotalRows = int.Parse(totalRows);
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public async Task<ReturnResult<DataStatisticsDTO>> GetDataStatisticss()
        {
            List<DataStatisticsDTO> dataStatisticsDTOs = new List<DataStatisticsDTO>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("DATA_STATISTICS", CommandType.StoredProcedure)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<DataStatisticsDTO>(out dataStatisticsDTOs)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<DataStatisticsDTO>()
            {
                ItemList = dataStatisticsDTOs,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
    }
}
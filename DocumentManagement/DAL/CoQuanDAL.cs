﻿using Common.Common;
using DocumentManagement.Common;
using DocumentManagement.Model;
using DocumentManagement.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.DAL
{
    public class CoQuanDAL
    {
        private CoQuanDAL()
        {

        }
        private static CoQuanDAL _instance;
        public static CoQuanDAL GetCoQuanDALInstance()
        {
            if (_instance == null)
            {
                _instance = new CoQuanDAL();
            }
            return _instance;
        }

        /// <summary>
        /// lấy danh sách cơ quan theo điều kiện tìm kiếm + phân trang
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ReturnResult<CoQuan> GetCoQuanWithPaging(BaseCondition<CoQuan> condition)
        {
            DbProvider provider = new DbProvider();
            List<CoQuan> list = new List<CoQuan>();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            string totalRecords = String.Empty;
            var result = new ReturnResult<CoQuan>();
            try
            {
                provider.SetQuery("COQUAN_GET_SEARCH_WITH_PAGING", System.Data.CommandType.StoredProcedure)
                    .SetParameter("InWhere", System.Data.SqlDbType.NVarChar, condition.IN_WHERE)
                    .SetParameter("InSort", System.Data.SqlDbType.NVarChar, condition.IN_SORT)
                    .SetParameter("StartRow", System.Data.SqlDbType.Int, condition.PageIndex)
                    .SetParameter("PageSize", System.Data.SqlDbType.Int, condition.PageSize)
                    .SetParameter("TotalRecords", System.Data.SqlDbType.Int, DBNull.Value, System.Data.ParameterDirection.Output)
                    .SetParameter("ErrorCode", System.Data.SqlDbType.NVarChar, DBNull.Value, 100, System.Data.ParameterDirection.Output)
                    .SetParameter("ErrorMessage", System.Data.SqlDbType.NVarChar, DBNull.Value, 4000, System.Data.ParameterDirection.Output).GetList<CoQuan>(out list).Complete();

                if (list.Count > 0)
                {
                    result.ItemList = list;
                }
                provider.GetOutValue("ErrorCode", out outCode)
                           .GetOutValue("ErrorMessage", out outMessage)
                            .GetOutValue("TotalRecords", out totalRecords);

                if (outCode != "0" || outCode == "")
                {
                    result.ErrorCode = outCode;
                    result.ErrorMessage = outMessage;
                }
                else
                {
                    result.ErrorCode = "";
                    result.ErrorMessage = "";
                    result.TotalRows = int.Parse(totalRecords);
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ReturnResult<CoQuan> GetCoQuanById(int id)
        {
            DbProvider provider = new DbProvider();
            var result = new ReturnResult<CoQuan>();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            string totalRecords = String.Empty;
            CoQuan item = new CoQuan();
            try
            {
                provider.SetQuery("COQUAN_GET_BY_ID", System.Data.CommandType.StoredProcedure)
                    .SetParameter("CoQuanID", System.Data.SqlDbType.Int, id, System.Data.ParameterDirection.Input)
                    .SetParameter("ErrorCode", System.Data.SqlDbType.NVarChar, DBNull.Value, 100, System.Data.ParameterDirection.Output)
                    .SetParameter("ErrorMessage", System.Data.SqlDbType.NVarChar, DBNull.Value, 4000, System.Data.ParameterDirection.Output)
                    .GetSingle<CoQuan>(out item).Complete();

                provider.GetOutValue("ErrorCode", out outCode)
                          .GetOutValue("ErrorMessage", out outMessage)
                           .GetOutValue("TotalRecords", out totalRecords);

                if (outCode != "0" || outCode == "")
                {
                    result.ErrorCode = outCode;
                    result.ErrorMessage = outMessage;
                }
                else
                {
                    result.Item = item;
                    result.ErrorCode = outCode;
                    result.ErrorMessage = outMessage;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        public ReturnResult<CoQuan> InsertCoQuan(CoQuan coQuan)
        {
            DbProvider provider = new DbProvider();
            var result = new ReturnResult<CoQuan>();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            string totalRecords = String.Empty;
            try
            {
                provider.SetQuery("[Organ_INSERT]", System.Data.CommandType.StoredProcedure)
                    .SetParameter("TenCoQuan", System.Data.SqlDbType.NVarChar, coQuan.TenCoQuan)
                    .SetParameter("TinhID", System.Data.SqlDbType.Int, coQuan.TinhID, System.Data.ParameterDirection.Input)
                    .SetParameter("HuyenID", System.Data.SqlDbType.Int, coQuan.HuyenID, System.Data.ParameterDirection.Input)
                    .SetParameter("XaPhuongID", System.Data.SqlDbType.Int, coQuan.XaPhuongID, System.Data.ParameterDirection.Input)
                    .SetParameter("LoaiCoQuan", System.Data.SqlDbType.Int, coQuan.LoaiCoQuanID, System.Data.ParameterDirection.Input)
                    .SetParameter("ErrorCode", System.Data.SqlDbType.NVarChar, DBNull.Value, 100, System.Data.ParameterDirection.Output)
                    .SetParameter("ErrorMessage", System.Data.SqlDbType.NVarChar, DBNull.Value, 4000, System.Data.ParameterDirection.Output)
                    .GetSingle<CoQuan>(out coQuan).Complete();

                provider.GetOutValue("ErrorCode", out outCode)
                          .GetOutValue("ErrorMessage", out outMessage)
                           .GetOutValue("TotalRecords", out totalRecords);

                if (outCode != "0" || outCode == "")
                {
                    result.ErrorCode = outCode;
                    result.ErrorMessage = outMessage;
                }
                else
                {
                    result.Item = coQuan;
                    result.ErrorCode = outCode;
                    result.ErrorMessage = outMessage;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
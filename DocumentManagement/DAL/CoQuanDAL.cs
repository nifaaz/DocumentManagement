using Common.Common;
using DocumentManagement.Common;
using DocumentManagement.Model;
using DocumentManagement.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using DocumentManagement.Models.Entity.Organ;
using System.Collections;
using DocumentManagement.Model.Entity;

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
                    .SetParameter("InWhere", System.Data.SqlDbType.NVarChar, condition.IN_WHERE ?? String.Empty)
                    .SetParameter("InSort", System.Data.SqlDbType.NVarChar, condition.IN_SORT ?? String.Empty)
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
                provider.SetQuery("COQUAN_GET_BY_ID", CommandType.StoredProcedure)
                    .SetParameter("CoQuanID", SqlDbType.Int, id, ParameterDirection.Input)
                    .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                    .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                    .GetSingle<CoQuan>(out item).Complete();

                provider.GetOutValue("ErrorCode", out outCode)
                          .GetOutValue("ErrorMessage", out outMessage);

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
                result.Failed("-1", ex.Message);
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
                provider.SetQuery("[Organ_INSERT]", CommandType.StoredProcedure)
                    .SetParameter("OrganCode", SqlDbType.NVarChar, coQuan.OrganCode)
                    .SetParameter("TenCoQuan", SqlDbType.NVarChar, coQuan.TenCoQuan)
                    .SetParameter("TinhID", SqlDbType.Int, coQuan.TinhID, ParameterDirection.Input)
                    .SetParameter("HuyenID", SqlDbType.Int, coQuan.HuyenID, ParameterDirection.Input)
                    .SetParameter("XaPhuongID", SqlDbType.Int, coQuan.XaPhuongID, ParameterDirection.Input)
                    .SetParameter("LoaiCoQuan", SqlDbType.Int, coQuan.LoaiCoQuanID, ParameterDirection.Input)
                    .SetParameter("CreateBy", SqlDbType.VarChar, coQuan.CreateBy, 50)
                    .SetParameter("Description", SqlDbType.NVarChar, coQuan.Description, 1000)
                    .SetParameter("Notes", SqlDbType.NVarChar, coQuan.Notes, 1000)
                    .SetParameter("AddressDetail", SqlDbType.NVarChar, coQuan.AddressDetail, 500)
                    .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                    .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                    .ExcuteNonQuery()
                    .Complete();

                provider.GetOutValue("ErrorCode", out outCode)
                          .GetOutValue("ErrorMessage", out outMessage);

                if (outCode != "0" || outCode == "")
                {
                    result.Failed(outCode, outMessage);

                }
                else
                {
                    result.Item = coQuan;
                    result.ErrorCode = "0";
                    result.ErrorMessage = "";
                }
            }
            catch (Exception ex)
            {
                result.Failed("-1", ex.Message);
            }
            return result;
        }

        /// <summary>
        /// cập nhật cơ quan
        /// </summary>
        /// <param name="coQuan"></param>
        /// <returns></returns>
        public ReturnResult<CoQuan> UpdateCoQuan(CoQuan coQuan)
        {
            ReturnResult<CoQuan> result = new ReturnResult<CoQuan>(); ;
            DbProvider db;
            try
            {
                db = new DbProvider();
                db.SetQuery("Organ_EDIT", CommandType.StoredProcedure);
                db.SetParameter("CoQuanID", SqlDbType.Int, coQuan.CoQuanID);
                db.SetParameter("OrganCode", SqlDbType.NVarChar, coQuan.OrganCode);
                db.SetParameter("DiaChiID", SqlDbType.Int, coQuan.DiaChiID);
                db.SetParameter("TenCoQuan", SqlDbType.NVarChar, coQuan.TenCoQuan, 500);
                db.SetParameter("LoaiCoQuan", SqlDbType.Int, coQuan.LoaiCoQuanID);
                db.SetParameter("AddressDetail", SqlDbType.NVarChar, coQuan.AddressDetail, 500);
                db.SetParameter("TinhID", SqlDbType.Int, coQuan.TinhID);
                db.SetParameter("HuyenID", SqlDbType.Int, coQuan.HuyenID);
                db.SetParameter("XaPhuongID", SqlDbType.Int, coQuan.XaPhuongID);
                db.SetParameter("UpdatedBy", SqlDbType.NVarChar, coQuan.UpdatedBy, 50);
                db.SetParameter("Description", SqlDbType.NVarChar, coQuan.Description, 1000);
                db.SetParameter("Notes", SqlDbType.NVarChar, coQuan.Notes, 1000);
                db.SetParameter("ErrorCode", SqlDbType.Int, DBNull.Value, ParameterDirection.Output);
                db.SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output);
                db.ExcuteNonQuery();
                db.Complete();
                db.GetOutValue("ErrorCode", out string errorCode);
                db.GetOutValue("ErrorMessage", out string errorMessage);
                if (errorCode.ToString() == "0")
                {
                    result.ErrorCode = "0";
                    result.ErrorMessage = "";
                }
                else
                {
                    result.Failed(errorCode, errorMessage);
                }
            }
            catch (Exception ex)
            {
                result.Failed("-1", ex.Message);
            }
            return result;
        }

        public ReturnResult<CoQuan> DeleteCoQuan (int id)
        {
            DbProvider provider = new DbProvider();
            var result = new ReturnResult<CoQuan>();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            string totalRecords = String.Empty;
            CoQuan item = new CoQuan();
            try
            {
                provider.SetQuery("COQUAN_DELETE", CommandType.StoredProcedure)
                    .SetParameter("CoQuanID", SqlDbType.Int, id, System.Data.ParameterDirection.Input)
                    .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, System.Data.ParameterDirection.Output)
                    .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, System.Data.ParameterDirection.Output)
                    .ExcuteNonQuery().Complete();

                provider.GetOutValue("ErrorCode", out outCode)
                          .GetOutValue("ErrorMessage", out outMessage);

                if (outCode != "0")
                {
                    result.Failed(outCode, outMessage);
                }
                else
                {
                    result.ErrorCode = "0";
                    result.ErrorMessage = "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public ReturnResult<CoQuan> GetAllCoQuan ()
        {
            var result = new ReturnResult<CoQuan>();
            DbProvider db;
            List<CoQuan> lst;
            try
            {
                result = new ReturnResult<CoQuan>();
                db = new DbProvider();
                lst = new List<CoQuan>();
                db.SetQuery("Organ_GET_ALL", CommandType.StoredProcedure)
                    .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 400, ParameterDirection.Output)
                    .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                    .GetList<CoQuan>(out lst)
                    .Complete();
                db.GetOutValue("ErrorCode", out string outCode)
                    .GetOutValue("ErrorMessage", out string outMessage);
                // get out value
                if (outCode.ToString() != "0")
                {
                    result.Failed(outCode, outMessage);
                    return result;
                }
                else
                {
                    result.ItemList = lst;
                    result.ErrorCode = "0";
                    result.ErrorMessage = "";
                }
            }
            catch (Exception ex)
            {
                result.Failed("-1", ex.Message);
            }
            return result;
        }

        public ReturnResult<Font> GetFontsByOrganId(BaseCondition<CoQuan> condition)
        {
            ReturnResult<Font> result = new ReturnResult<Font>();
            DbProvider db;
            List<Font> lstResult;
            try
            {
                db = new DbProvider();
                lstResult = new List<Font>();
                db.SetQuery("ORGAN_GET_ALL_FOND_WITH_PAGING", CommandType.StoredProcedure);
                db.SetParameter("OrganId", SqlDbType.Int, condition.Item.CoQuanID);
                db.SetParameter("StartRow", SqlDbType.Int, condition.PageIndex);
                db.SetParameter("PageSize", SqlDbType.Int, condition.PageSize);
                db.SetParameter("InWhere", SqlDbType.NVarChar, condition.IN_WHERE == null ? "" : condition.IN_WHERE, 500);
                db.SetParameter("InSort", SqlDbType.NVarChar, condition.IN_SORT == null ? "" : condition.IN_SORT, 200);
                db.SetParameter("TotalRecords", SqlDbType.Int, DBNull.Value, ParameterDirection.Output);
                db.SetParameter("ErrorCode", SqlDbType.Int, DBNull.Value, ParameterDirection.Output);
                db.SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 2000, ParameterDirection.Output);
                db.GetList<Font>(out lstResult);
                db.Complete();
                db.GetOutValue("ErrorCode", out int errorCode);
                db.GetOutValue("ErrorMessage", out string errorMessage);
                    db.GetOutValue("TotalRecords", out int totalRecords);

                if (errorCode.ToString() != "0")
                {
                    result.Failed(errorCode.ToString(), errorMessage);
                }
                else
                {
                    result.ItemList = lstResult;
                    result.TotalRows = totalRecords;
                    result.ErrorCode = "0";
                    result.ErrorMessage = "";
                }
            }
            catch (Exception ex)
            {
                result.Failed("-1", ex.Message);
            }
            return result;
        }

    }

}

using Common.Common;
using DocumentManagement.Common;
using DocumentManagement.Model;
using DocumentManagement.Model.Entity.OrganType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.DAL
{
    public class OrganTypeDAL
    {
        private OrganTypeDAL() { }

        private static volatile OrganTypeDAL _instance;

        static object key = new object();

        public static OrganTypeDAL GetOrganTypeDALInstance
        {
            get
            {
                lock (key)
                {
                    if (_instance == null)
                    {
                        _instance = new OrganTypeDAL();
                    }
                }

                return _instance;
            }

            private set
            {
                _instance = value;
            }
        }
        public ReturnResult<OrganType> GetPagingWithSearchResults(BaseCondition<OrganType> condition)
        {
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            dbProvider.SetQuery("OrganType_GET_PAGING", CommandType.StoredProcedure)
                .SetParameter("FromRecord", SqlDbType.NVarChar, condition.FromRecord, ParameterDirection.Input)
                .SetParameter("PageSize", SqlDbType.NVarChar, condition.PageSize, ParameterDirection.Input)
                .SetParameter("InWhere", SqlDbType.NVarChar, condition.IN_WHERE, ParameterDirection.Input)
                .SetParameter("InSort", SqlDbType.NVarChar, condition.IN_SORT, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .ExcuteNonQuery()
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<OrganType>()
            {
                ErrorCode = outCode,
                ErrorMessage = outMessage,
            };
        }

        public ReturnResult<OrganType> GetAllOrganType()
        {
            List<OrganType> OrganTypeList = new List<OrganType>();
            DbProvider dbProvider = new DbProvider();
            var result = new ReturnResult<OrganType>();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            // int totalRows = 0;
            try
            {
                dbProvider.SetQuery("OrganType_GET_ALL", CommandType.StoredProcedure)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<OrganType>(out OrganTypeList)
                .Complete();
                dbProvider.GetOutValue("ErrorCode", out outCode)
                           .GetOutValue("ErrorMessage", out outMessage);
                if (outCode.ToString() == "0")
                {
                    result.ItemList = OrganTypeList;
                    result.ErrorCode = outCode;
                    result.ErrorMessage = "";
                    result.ErrorCode = "0";
                }
                else
                {
                    result.ItemList = null;
                    result.Failed(outCode, outMessage);
                }
            }
            catch (Exception ex)
            {
                result.Failed("-1", ex.Message);
            }
            return result;
        }
        public ReturnResult<OrganType> OrganTypeSearch(string searchStr)
        {
            List<OrganType> OrganTypeList = new List<OrganType>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("OrganType_SEARCH", CommandType.StoredProcedure)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<OrganType>(out OrganTypeList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<OrganType>()
            {
                ItemList = OrganTypeList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<OrganType> GetOrganTypeByID(int OrganTypeID)
        {
            List<OrganType> OrganTypeList = new List<OrganType>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("OrganType_GET_BY_ID", CommandType.StoredProcedure)
                .SetParameter("CoQuanID", SqlDbType.Int, OrganTypeID, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<OrganType>(out OrganTypeList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<OrganType>()
            {
                ItemList = OrganTypeList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<OrganType> GetOrganTypeByAddressID(int addressID)
        {
            List<OrganType> OrganTypeList = new List<OrganType>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("OrganType_GET_BY_ADDRESSID", CommandType.StoredProcedure)
                .SetParameter("DiaChiID", SqlDbType.Int, addressID, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<OrganType>(out OrganTypeList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<OrganType>()
            {
                ItemList = OrganTypeList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<OrganType> GetOrganTypeByOrganTypeTypeID(int OrganTypeTypeID)
        {
            List<OrganType> OrganTypeList = new List<OrganType>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("OrganType_GET_BY_OrganTypeTYPEID", CommandType.StoredProcedure)
                .SetParameter("LoaiCoQuan", SqlDbType.Int, OrganTypeTypeID, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<OrganType>(out OrganTypeList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<OrganType>()
            {
                ItemList = OrganTypeList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<OrganType> DeleteOrganType(int OrganTypeID)
        {
            List<OrganType> OrganTypeList = new List<OrganType>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("OrganType_DELETE", CommandType.StoredProcedure)
                .SetParameter("CoQuanID", SqlDbType.Int, OrganTypeID, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<OrganType>(out OrganTypeList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<OrganType>()
            {
                ItemList = OrganTypeList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        //public ReturnResult<OrganType> UpdateOrganType(OrganType OrganType)
        //{
        //    List<OrganType> OrganTypeList = new List<OrganType>();
        //    DbProvider dbProvider = new DbProvider();
        //    string outCode = String.Empty;
        //    string outMessage = String.Empty;
        //    int totalRows = 0;
        //    dbProvider.SetQuery("OrganType_EDIT", CommandType.StoredProcedure)
        //        .SetParameter("CoQuanID", SqlDbType.Int, OrganType.OrganTypeID, ParameterDirection.Input)
        //        .SetParameter("TenCoQuan", SqlDbType.NVarChar, OrganType.OrganTypeName, 50, ParameterDirection.Input)
        //        .SetParameter("DiaChiID", SqlDbType.Int, OrganType.AddressID, ParameterDirection.Input)
        //        .SetParameter("OrganTypeType", SqlDbType.Int, OrganType.OrganTypeTypeID, ParameterDirection.Input)
        //        .SetParameter("NgayCapNhat", SqlDbType.Int, OrganType.UpdateTime, ParameterDirection.Input)
        //        .SetParameter("Status", SqlDbType.Int, OrganType.Status, ParameterDirection.Input)
        //        .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
        //        .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
        //        .GetList<OrganType>(out OrganTypeList)
        //        .Complete();
        //    dbProvider.GetOutValue("ErrorCode", out outCode)
        //               .GetOutValue("ErrorMessage", out outMessage);

        //    return new ReturnResult<OrganType>()
        //    {
        //        ItemList = OrganTypeList,
        //        ErrorCode = outCode,
        //        ErrorMessage = outMessage,
        //        TotalRows = totalRows
        //    };
        //}
        //public ReturnResult<OrganType> InsertOrganType(OrganType OrganType)
        //{
        //    List<OrganType> OrganTypeList = new List<OrganType>();
        //    DbProvider dbProvider = new DbProvider();
        //    string outCode = String.Empty;
        //    string outMessage = String.Empty;
        //    int totalRows = 0;
        //    dbProvider.SetQuery("OrganType_INSERT", CommandType.StoredProcedure)
        //        .SetParameter("TenCoQuan", SqlDbType.NVarChar, OrganType.OrganTypeName, 50, ParameterDirection.Input)
        //        .SetParameter("DiaChiID", SqlDbType.Int, OrganType.AddressID, ParameterDirection.Input)
        //        .SetParameter("OrganTypeType", SqlDbType.Int, OrganType.OrganTypeTypeID, ParameterDirection.Input)
        //        .SetParameter("NgayCapNhat", SqlDbType.Int, OrganType.UpdateTime, ParameterDirection.Input)
        //        .SetParameter("Status", SqlDbType.Int, OrganType.Status, ParameterDirection.Input)
        //        .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
        //        .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
        //        .GetList<OrganType>(out OrganTypeList)
        //        .Complete();
        //    dbProvider.GetOutValue("ErrorCode", out outCode)
        //               .GetOutValue("ErrorMessage", out outMessage);

        //    return new ReturnResult<OrganType>()
        //    {
        //        ItemList = OrganTypeList,
        //        ErrorCode = outCode,
        //        ErrorMessage = outMessage,
        //        TotalRows = totalRows
        //    };
        //}
    }
}

using Common.Common;
using DocumentManagement.Common;
using DocumentManagement.Model;
using DocumentManagement.Model.Entity.Address;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.DAL
{
    public class AddressDAL
    {
        private AddressDAL() { }

        private static volatile AddressDAL _instance;

        static object key = new object();

        public static AddressDAL GetAddressDALInstance
        {
            get
            {
                lock (key)
                {
                    if (_instance == null)
                    {
                        _instance = new AddressDAL();
                    }
                }

                return _instance;
            }

            private set
            {
                _instance = value;
            }
        }
        public ReturnResult<Address> GetPagingWithSearchResults(BaseCondition<Address> condition)
        {
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            dbProvider.SetQuery("Address_GET_PAGING", CommandType.StoredProcedure)
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

            return new ReturnResult<Address>()
            {
                ErrorCode = outCode,
                ErrorMessage = outMessage,
            };
        }
        public ReturnResult<Address> GetAllAddress()
        {
            List<Address> AddressList = new List<Address>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("Address_GET_ALL", CommandType.StoredProcedure)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<Address>(out AddressList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Address>()
            {
                ItemList = AddressList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<Address> AddressExport()
        {
            List<Address> AddressList = new List<Address>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("Address_EXPORT", CommandType.StoredProcedure)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<Address>(out AddressList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Address>()
            {
                ItemList = AddressList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<Address> AddressSearch(string searchStr)
        {
            List<Address> AddressList = new List<Address>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("Address_GET_ALL", CommandType.StoredProcedure)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<Address>(out AddressList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Address>()
            {
                ItemList = AddressList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<Address> GetAddressByID(int PhongID)
        {
            List<Address> AddressList = new List<Address>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("Address_GET_BY_ID", CommandType.StoredProcedure)
                .SetParameter("PhongID", SqlDbType.Int, PhongID, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<Address>(out AddressList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Address>()
            {
                ItemList = AddressList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<Provincial> GetAllTinh()
        {
            List<Provincial> ProvincialList = new List<Provincial>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("Address_ALL_TINH", CommandType.StoredProcedure)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<Provincial>(out ProvincialList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Provincial>()
            {
                ItemList = ProvincialList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<District> GetDistrictByProvinceID(int tinhID)
        {
            List<District> DistrictList = new List<District>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("Address_GET_HUYEN_BY_TINH_ID", CommandType.StoredProcedure)
                .SetParameter("TinhID", SqlDbType.Int, tinhID, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<District>(out DistrictList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<District>()
            {
                ItemList = DistrictList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<Wards> GetWardByDistrictID(int huyenID)
        {
            List<Wards> WardsList = new List<Wards>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("Address_GET_XAPHUONG_BY_HUYEN_ID", CommandType.StoredProcedure)
                .SetParameter("HuyenID", SqlDbType.Int, huyenID, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<Wards>(out WardsList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Wards>()
            {
                ItemList = WardsList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<Address> DeleteAddress(int PhongID)
        {
            List<Address> AddressList = new List<Address>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("Address_DELETE", CommandType.StoredProcedure)
                .SetParameter("PhongID", SqlDbType.Int, PhongID, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<Address>(out AddressList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Address>()
            {
                ItemList = AddressList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }

        public ReturnResult<Wards> GetAllWardsByProvinceId (int provinceId)
        {
            ReturnResult<Wards> result = new ReturnResult<Wards>();
            DbProvider db;
            List<Wards> lstWards;
            try
            {
                db = new DbProvider();
                lstWards = new List<Wards>();
                db.SetQuery("ADDRESS_GET_ALL_WARD_BY_PROVINCEID", CommandType.StoredProcedure)
                    .SetParameter("TinhID", SqlDbType.Int, provinceId)
                    .SetParameter("ErrorCode", SqlDbType.VarChar, DBNull.Value, 50, ParameterDirection.Output)
                    .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                    .GetList<Wards>(out lstWards).Complete();

                // get output value
                db.GetOutValue("ErrorCode", out string outCode)
                    .GetOutValue("ErrorMessage", out string outMessage);

                if (outCode.ToString() != "0")
                {
                    result.Failed(outCode, outMessage);
                }
                else
                {
                    result.ItemList = lstWards;
                    result.ErrorCode = "0";
                    result.ErrorMessage = "";
                }
            }
            catch (Exception ex)
            {
                result.Failed("-1", ex.Message);
                throw ex;
            }
            return result;
        }

        //public ReturnResult<Address> UpdateAddress(Address Address)
        //{
        //    List<Address> AddressList = new List<Address>();
        //    DbProvider dbProvider = new DbProvider();
        //    string outCode = String.Empty;
        //    string outMessage = String.Empty;
        //    int totalRows = 0;
        //    dbProvider.SetQuery("Address_EDIT", CommandType.StoredProcedure)
        //        .SetParameter("PhongID", SqlDbType.Int, Address.AddressID, ParameterDirection.Input)
        //        .SetParameter("PhongSo", SqlDbType.NChar, Address.AddressNumber, 10, ParameterDirection.Input)
        //        .SetParameter("CoQuanID", SqlDbType.Int, Address.OrganID, ParameterDirection.Input)
        //        .SetParameter("TenPhong", SqlDbType.NVarChar, Address.AddressName, 50, ParameterDirection.Input)
        //        .SetParameter("LichSu", SqlDbType.NVarChar, Address.History, 500, ParameterDirection.Input)
        //        .SetParameter("NgonNgu", SqlDbType.NVarChar, Address.Lang, 50, ParameterDirection.Input)
        //        .SetParameter("NgayCapNhat", SqlDbType.DateTime, Address.UpdateTime, ParameterDirection.Input)
        //        .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
        //        .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
        //        .GetList<Address>(out AddressList)
        //        .Complete();
        //    dbProvider.GetOutValue("ErrorCode", out outCode)
        //               .GetOutValue("ErrorMessage", out outMessage);

        //    return new ReturnResult<Address>()
        //    {
        //        ItemList = AddressList,
        //        ErrorCode = outCode,
        //        ErrorMessage = outMessage,
        //        TotalRows = totalRows
        //    };
        //}
        //public ReturnResult<Address> InsertAddress(Address Address)
        //{
        //    List<Address> AddressList = new List<Address>();
        //    DbProvider dbProvider = new DbProvider();
        //    string outCode = String.Empty;
        //    string outMessage = String.Empty;
        //    int totalRows = 0;
        //    dbProvider.SetQuery("Address_CREATE", CommandType.StoredProcedure)
        //        .SetParameter("PhongSo", SqlDbType.NChar, Address.AddressNumber, 10, ParameterDirection.Input)
        //        .SetParameter("CoQuanID", SqlDbType.Int, Address.OrganID, ParameterDirection.Input)
        //        .SetParameter("TenPhong", SqlDbType.NVarChar, Address.AddressName, 50, ParameterDirection.Input)
        //        .SetParameter("LichSu", SqlDbType.NVarChar, Address.History, 500, ParameterDirection.Input)
        //        .SetParameter("NgonNgu", SqlDbType.NVarChar, Address.Lang, 50, ParameterDirection.Input)
        //        .SetParameter("NgayCapNhat", SqlDbType.DateTime, Address.UpdateTime, ParameterDirection.Input)
        //        .SetParameter("NgayTao", SqlDbType.DateTime, Address.CreateTime, ParameterDirection.Input)
        //        .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
        //        .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
        //        .GetList<Address>(out AddressList)
        //        .Complete();
        //    dbProvider.GetOutValue("ErrorCode", out outCode)
        //               .GetOutValue("ErrorMessage", out outMessage);

        //    return new ReturnResult<Address>()
        //    {
        //        ItemList = AddressList,
        //        ErrorCode = outCode,
        //        ErrorMessage = outMessage,
        //        TotalRows = totalRows
        //    };
        //}
    }
}

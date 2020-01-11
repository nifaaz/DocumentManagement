using DocumentManagement.Common;
using DocumentManagement.Model;
using DocumentManagement.Model.Entity.Organ;
using DocumentManagement.Model.Entity.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.DAL
{
    public class OrganDAL
    {
        public ReturnResult<Organ> GetAllOrgan()
        {
            List<Organ> OrganList = new List<Organ>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("Organ_GET_ALL", CommandType.StoredProcedure)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<Organ>(out OrganList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Organ>()
            {
                ItemList = OrganList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<Organ> OrganSearch(string searchStr)
        {
            List<Organ> OrganList = new List<Organ>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("Organ_SEARCH", CommandType.StoredProcedure)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<Organ>(out OrganList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Organ>()
            {
                ItemList = OrganList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<Organ> GetOrganByID(int organID)
        {
            List<Organ> OrganList = new List<Organ>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("Organ_GET_BY_ID", CommandType.StoredProcedure)
                .SetParameter("CoQuanID", SqlDbType.Int, organID, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<Organ>(out OrganList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Organ>()
            {
                ItemList = OrganList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<Organ> GetOrganByAddressID(int addressID)
        {
            List<Organ> OrganList = new List<Organ>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("Organ_GET_BY_ADDRESSID", CommandType.StoredProcedure)
                .SetParameter("DiaChiID", SqlDbType.Int, addressID, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<Organ>(out OrganList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Organ>()
            {
                ItemList = OrganList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<Organ> GetOrganByOrganTypeID(int organTypeID)
        {
            List<Organ> OrganList = new List<Organ>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("Organ_GET_BY_ORGANTYPEID", CommandType.StoredProcedure)
                .SetParameter("LoaiCoQuan", SqlDbType.Int, organTypeID, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<Organ>(out OrganList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Organ>()
            {
                ItemList = OrganList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<Organ> DeleteOrgan(int organID)
        {
            List<Organ> OrganList = new List<Organ>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("Organ_DELETE", CommandType.StoredProcedure)
                .SetParameter("CoQuanID", SqlDbType.Int, organID, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<Organ>(out OrganList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Organ>()
            {
                ItemList = OrganList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<Organ> UpdateOrgan(Organ organ)
        {
            List<Organ> organList = new List<Organ>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("Organ_EDIT", CommandType.StoredProcedure)
                .SetParameter("CoQuanID", SqlDbType.Int, organ.OrganID, ParameterDirection.Input)
                .SetParameter("TenCoQuan", SqlDbType.NVarChar, organ.OrganName, 50, ParameterDirection.Input)
                .SetParameter("DiaChiID", SqlDbType.Int, organ.AddressID, ParameterDirection.Input)
                .SetParameter("TinhID", SqlDbType.Int, organ.ProvincialID, ParameterDirection.Input)
                .SetParameter("HuyenID", SqlDbType.Int, organ.DistrictID, ParameterDirection.Input)
                .SetParameter("XaPhuongID", SqlDbType.Int, organ.WardsID, ParameterDirection.Input)
                .SetParameter("LoaiCoQuan", SqlDbType.Int, organ.OrganTypeID, ParameterDirection.Input)
                .SetParameter("NgayCapNhat", SqlDbType.Int, organ.UpdateTime, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<Organ>(out organList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Organ>()
            {
                ItemList = organList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<Organ> InsertOrgan(Organ organ)
        {
            List<Organ> organList = new List<Organ>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("Organ_INSERT", CommandType.StoredProcedure)
                .SetParameter("TenCoQuan", SqlDbType.NVarChar, organ.OrganName, 50, ParameterDirection.Input)
                .SetParameter("DiaChiID", SqlDbType.Int, organ.AddressID, ParameterDirection.Input)
                .SetParameter("TinhID", SqlDbType.Int, organ.ProvincialID, ParameterDirection.Input)
                .SetParameter("HuyenID", SqlDbType.Int, organ.DistrictID, ParameterDirection.Input)
                .SetParameter("XaPhuongID", SqlDbType.Int, organ.WardsID, ParameterDirection.Input)
                .SetParameter("LoaiCoQuan", SqlDbType.Int, organ.OrganTypeID, ParameterDirection.Input)
                .SetParameter("NgayCapNhat", SqlDbType.Int, organ.UpdateTime, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<Organ>(out organList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Organ>()
            {
                ItemList = organList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
    }
}

﻿using DocumentManagement.Common;
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
        public ReturnResult<Organ> UpdateOrgan(Organ Organ)
        {
            List<Organ> OrganList = new List<Organ>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("Organ_EDIT", CommandType.StoredProcedure)
                .SetParameter("CoQuanID", SqlDbType.Int, Organ.OrganID, ParameterDirection.Input)
                .SetParameter("TenCoQuan", SqlDbType.NVarChar, Organ.OrganName, 50, ParameterDirection.Input)
                .SetParameter("DiaChiID", SqlDbType.Int, Organ.AddressID, ParameterDirection.Input)
                .SetParameter("OrganType", SqlDbType.Int, Organ.OrganTypeID, ParameterDirection.Input)
                .SetParameter("NgayCapNhat", SqlDbType.Int, Organ.UpdateTime, ParameterDirection.Input)
                .SetParameter("Status", SqlDbType.Int, Organ.Status, ParameterDirection.Input)
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
        public ReturnResult<Organ> InsertOrgan(Organ Organ)
        {
            List<Organ> OrganList = new List<Organ>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("Organ_INSERT", CommandType.StoredProcedure)
                .SetParameter("TenCoQuan", SqlDbType.NVarChar, Organ.OrganName, 50, ParameterDirection.Input)
                .SetParameter("DiaChiID", SqlDbType.Int, Organ.AddressID, ParameterDirection.Input)
                .SetParameter("OrganType", SqlDbType.Int, Organ.OrganTypeID, ParameterDirection.Input)
                .SetParameter("NgayCapNhat", SqlDbType.Int, Organ.UpdateTime, ParameterDirection.Input)
                .SetParameter("Status", SqlDbType.Int, Organ.Status, ParameterDirection.Input)
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
    }
}
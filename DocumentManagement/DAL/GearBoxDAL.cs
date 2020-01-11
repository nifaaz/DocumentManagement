using DocumentManagement.Common;
using DocumentManagement.Model;
using DocumentManagement.Model.Entity.GearBox;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.DAL
{
    public class GearBoxDAL
    {
        public ReturnResult<GearBox> GearBoxExport()
        {
            List<GearBox> GearBoxList = new List<GearBox>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("GearBox_EXPORT", CommandType.StoredProcedure)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<GearBox>(out GearBoxList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<GearBox>()
            {
                ItemList = GearBoxList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        } 
        public ReturnResult<GearBox> GetAllGearBox()
        {
            List<GearBox> GearBoxList = new List<GearBox>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("GearBox_GET_ALL", CommandType.StoredProcedure)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<GearBox>(out GearBoxList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<GearBox>()
            {
                ItemList = GearBoxList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<GearBox> GearBoxSearch(string searchStr)
        {
            List<GearBox> GearBoxList = new List<GearBox>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("GearBox_SEARCH", CommandType.StoredProcedure)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<GearBox>(out GearBoxList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<GearBox>()
            {
                ItemList = GearBoxList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<GearBox> GetGearBoxByID(int gearBoxID)
        {
            List<GearBox> GearBoxList = new List<GearBox>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("GearBox_GET_BY_ID", CommandType.StoredProcedure)
                .SetParameter("HopSoID", SqlDbType.Int, gearBoxID, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<GearBox>(out GearBoxList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<GearBox>()
            {
                ItemList = GearBoxList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<GearBox> GetGearBoxByTableOfContentsID(int tabOfContID)
        {
            List<GearBox> GearBoxList = new List<GearBox>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("GearBox_GET_BY_TABLEOFCONTID", CommandType.StoredProcedure)
                .SetParameter("MucLucHoSoID", SqlDbType.Int, tabOfContID, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<GearBox>(out GearBoxList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<GearBox>()
            {
                ItemList = GearBoxList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<GearBox> DeleteGearBox(int gearBoxID)
        {
            List<GearBox> GearBoxList = new List<GearBox>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("GearBox_DELETE", CommandType.StoredProcedure)
                .SetParameter("HopSoID", SqlDbType.Int, gearBoxID, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<GearBox>(out GearBoxList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<GearBox>()
            {
                ItemList = GearBoxList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<GearBox> UpdateGearBox(GearBox GearBox)
        {
            List<GearBox> GearBoxList = new List<GearBox>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("GearBox_EDIT", CommandType.StoredProcedure)
                .SetParameter("HopSoID", SqlDbType.Int, GearBox.GearBoxID, ParameterDirection.Input)
                .SetParameter("MaHopSo", SqlDbType.NVarChar, GearBox.GearBoxName, 50, ParameterDirection.Input)
                .SetParameter("TieuDeHopSo", SqlDbType.NVarChar, GearBox.GearBoxTitle, 50, ParameterDirection.Input)
                .SetParameter("MucLucHoSoID", SqlDbType.Int, GearBox.TabOfContID, ParameterDirection.Input)
                .SetParameter("NgayBatDau", SqlDbType.DateTime, GearBox.StartDate, ParameterDirection.Input)
                .SetParameter("NgayKetThuc", SqlDbType.DateTime, GearBox.EndDate, ParameterDirection.Input)
                .SetParameter("NgayCapNhat", SqlDbType.DateTime, GearBox.UpdateTime, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<GearBox>(out GearBoxList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<GearBox>()
            {
                ItemList = GearBoxList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<GearBox> InsertGearBox(GearBox GearBox)
        {
            List<GearBox> GearBoxList = new List<GearBox>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("GearBox_INSERT", CommandType.StoredProcedure)
                .SetParameter("MaHopSo", SqlDbType.NVarChar, GearBox.GearBoxName, 50, ParameterDirection.Input)
                .SetParameter("TieuDeHopSo", SqlDbType.NVarChar, GearBox.GearBoxTitle, 50, ParameterDirection.Input)
                .SetParameter("MucLucHoSoID", SqlDbType.Int, GearBox.TabOfContID, ParameterDirection.Input)
                .SetParameter("NgayBatDau", SqlDbType.DateTime, GearBox.StartDate, ParameterDirection.Input)
                .SetParameter("NgayKetThuc", SqlDbType.DateTime, GearBox.EndDate, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<GearBox>(out GearBoxList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<GearBox>()
            {
                ItemList = GearBoxList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
    }
}

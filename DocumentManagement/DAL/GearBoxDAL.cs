using Common.Common;
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
        private GearBoxDAL() { }

        private static volatile GearBoxDAL _instance;

        static object key = new object();

        public static GearBoxDAL GetGearBoxDALInstance
        {
            get
            {
                lock (key)
                {
                    if (_instance == null)
                    {
                        _instance = new GearBoxDAL();
                    }
                }

                return _instance;
            }

            private set
            {
                _instance = value;
            }
        }
        public ReturnResult<GearBox> GetPagingWithSearchResults(BaseCondition<GearBox> condition)
        {
            DbProvider provider = new DbProvider();
            List<GearBox> list = new List<GearBox>();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            string totalRecords = String.Empty;
            var result = new ReturnResult<GearBox>();
            try
            {
                provider.SetQuery("GearBox_GET_PAGING", System.Data.CommandType.StoredProcedure)
                    .SetParameter("InWhere", System.Data.SqlDbType.NVarChar, condition.IN_WHERE ?? String.Empty)
                    .SetParameter("InSort", System.Data.SqlDbType.NVarChar, condition.IN_SORT ?? String.Empty)
                    .SetParameter("StartRow", System.Data.SqlDbType.Int, condition.PageIndex)
                    .SetParameter("PageSize", System.Data.SqlDbType.Int, condition.PageSize)
                    .SetParameter("TotalRecords", System.Data.SqlDbType.Int, DBNull.Value, System.Data.ParameterDirection.Output)
                    .SetParameter("ErrorCode", System.Data.SqlDbType.NVarChar, DBNull.Value, 100, System.Data.ParameterDirection.Output)
                    .SetParameter("ErrorMessage", System.Data.SqlDbType.NVarChar, DBNull.Value, 4000, System.Data.ParameterDirection.Output).GetList<GearBox>(out list).Complete();

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
            var result = new ReturnResult<GearBox>();
            GearBox item = new GearBox();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            try
            {
                dbProvider.SetQuery("GearBox_GET_BY_ID", CommandType.StoredProcedure)
                .SetParameter("HopSoID", SqlDbType.Int, gearBoxID, ParameterDirection.Input)
               .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
               .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
               .GetSingle<GearBox>(out item)
               .Complete();

            }
            catch (Exception ex)
            {

                throw;
            }
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<GearBox>()
            {
                Item = item,
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

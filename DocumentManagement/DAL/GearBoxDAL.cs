using Common.Common;
using DocumentManagement.Common;
using DocumentManagement.Model;
using DocumentManagement.Model.Entity;
using DocumentManagement.Model.Entity.GearBox;
using DocumentManagement.Model.Entity.TableOfContens;
using DocumentManagement.Models.Entity.Profile;
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
        public ReturnResult<GearBox> GetGearBoxByTabOfContID(string id)
        {
            DbProvider provider = new DbProvider();
            List<GearBox> list = new List<GearBox>();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            string totalRecords = String.Empty;
            var result = new ReturnResult<GearBox>();
            try
            {
                provider.SetQuery("GEARBOX_GET_BY_TABLE_OF_CONTENT_ID", System.Data.CommandType.StoredProcedure)
                    .SetParameter("Id", System.Data.SqlDbType.NVarChar, id ?? String.Empty)
                    .SetParameter("ErrorCode", System.Data.SqlDbType.NVarChar, DBNull.Value, 100, System.Data.ParameterDirection.Output)
                    .SetParameter("ErrorMessage", System.Data.SqlDbType.NVarChar, DBNull.Value, 4000, System.Data.ParameterDirection.Output).GetList<GearBox>(out list).Complete();
                if (list.Count > 0)
                {
                    result.ItemList = list;
                }
                provider.GetOutValue("ErrorCode", out outCode)
                           .GetOutValue("ErrorMessage", out outMessage);

                if (outCode != "0")
                {
                    result.ErrorCode = outCode;
                    result.ErrorMessage = outMessage;
                }
                else
                {
                    result.ErrorCode = "";
                    result.ErrorMessage = "";
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
        public ReturnResult<Profiles> GetProfileByGearBoxID(BaseCondition<Profiles> condition)
        {
            List<Profiles> profiles = new List<Profiles>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            var result = new ReturnResult<Profiles>();
            try
            {
                dbProvider.SetQuery("PROFILE_GET_BY_GearBoxID", CommandType.StoredProcedure)
                .SetParameter("InWhere", System.Data.SqlDbType.NVarChar, condition.IN_WHERE == null ? "" : condition.IN_WHERE)
                .SetParameter("InSort", System.Data.SqlDbType.NVarChar, condition.IN_SORT == null ? "" : condition.IN_SORT)
                .SetParameter("StartRow", System.Data.SqlDbType.Int, condition.PageIndex)
                .SetParameter("PageSize", System.Data.SqlDbType.Int, condition.PageSize)
                .SetParameter("TotalRecords", System.Data.SqlDbType.Int, DBNull.Value, System.Data.ParameterDirection.Output)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<Profiles>(out profiles)
                .Complete();

                if (profiles.Count > 0)
                {
                    result.ItemList = profiles;
                }
                dbProvider.GetOutValue("ErrorCode", out outCode)
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

        public ReturnResult<GearBox> GetGearBoxByTableOfContentsID(BaseCondition<GearBox> condition)
        {
            List<GearBox> gearBoxes = new List<GearBox>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            var result = new ReturnResult<GearBox>();
            try
            {
                dbProvider.SetQuery("GearBox_GET_BY_TABLEOFCONTID", CommandType.StoredProcedure)
                .SetParameter("InWhere", System.Data.SqlDbType.NVarChar, condition.IN_WHERE == null ? "" : condition.IN_WHERE)
                .SetParameter("InSort", System.Data.SqlDbType.NVarChar, condition.IN_SORT == null ? "" : condition.IN_SORT)
                .SetParameter("StartRow", System.Data.SqlDbType.Int, condition.PageIndex)
                .SetParameter("PageSize", System.Data.SqlDbType.Int, condition.PageSize)
                .SetParameter("TotalRecords", System.Data.SqlDbType.Int, DBNull.Value, System.Data.ParameterDirection.Output)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<GearBox>(out gearBoxes)
                .Complete();

                if (gearBoxes.Count > 0)
                {
                    result.ItemList = gearBoxes;
                }
                dbProvider.GetOutValue("ErrorCode", out outCode)
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
        
        public ReturnResult<GearBox> DeleteGearBox(int gearBoxID)
        {
            DbProvider provider = new DbProvider();
            var result = new ReturnResult<GearBox>();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            string totalRecords = String.Empty;
            GearBox item = new GearBox();
            try
            {
                provider.SetQuery("GearBox_DELETE", CommandType.StoredProcedure)
                    .SetParameter("HopSoID", SqlDbType.Int, gearBoxID, System.Data.ParameterDirection.Input)
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
        public ReturnResult<GearBox> UpdateGearBox(GearBox gearBox)
        {
            ReturnResult<GearBox> result;
            DbProvider db;
            try
            {
                result = new ReturnResult<GearBox>();
                db = new DbProvider();
                db.SetQuery("GearBox_EDIT", CommandType.StoredProcedure)
                    .SetParameter("HopSoID", SqlDbType.Int, gearBox.GearBoxID,ParameterDirection.Input)
                    .SetParameter("MaHopSo", SqlDbType.NVarChar, gearBox.GearBoxCode, 10, ParameterDirection.Input)
                    .SetParameter("TieuDeHopSo", SqlDbType.NVarChar, gearBox.GearBoxTitle,300, ParameterDirection.Input)
                    .SetParameter("MucLucHoSoID", SqlDbType.Int, gearBox.TabOfContID, ParameterDirection.Input)
                    .SetParameter("GhiChu", SqlDbType.NVarChar, gearBox.Note, 300, ParameterDirection.Input)
                    .SetParameter("NgayBatDau", SqlDbType.NVarChar, gearBox.StDate.ToString(), 100, ParameterDirection.Input)
                    .SetParameter("NgayKetThuc", SqlDbType.NVarChar, gearBox.EDate.ToString(), 100, ParameterDirection.Input)
                    .SetParameter("NgayCapNhat", SqlDbType.NVarChar, gearBox.UpdateTime.ToString(), 100, ParameterDirection.Input)
                    .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                    .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                    .ExcuteNonQuery()
                    .Complete();
                db.GetOutValue("ErrorCode", out string errorCode)
                    .GetOutValue("ErrorMessage", out string errorMessage);
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
                throw ex;
            }
            return result;
        }
        public ReturnResult<GearBox> InsertGearBox(GearBox gearBox)
        {
            DbProvider provider = new DbProvider();
            var result = new ReturnResult<GearBox>();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            string totalRecords = String.Empty;
            try
            {
                provider.SetQuery("[GearBox_INSERT]", System.Data.CommandType.StoredProcedure)
                .SetParameter("MaHopSo", SqlDbType.NVarChar, gearBox.GearBoxCode, 10, ParameterDirection.Input)
                .SetParameter("TieuDeHopSo", SqlDbType.NVarChar, gearBox.GearBoxTitle,300, ParameterDirection.Input)
                .SetParameter("MucLucHoSoID", SqlDbType.Int, gearBox.TabOfContID, ParameterDirection.Input)
                .SetParameter("GhiChu", SqlDbType.NVarChar, gearBox.Note, 300, ParameterDirection.Input)
                .SetParameter("NgayBatDau", SqlDbType.NVarChar, gearBox.StDate.ToString(), 100, ParameterDirection.Input)
                .SetParameter("NgayKetThuc", SqlDbType.NVarChar, gearBox.EDate.ToString(), 100, ParameterDirection.Input)
                .SetParameter("NgayTao", SqlDbType.NVarChar, gearBox.CreateTime.ToString(), 100, ParameterDirection.Input)
                .SetParameter("isDeleted", SqlDbType.Int, gearBox.isDeleted, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                    .GetSingle<GearBox>(out gearBox).Complete();

                provider.GetOutValue("ErrorCode", out outCode)
                          .GetOutValue("ErrorMessage", out outMessage);

                if (outCode != "0" || outCode == "")
                {
                    result.ErrorCode = outCode;
                    result.ErrorMessage = outMessage;
                }
                else
                {
                    result.Item = gearBox;
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

        public ReturnResult<Font> GetFontsByOrganID(int organID)
        {
            List<Font> fons = new List<Font>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("GET_FONTS_BY_ORGAN_ID", CommandType.StoredProcedure)
                .SetParameter("CoQuanID", SqlDbType.Int, organID, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<Font>(out fons)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Font>()
            {
                ItemList = fons,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<TableOfContents> GetTableOfContentsByFontID(int fontID)
        {
            List<TableOfContents> tableOfContens = new List<TableOfContents>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("GET_TABOFCONTS_BY_FONT_ID", CommandType.StoredProcedure)
                .SetParameter("PhongID", SqlDbType.Int, fontID, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<TableOfContents>(out tableOfContens)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<TableOfContents>()
            {
                ItemList = tableOfContens,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
    }
}

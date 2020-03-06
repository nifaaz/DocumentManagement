using Common.Common;
using DocumentManagement.Common;
using DocumentManagement.Model;

using DocumentManagement.Models.Entity.Profile;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.DAL
{
    public class ProfileDAL
    {
        private ProfileDAL() { }

        private static volatile ProfileDAL _instance;

        static object key = new object();

        public static ProfileDAL GetProfileDALInstance
        {
            get
            {
                lock (key)
                {
                    if (_instance == null)
                    {
                        _instance = new ProfileDAL();
                    }
                }

                return _instance;
            }

            private set
            {
                _instance = value;
            }
        }
        public ReturnResult<Profile> GetPagingWithSearchResults(BaseCondition<Profile> condition)
        {
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            dbProvider.SetQuery("Profile_GET_PAGING", CommandType.StoredProcedure)
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

            return new ReturnResult<Profile>()
            {
                ErrorCode = outCode,
                ErrorMessage = outMessage,
            };
        }
        public ReturnResult<Profiles> GetAllProfiles()
        {
            List<Profiles> profileList = new List<Profiles>();
            DbProvider dbProvider = new DbProvider();
            var result = new ReturnResult<Profiles>();
            try
            {
                string outCode = String.Empty;
                string outMessage = String.Empty;
                dbProvider.SetQuery("PROFILE_GET_ALL", CommandType.StoredProcedure)
                    .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                    .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 255, ParameterDirection.Output)
                    .GetList<Profiles>(out profileList)
                    .Complete();
                dbProvider.GetOutValue("ErrorCode", out outCode)
                           .GetOutValue("ErrorMessage", out outMessage);

                if (outCode != "0")
                {
                    result.Failed(outCode, outMessage);
                }
                else
                {
                    result.ItemList = profileList;
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
        public ReturnResult<Profile> ExportProfile()
        {
            List<Profile> profileList = new List<Profile>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("PROFILE_EXPORT", CommandType.StoredProcedure)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 255, ParameterDirection.Output)
                .GetList<Profile>(out profileList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Profile>()
            {
                ItemList = profileList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<Profile> GetProfileByID(int profileID)
        {
            List<Profile> profileList = new List<Profile>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("PROFILE_GET_BY_ID", CommandType.StoredProcedure)
                .SetParameter("HoSoID", SqlDbType.Int, profileID, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 255, ParameterDirection.Output)
                .GetList<Profile>(out profileList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Profile>()
            {
                ItemList = profileList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<Profile> GetProfileByGearBoxID(int gearBoxID)
        {
            List<Profile> profileList = new List<Profile>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("PROFILE_GET_BY_GearBoxID", CommandType.StoredProcedure)
                .SetParameter("HopSoID", SqlDbType.Int, gearBoxID, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 255, ParameterDirection.Output)
                .GetList<Profile>(out profileList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Profile>()
            {
                ItemList = profileList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<Profile> SearchProfile(string searchStr)
        {
            List<Profile> profileList = new List<Profile>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("PROFILE_SEARCH", CommandType.StoredProcedure)
               .SetParameter("SearchStr", SqlDbType.NVarChar, searchStr, 500, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 255, ParameterDirection.Output)
                .GetList<Profile>(out profileList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Profile>()
            {
                ItemList = profileList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<Profile> CreateProfile(Profile profile)
        {
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            dbProvider.SetQuery("PROFILE_CREATE", CommandType.StoredProcedure)
                .SetParameter("HopSoID", SqlDbType.Int, profile.GearBoxID, ParameterDirection.Input)
                .SetParameter("TieuDeHoSo", SqlDbType.NVarChar, profile.ProfileTitle, 255, ParameterDirection.Input)
                .SetParameter("TenHoSo", SqlDbType.NVarChar, profile.ProfileName, 255, ParameterDirection.Input)
                .SetParameter("NgayTao", SqlDbType.DateTime, profile.CreateTime, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .ExcuteNonQuery()
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Profile>()
            {
                ErrorCode = outCode,
                ErrorMessage = outMessage,
            };
        }

        public ReturnResult<Profile> UpdateProfile(Profile profile)
        {
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            dbProvider.SetQuery("PROFILE_UPDATE", CommandType.StoredProcedure)
                .SetParameter("HoSoID", SqlDbType.Int, profile.ProfileID, ParameterDirection.Input)
                .SetParameter("HopSoID", SqlDbType.Int, profile.GearBoxID, ParameterDirection.Input)
                .SetParameter("TieuDeHoSo", SqlDbType.Int, profile.ProfileTitle, ParameterDirection.Input)
                .SetParameter("TenHoSo", SqlDbType.Int, profile.ProfileName, ParameterDirection.Input)
                .SetParameter("NgayCapNhat", SqlDbType.DateTime, profile.UpdateTime, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .ExcuteNonQuery()
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Profile>()
            {
                ErrorCode = outCode,
                ErrorMessage = outMessage,
            };
        }

        public ReturnResult<Profile> DeleteProfile(int profileId)
        {
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            dbProvider.SetQuery("PROFILE_DELETE", CommandType.StoredProcedure)
                .SetParameter("HoSoID", SqlDbType.Int, profileId, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .ExcuteNonQuery()
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Profile>()
            {
                ErrorCode = outCode,
                ErrorMessage = outMessage,
            };
        }
        // viết lại api cho profile từ đây
        public ReturnResult<Profiles> ProfilesGetSearchWithPaging (BaseCondition<Profiles> condition)
        {
            ReturnResult<Profiles> result = new ReturnResult<Profiles>();
            DbProvider db;
            List<Profiles> lstResult;
            try
            {
                db = new DbProvider();
                lstResult = new List<Profiles>();
                db.SetQuery("PROFILES_GET_SEARCH_WITH_PAGING", CommandType.StoredProcedure)
                    .SetParameter("PageIndex", SqlDbType.Int, condition.PageIndex)
                    .SetParameter("PageSize", SqlDbType.Int, condition.PageSize)
                    .SetParameter("InWhere", SqlDbType.NVarChar, condition.IN_WHERE, 500)
                    .SetParameter("InSort", SqlDbType.NVarChar, condition.IN_SORT, 200)
                    .SetParameter("TotalRecords", SqlDbType.Int, DBNull.Value, ParameterDirection.Output)
                    .SetParameter("ErrorCode", SqlDbType.Int, DBNull.Value, ParameterDirection.Output)
                    .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 2000, ParameterDirection.Output)
                    .GetList<Profiles>(out lstResult)
                    .Complete();
                db.GetOutValue("ErrorCode", out int errorCode)
                    .GetOutValue("ErrorMessage", out string errorMessage)
                    .GetOutValue("TotalRecords", out int totalRecords);
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

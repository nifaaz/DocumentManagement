using DocumentManagement.Common;
using DocumentManagement.Model;
using DocumentManagement.Model.Entity.Profile;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.DAL
{
    public class ProfileDAL
    {
        public ReturnResult<Profile> GetAllProfile()
        {
            List<Profile> profileList = new List<Profile>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("PROFILE_GET_ALL", CommandType.StoredProcedure)
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
    }
}

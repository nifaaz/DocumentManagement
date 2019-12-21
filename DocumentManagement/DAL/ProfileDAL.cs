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

        public ReturnResult<Profile> CreateProfile(Profile profile)
        {
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            dbProvider.SetQuery("PROFILE_CREATE", CommandType.StoredProcedure)
                .SetParameter("GearBoxId", SqlDbType.Int, profile.GearBoxId, ParameterDirection.Input)
                .SetParameter("ProfileTitle", SqlDbType.NVarChar, profile.ProfileTitle, 255, ParameterDirection.Input)
                .SetParameter("ProfileName", SqlDbType.NVarChar, profile.ProfileName, 255, ParameterDirection.Input)
                .SetParameter("NumberOfDocs", SqlDbType.Int, profile.NumberOfDocs, ParameterDirection.Input)
                .SetParameter("CreatedDate", SqlDbType.DateTime, profile.CreatedDate, ParameterDirection.Input)
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
                .SetParameter("ProfileId", SqlDbType.Int, profile.ProfileId, ParameterDirection.Input)
                .SetParameter("GearBoxId", SqlDbType.Int, profile.GearBoxId, ParameterDirection.Input)
                .SetParameter("ProfileTitle", SqlDbType.Int, profile.ProfileTitle, ParameterDirection.Input)
                .SetParameter("ProfileName", SqlDbType.Int, profile.ProfileName, ParameterDirection.Input)
                .SetParameter("NumberOfDocs", SqlDbType.Int, profile.NumberOfDocs, ParameterDirection.Input)
                .SetParameter("CreatedDate", SqlDbType.DateTime, profile.CreatedDate, ParameterDirection.Input)
                .SetParameter("UpdatedDate", SqlDbType.DateTime, profile.UpdatedDate, ParameterDirection.Input)
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
            dbProvider.SetQuery("PROFILE_CREATE", CommandType.StoredProcedure)
                .SetParameter("ProfileId", SqlDbType.Int, profileId, ParameterDirection.Input)
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

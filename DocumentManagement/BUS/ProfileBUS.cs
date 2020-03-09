using Common.Common;
using DocumentManagement.Common;
using DocumentManagement.DAL;

using DocumentManagement.Models.Entity.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.BUS
{
    public class ProfileBUS
    {
        private static ProfileDAL profileDAL = ProfileDAL.GetProfileDALInstance;
        private ProfileBUS() { }

        private static volatile ProfileBUS _instance;

        static object key = new object();

        public static ProfileBUS GetProfileBUSInstance
        {
            get
            {
                lock (key)
                {
                    if (_instance == null)
                    {
                        _instance = new ProfileBUS();
                    }
                }

                return _instance;
            }

            private set
            {
                _instance = value;
            }
        }
        public ReturnResult<Profiles> GetPagingWithSearchResults(BaseCondition<Profiles> condition)
        {
            var result = profileDAL.GetPagingWithSearchResults(condition);
            return result;
        }
        //public ReturnResult<Profile> GetAllProfile()
        //{
        //    var result = profileDAL.GetAllProfile();
        //    return result;
        //}
        public ReturnResult<Profiles> ExportProfile()
        {
            var result = profileDAL.ExportProfile();
            return result;
        }
        public ReturnResult<Profiles> ProfileSearch(string serachStr)
        {
            var result = profileDAL.SearchProfile(serachStr);
            return result;
        }
        public ReturnResult<Profiles> GetProfileByID(int profileID)
        {
            var result = profileDAL.GetProfileByID(profileID);
            return result;
        }
        public ReturnResult<Profiles> GetProfileByGearBoxID(int gearBoxID)
        {
            var result = profileDAL.GetProfileByGearBoxID(gearBoxID);
            return result;
        }
        public ReturnResult<Profiles> CreateProfile(Profiles profile)
        {
            var result = profileDAL.CreateProfile(profile);
            return result;
        }
        public ReturnResult<Profiles> DeleteProfile(int profileId)
        {
            var result = profileDAL.DeleteProfile(profileId);
            return result;
        }
        public ReturnResult<Profiles> UpdateProfile(Profiles profile)
        {
            var result = profileDAL.UpdateProfile(profile);
            return result;
        }

        // viết lại profile từ đây
        public ReturnResult<Profiles> ProfilesGetSearchWithPaging (BaseCondition<Profiles> condition)
        {
            return profileDAL.ProfilesGetSearchWithPaging(condition);
        }

        public ReturnResult<Profiles> GetAllProfiles()
        {
            return profileDAL.GetAllProfiles();
        }
    }
}

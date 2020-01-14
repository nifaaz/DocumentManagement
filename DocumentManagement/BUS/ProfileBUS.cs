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
        private ProfileDAL _profileDAL;
        private ProfileDAL ProfileDAL
        {
            get
            {
                _profileDAL = new ProfileDAL();
                return _profileDAL;
            }
        }
        public ReturnResult<Profile> GetAllProfile()
        {
            var result = ProfileDAL.GetAllProfile();
            return result;
        }
        public ReturnResult<Profile> ExportProfile()
        {
            var result = ProfileDAL.ExportProfile();
            return result;
        }
        public ReturnResult<Profile> ProfileSearch(string serachStr)
        {
            var result = ProfileDAL.SearchProfile(serachStr);
            return result;
        }
        public ReturnResult<Profile> GetProfileByID(int profileID)
        {
            var result = ProfileDAL.GetProfileByID(profileID);
            return result;
        }
        public ReturnResult<Profile> GetProfileByGearBoxID(int gearBoxID)
        {
            var result = ProfileDAL.GetProfileByGearBoxID(gearBoxID);
            return result;
        }
        public ReturnResult<Profile> CreateProfile(Profile profile)
        {
            var result = ProfileDAL.CreateProfile(profile);
            return result;
        }
        public ReturnResult<Profile> DeleteProfile(int profileId)
        {
            var result = ProfileDAL.DeleteProfile(profileId);
            return result;
        }
        public ReturnResult<Profile> UpdateProfile(Profile profile)
        {
            var result = ProfileDAL.UpdateProfile(profile);
            return result;
        }
    }
}

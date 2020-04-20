using Common.Common;
using DocumentManagement.Common;
using DocumentManagement.DAL;
using DocumentManagement.Models.Entity.ComputerFile;
using DocumentManagement.Models.Entity.Document;
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

        public ReturnResult<Profile> GetProfileByGearBoxId(string gearBoxID)
        {
            var result = profileDAL.GetProfileByGearBoxId(gearBoxID);
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
        public ReturnResult<Profiles> Create (Profiles profiles, List<ComputerFile> files = null)
        {
            return profileDAL.Create(profiles, files);
        }

        public ReturnResult<Profiles> Update(Profiles profiles, List<ComputerFile> files = null, List<ComputerFile> extFiles = null, string folderPath = "")
        {
            return profileDAL.Update(profiles, files, extFiles, folderPath);
        }

        public ReturnResult<ProfileTypes> GetAllProfileTypes ()
        {
            return profileDAL.ProfileTypeGetAll();
        }

        public ReturnResult<Profiles> GetProfileByFileCode(string fileCode)
        {
            return profileDAL.GetProfileByFileCode(fileCode);
        }

        public ReturnResult<ComputerFile> GetListFilesByProfileId(BaseCondition<Profiles> condition)
        {
            return profileDAL.GetListFilesByProfileId(condition);
        }
   
        public ReturnResult<ComputerFile> GetComputerFileByProfileId(string profileId)
        {
            return profileDAL.GetComputerFileByProfileId(profileId);
        }
        public ReturnResult<Document> GetDocumentsByProfileId (BaseCondition<Profiles> condition)
        {
            return profileDAL.GetDocumentsByProfileId(condition);
        }
    }
}

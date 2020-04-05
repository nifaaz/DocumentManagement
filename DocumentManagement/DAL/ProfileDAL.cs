using Common.Common;
using DocumentManagement.Common;
using DocumentManagement.Model;
using DocumentManagement.Models.Entity.ComputerFile;
using DocumentManagement.Models.Entity.Document;
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
        public ReturnResult<ComputerFile> GetComputerFileByProfileId(string profileId)
        {
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            List<ComputerFile> list;
            ReturnResult<ComputerFile> result = new ReturnResult<ComputerFile>();
            try
            {
                list = new List<ComputerFile>();
                dbProvider.SetQuery("COMPUTER_GET_BY_PROFILE_ID", CommandType.StoredProcedure)
                   .SetParameter("ProfileId", SqlDbType.NVarChar, profileId, ParameterDirection.Input)
                   .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                   .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                   .GetList<ComputerFile>(out list)
                   .Complete();
                dbProvider.GetOutValue("ErrorCode", out outCode)
                           .GetOutValue("ErrorMessage", out outMessage);

                if (list.Count == 0)
                {
                    result.Failed("NE", "Không có file nào tồn tại trong hồ sơ, vui lòng thử lại.");
                }
                else
                {
                    if (outCode != "0")
                    {
                        result.Failed(outCode, outMessage);
                    }
                    else
                    {
                        result.ItemList = list;
                        result.Item = list[0];
                        result.ErrorCode = "0";
                        result.ErrorMessage = "";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Failed("-1", ex.Message);
            }
            return result;
        }
        
        public ReturnResult<Profiles> GetPagingWithSearchResults(BaseCondition<Profiles> condition)
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

            return new ReturnResult<Profiles>()
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
        public ReturnResult<Profiles> ExportProfile()
        {
            List<Profiles> profileList = new List<Profiles>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("PROFILE_EXPORT", CommandType.StoredProcedure)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 255, ParameterDirection.Output)
                .GetList<Profiles>(out profileList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Profiles>()
            {
                ItemList = profileList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
       
        public ReturnResult<Profiles> GetProfileByID(int profileID)
        {
            var result = new ReturnResult<Profiles>();
            Profiles profiles = new Profiles();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            try
            {
                dbProvider.SetQuery("PROFILE_GET_BY_ID", CommandType.StoredProcedure)
                .SetParameter("ProfileId", SqlDbType.Int, profileID, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 255, ParameterDirection.Output)
                .GetSingle<Profiles>(out profiles)
                .Complete();
                dbProvider.GetOutValue("ErrorCode", out outCode)
                           .GetOutValue("ErrorMessage", out outMessage);

                if (outCode.ToString() != "0")
                {
                    result.Failed(outCode, outMessage);
                }
                else
                {
                    result.Item = profiles;
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
        public ReturnResult<Profile> GetProfileByGearBoxId(string gearBoxId)
        {
            DbProvider provider = new DbProvider();
            List<Profile> list = new List<Profile>();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            string totalRecords = String.Empty;
            var result = new ReturnResult<Profile>();
            try
            {
                provider.SetQuery("PROFILE_GET_BY_GEAR_BOX_ID", CommandType.StoredProcedure)
                    .SetParameter("GearBoxId",SqlDbType.Int, gearBoxId ?? String.Empty)
                    .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                    .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                    .GetList<Profile>(out list)
                    .Complete();

                provider.GetOutValue("ErrorCode", out outCode)
                           .GetOutValue("ErrorMessage", out outMessage);

                if (outCode != "0")
                {
                    result.Failed(outCode, outMessage);
                }
                else
                {
                    if (list.Count > 0)
                    {
                        List<Profile> lstFilter = list.Where(item => item.TotalFiles > 0).ToList();
                        if (lstFilter.Count > 0)
                        {
                            result.ItemList = lstFilter;
                            result.Item = lstFilter[0]; // chỉ lấy 1 hồ sơ có trong hộp số
                            result.ErrorCode = "0";
                            result.ErrorMessage = "";
                        }
                        else
                        {
                            List<Profile> lstFileCompleted = list.Where(item => item.TotalFilesCompleted > 0).ToList();
                            if (lstFileCompleted.Count == list.Count)
                            {
                                result.Failed("CO", "Toàn bộ tài liệu trong hộp số đã được số hóa. Vui lòng chọn hộp số khác.");
                            }
                            //result.Failed("EN", "Không tồn tại hồ sơ trong hộp số. Vui lòng thử lại hoặc chọn hộp số khác.");
                        }
                    }
                    else
                    {
                        result.Failed("EN", "Không tồn tại hồ sơ trong hộp số. Vui lòng thử lại hoặc chọn hộp số khác.");
                    }
                }
            }
            catch (Exception ex)
            {
                result.Failed("-1", ex.Message);
            }
            return result;
        }
        public ReturnResult<Profiles> GetProfileByGearBoxID(int gearBoxID)
        {
            DbProvider provider = new DbProvider();
            List<Profiles> list = new List<Profiles>();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            string totalRecords = String.Empty;
            var result = new ReturnResult<Profiles>();
            try
            {
                provider.SetQuery("PROFILE_GET_BY_GEAR_BOX_ID", CommandType.StoredProcedure)
                    .SetParameter("GearBoxId", SqlDbType.Int, gearBoxID)
                    .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                    .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                    .GetList<Profiles>(out list)
                    .Complete();

                if (list.Count > 0)
                {
                    result.ItemList = list;
                    result.Item = list[0];
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
        public ReturnResult<Profiles> SearchProfile(string searchStr)
        {
            List<Profiles> profileList = new List<Profiles>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("PROFILE_SEARCH", CommandType.StoredProcedure)
               .SetParameter("SearchStr", SqlDbType.NVarChar, searchStr, 500, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 255, ParameterDirection.Output)
                .GetList<Profiles>(out profileList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Profiles>()
            {
                ItemList = profileList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<Profiles> CreateProfile(Profiles profile)
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

            return new ReturnResult<Profiles>()
            {
                ErrorCode = outCode,
                ErrorMessage = outMessage,
            };
        }

        public ReturnResult<Profiles> UpdateProfile(Profiles profile)
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

            return new ReturnResult<Profiles>()
            {
                ErrorCode = outCode,
                ErrorMessage = outMessage,
            };
        }

        public ReturnResult<Profiles> DeleteProfile(int profileId)
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

            return new ReturnResult<Profiles>()
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
                db.SetQuery("PROFILES_GET_SEARCH_WITH_PAGING", CommandType.StoredProcedure);
                db.SetParameter("PageIndex", SqlDbType.Int, condition.PageIndex);
                db.SetParameter("PageSize", SqlDbType.Int, condition.PageSize);
                db.SetParameter("InWhere", SqlDbType.NVarChar, condition.IN_WHERE, 500);
                db.SetParameter("InSort", SqlDbType.NVarChar, condition.IN_SORT, 200);
                db.SetParameter("TotalRecords", SqlDbType.Int, DBNull.Value, ParameterDirection.Output);
                db.SetParameter("ErrorCode", SqlDbType.Int, DBNull.Value, ParameterDirection.Output);
                db.SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 2000, ParameterDirection.Output);
                db.GetList<Profiles>(out lstResult);
                    db.Complete();
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

        /// <summary>
        /// thêm mới hồ sơ và upload file 
        /// </summary>
        /// <param name="profiles"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        public ReturnResult<Profiles> Create (Profiles profiles, List<ComputerFile> files = null)
        {
            ReturnResult<Profiles> result = new ReturnResult<Profiles>();
            DbProvider db;
            try
            {
                db = new DbProvider();
                string JsonStringFiles = string.Empty;
                if (files != null)
                {
                    JsonStringFiles = Libs.SerializeObject(files);
                }

                db.SetQuery("PROFILES_ADD_NEW", CommandType.StoredProcedure);
                db.SetParameter("GearBoxId", SqlDbType.Int, profiles.GearBoxId);
                db.SetParameter("ProfileType", SqlDbType.Int, profiles.ProfileTypeId);
                db.SetParameter("FileCode", SqlDbType.NVarChar, profiles.FileCode, 50);
                db.SetParameter("FileCatalog", SqlDbType.Int, profiles.FileCatalog);
                db.SetParameter("FileNotation", SqlDbType.NVarChar, profiles.FileNotation, 100);
                db.SetParameter("Title", SqlDbType.NVarChar, profiles.Title, 1000);
                db.SetParameter("Maintenance", SqlDbType.NVarChar, profiles.Maintenance, 200);
                db.SetParameter("Rights", SqlDbType.NVarChar, profiles.Rights, 200);
                db.SetParameter("Language", SqlDbType.NVarChar, profiles.Language, 50);
                db.SetParameter("StartDate", SqlDbType.DateTime, profiles.StartDate != null ? profiles.StartDate : DateTime.Now);
                db.SetParameter("EndDate", SqlDbType.DateTime, profiles.EndDate != null ? profiles.EndDate : DateTime.Now);
                db.SetParameter("TotalDoc", SqlDbType.Int, profiles.TotalDoc);
                db.SetParameter("Description", SqlDbType.NVarChar, profiles.Description, 1000);
                db.SetParameter("Keyword", SqlDbType.NVarChar, profiles.KeyWord, 200);
                db.SetParameter("InforSign", SqlDbType.NVarChar, profiles.InforSign, 200);
                db.SetParameter("SheetNumber", SqlDbType.Int, profiles.SheetNumber);
                db.SetParameter("PageNumber", SqlDbType.Int, profiles.PageNumber);
                db.SetParameter("Format", SqlDbType.NVarChar, profiles.Format, 50);
                db.SetParameter("CreateBy", SqlDbType.NVarChar, profiles.CreatedBy);
                db.SetParameter("JSONFILE", SqlDbType.NVarChar, JsonStringFiles);
                db.SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output);
                db.SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 400, ParameterDirection.Output);
                db.ExcuteNonQuery();
                db.Complete();
                db.GetOutValue("ErrorCode", out string outCode);
                db.GetOutValue("ErrorMessage", out string outMessage);

                if (outCode.ToString() != "0")
                {
                    result.Failed(outCode.ToString(), outMessage);
                }
                else
                {
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

        /// <summary>
        /// Cập nhật thông tin hồ sơ
        /// </summary>
        /// <param name="profiles"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        public ReturnResult<Profiles> Update(Profiles profiles, List<ComputerFile> files = null, List<ComputerFile> extFiles = null, string folderPath = "")
        {
            ReturnResult<Profiles> result = new ReturnResult<Profiles>();
            DbProvider db;
            try
            {
                db = new DbProvider();
                string JsonStringFiles = string.Empty;
                string JsonStringOverwrite = string.Empty;

                if (files != null)
                {
                    JsonStringFiles = Libs.SerializeObject(files);
                }

                if (extFiles != null)
                {
                    JsonStringOverwrite = Libs.SerializeObject(extFiles);
                }

                db.SetQuery("PROFILES_UPDATE", CommandType.StoredProcedure);
                db.SetParameter("ProfileId", SqlDbType.Int, profiles.ProfileId);
                db.SetParameter("GearBoxId", SqlDbType.Int, profiles.GearBoxId);
                db.SetParameter("ProfileType", SqlDbType.Int, profiles.ProfileTypeId);
                db.SetParameter("FileCode", SqlDbType.NVarChar, profiles.FileCode, 50);
                db.SetParameter("FileCatalog", SqlDbType.Int, profiles.FileCatalog);
                db.SetParameter("FileNotation", SqlDbType.NVarChar, profiles.FileNotation, 100);
                db.SetParameter("Title", SqlDbType.NVarChar, profiles.Title, 1000);
                db.SetParameter("Maintenance", SqlDbType.NVarChar, profiles.Maintenance, 200);
                db.SetParameter("Rights", SqlDbType.NVarChar, profiles.Rights, 200);
                db.SetParameter("Language", SqlDbType.NVarChar, profiles.Language, 50);
                db.SetParameter("StartDate", SqlDbType.DateTime, profiles.StartDate != null ? profiles.StartDate : DateTime.Now);
                db.SetParameter("EndDate", SqlDbType.DateTime, profiles.EndDate != null ? profiles.EndDate : DateTime.Now);
                db.SetParameter("TotalDoc", SqlDbType.Int, profiles.TotalDoc);
                db.SetParameter("Description", SqlDbType.NVarChar, profiles.Description, 1000);
                db.SetParameter("Keyword", SqlDbType.NVarChar, profiles.KeyWord, 200);
                db.SetParameter("InforSign", SqlDbType.NVarChar, profiles.InforSign, 200);
                db.SetParameter("SheetNumber", SqlDbType.Int, profiles.SheetNumber);
                db.SetParameter("PageNumber", SqlDbType.Int, profiles.PageNumber);
                db.SetParameter("Format", SqlDbType.NVarChar, profiles.Format, 50);
                db.SetParameter("UpdateBy", SqlDbType.NVarChar, profiles.UpdatedBy);
                db.SetParameter("JSONFILE", SqlDbType.NVarChar, JsonStringFiles);
                db.SetParameter("JSONOVER", SqlDbType.NVarChar, JsonStringOverwrite);
                db.SetParameter("FolderPath", SqlDbType.NVarChar, folderPath != null ? folderPath : "");
                db.SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output);
                db.SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 400, ParameterDirection.Output);
                db.ExcuteNonQuery();
                db.Complete();
                db.GetOutValue("ErrorCode", out string outCode);
                db.GetOutValue("ErrorMessage", out string outMessage);

                if (outCode.ToString() != "0")
                {
                    result.Failed(outCode.ToString(), outMessage);
                }
                else
                {
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
        //
        // lấy danh sách loại hồ sơ
        public ReturnResult<ProfileTypes> ProfileTypeGetAll()
        {
            var result = new ReturnResult<ProfileTypes>();
            DbProvider db;
            List<ProfileTypes> lst = new List<ProfileTypes>();
            try
            {
                db = new DbProvider();
                db.SetQuery("ProfileType_GET_ALL", CommandType.StoredProcedure)
                    .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                    .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                    .GetList<ProfileTypes>(out lst).Complete();

                db.GetOutValue("ErrorCode", out string outCode)
                    .GetOutValue("ErrorMessage", out string outMessage);

                if (outCode != "0")
                {
                    result.Failed(outCode, outMessage);
                }
                else
                {
                    result.ItemList = lst;
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


        /// <summary>
        /// lấy danh sách file có trong hồ sơ có profileId
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ReturnResult<ComputerFile> GetListFilesByProfileId (BaseCondition<Profiles> condition)
        {
            var result = new ReturnResult<ComputerFile>();
            DbProvider db;
            List<ComputerFile> lst = new List<ComputerFile>();
            try
            {
                db = new DbProvider();
                db.SetQuery("PROFILE_GET_All_FILE_BY_ID", CommandType.StoredProcedure)
                    .SetParameter("ProfileId", SqlDbType.Int, condition.Item.ProfileId)
                    .SetParameter("PageIndex", SqlDbType.Int, condition.PageIndex)
                    .SetParameter("PageSize", SqlDbType.Int, condition.PageSize)
                    .SetParameter("TotalRecords", SqlDbType.Int, DBNull.Value, ParameterDirection.Output)
                    .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                    .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                    .GetList<ComputerFile>(out lst).Complete();

                db.GetOutValue("ErrorCode", out string outCode)
                    .GetOutValue("ErrorMessage", out string outMessage)
                    .GetOutValue("TotalRecords", out string totalRows);

                if (lst.Count > 0)
                {
                    for (int i = 0; i < lst.Count; i++)
                    {
                        string[] urlArr = lst[i].FolderPath.Split('\\');
                        int length = urlArr.Length;
                        lst[i].Url = urlArr[length - 1] + "\\" + lst[i].FileName;
                    }
                }
                
                if (outCode != "0")
                {
                    result.Failed(outCode, outMessage);
                }
                else
                {
                    result.ItemList = lst.Count > 0 ? lst : null;
                    result.TotalRows = int.Parse(totalRows);
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

        public ReturnResult<Document> GetDocumentsByProfileId (BaseCondition<Profiles> condition)
        {
            ReturnResult<Document> result = new ReturnResult<Document>();
            DbProvider db;
            List<Document> lstResult;
            try
            {
                db = new DbProvider();
                lstResult = new List<Document>();
                db.SetQuery("PROFILE_GET_ALL_DOCUMENTS_WITH_PAGING", CommandType.StoredProcedure);
                db.SetParameter("ProfileId", SqlDbType.Int, condition.Item.ProfileId);
                db.SetParameter("StartRow", SqlDbType.Int, condition.PageIndex);
                db.SetParameter("PageSize", SqlDbType.Int, condition.PageSize);
                db.SetParameter("InWhere", SqlDbType.NVarChar, condition.IN_WHERE == null ? "" : condition.IN_WHERE, 500);
                db.SetParameter("InSort", SqlDbType.NVarChar, condition.IN_SORT == null ? "" : condition.IN_SORT, 200);
                db.SetParameter("TotalRecords", SqlDbType.Int, DBNull.Value, ParameterDirection.Output);
                db.SetParameter("ErrorCode", SqlDbType.Int, DBNull.Value, ParameterDirection.Output);
                db.SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 2000, ParameterDirection.Output);
                db.GetList<Document>(out lstResult);
                db.Complete();
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

        public ReturnResult<Profiles> GetProfileByFileCode(string fileCode)
        {
            var result = new ReturnResult<Profiles>();
            try
            {
                DbProvider dbProvider = new DbProvider();
                string outCode = String.Empty;
                string outMessage = String.Empty;
                dbProvider.SetQuery("PROFILE_GET_BY_FILECODE", CommandType.StoredProcedure)
                    .SetParameter("FileCode", SqlDbType.NVarChar, fileCode, 50, ParameterDirection.Input)
                    .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                    .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 255, ParameterDirection.Output)
                    .GetSingle<Profiles>(out Profiles profiles)
                    .Complete();
                dbProvider.GetOutValue("ErrorCode", out outCode)
                           .GetOutValue("ErrorMessage", out outMessage);

                if (outCode.ToString() != "0")
                {
                    result.Failed(outCode, outMessage);
                }
                else
                {
                    result.Item = profiles;
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Common.Common;
using DocumentManagement.BUS;
using DocumentManagement.Common;
using DocumentManagement.Models.Entity.ComputerFile;
using DocumentManagement.Models.Entity.Profile;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DocumentManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private static ProfileBUS profileBUS = ProfileBUS.GetProfileBUSInstance;
        //private static readonly string FILE_UPLOAD_DIR = Environment.CurrentDirectory + @"\FilesUpload";
        //private static readonly string CURRENT_DIRECTORY = Environment.CurrentDirectory;

        [HttpGet]
        public IActionResult GetPagingWithSearchResults(BaseCondition<Profile> condition)
        {
            var result = profileBUS.GetPagingWithSearchResults(condition);
            return Ok(result);
        }

        //[HttpGet]
        //public IActionResult GetAllProfile()
        //{
        //    var result = profileBUS.GetAllProfile();
        //    return Ok(result);
        //}
        [HttpGet("{profileID}")]
        public IActionResult GetProfileByID(int profileID)
        {
            var result = profileBUS.GetProfileByID(profileID);
            return Ok(result);
        }
        [HttpGet("{gearboxID}")]
        public IActionResult GetProfileByGeaBoxID(int gearboxID)
        {
            var result = profileBUS.GetProfileByGearBoxID(gearboxID);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateProfile(Profile profile)
        {
            var result = profileBUS.CreateProfile(profile);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult UpdateProfile(Profile profile)
        {
            var result = profileBUS.UpdateProfile(profile);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult DeleteProfile(int profileId)
        {
            var result = profileBUS.DeleteProfile(profileId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> ProfilesAddNewAndUploadFile()
        {
            IFormCollection form;
            //form = await Request.ReadFormAsync();
            object obj3 = Request.Form["profile"]; // object
            Profiles profile = Libs.DeserializeObject<Profiles>(obj3.ToString());
            ReturnResult<Profiles> result = new ReturnResult<Profiles>();
            try
            {
                List<IFormFile> files = Request.Form.Files.ToList(); // danh sách file
                List<ComputerFile> lstFilesExists = new List<ComputerFile>();


                if (files.Count > 0)
                {
                    
                    string[] lstDirFilesUpload = Directory.GetFiles(Const.FILE_UPLOAD_DIR);
                    
                    foreach (var fileAlreadyExsists in lstDirFilesUpload)
                    {
                        foreach (var file in files)
                        {
                            if (fileAlreadyExsists.IndexOf(file.FileName) > -1)
                            {
                                lstFilesExists.Add(new ComputerFile() { 
                                    FileName = fileAlreadyExsists
                                });
                            }
                        }

                        //if (System.IO.File.Exists(fileAlreadyExsists))
                        //{
                        //    System.IO.File.Delete(fileAlreadyExsists);
                        //}
                    }
                    string overwrite = Request.Form["overwrite"].ToString();
                    if (lstFilesExists.Count > 0)
                    {
                        if (overwrite == "accept")
                        {
                            foreach (var fileAlreadyExists in lstFilesExists)
                            {
                                System.IO.File.Delete(fileAlreadyExists.FileName);
                            }
                            // overwrite file already exists
                            foreach (var file in files)
                            {
                                var filePath = FilesUtillities.GetFilePath(file);
                                await FilesUtillities.CopyFileToPhysicalDisk(file, filePath);
                            }
                        }
                        else
                        {
                            var fileResult = new ReturnResult<ComputerFile>()
                            {
                                ReturnValue = Libs.SerializeObject(lstFilesExists.Select(item => item.FileName))
                            };

                            fileResult.Failed("-1", "Tồn tại file đã được upload lên hệ thống.");
                            return Ok(fileResult);
                        }
                    }
                    else
                    {
                        foreach (var file in files)
                        {
                            var filePath = FilesUtillities.GetFilePath(file);
                            await FilesUtillities.CopyFileToPhysicalDisk(file, filePath);
                        }
                    }
                }
                else
                {
                    // không tải file lên thì chỉ send thông tin hồ sơ

                }
                // get all path
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(new ErrorObject() { 
                    ErrorNumber = 1,
                    ErrorMessage = ex.Message
                });
            }
        }

        /// <summary>
        /// Lấy dữ liệu + tìm kiếm + phân trang cho hồ sơ
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ProfilesGetSearchWithPaging (BaseCondition<Profiles> condition)
        {
            return Ok(profileBUS.ProfilesGetSearchWithPaging(condition));
        }

        [HttpGet]
        public IActionResult GetAllProfiles ()
        {
            ReturnResult<Profiles> result = profileBUS.GetAllProfiles();
            return Ok(new ProfileFilterOptions()
            {
                lstFileCode = result.ItemList.Select(item => item.FileCode).Distinct().ToList(),
                lstTitle = result.ItemList.Select(item => item.Title).Distinct().ToList(),
                lstGearBoxTitle = result.ItemList.Select(item => item.GearBoxTitle).Distinct().ToList()
            });
        }
    }
}
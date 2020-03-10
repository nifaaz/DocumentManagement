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
using iTextSharp.text.pdf;

namespace DocumentManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private static readonly ProfileBUS profileBUS = ProfileBUS.GetProfileBUSInstance;
        //private static readonly string FILE_UPLOAD_DIR = Environment.CurrentDirectory + @"\FilesUpload";
        //private static readonly string CURRENT_DIRECTORY = Environment.CurrentDirectory;
        private static readonly GearBoxBUS gearBoxBUS = GearBoxBUS.GetGearBoxBUSInstance;

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

        public int GetNumberOfPdfPages(string pathDocument)
        {
            if (pathDocument.Split('.').Length > 0)
            {
                string[] arrPathFile = pathDocument.Split('.');
                int length = arrPathFile.Length;
                if (arrPathFile[length - 1].ToLower() == "pdf")
                {
                    PdfReader reader = new PdfReader(pathDocument);
                    int pages = reader.NumberOfPages;
                    reader.Dispose();
                    reader.Close();
                    return pages;
                }
            }
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
                ICollection<IFormFile> files = Request.Form.Files.ToList(); // danh sách file
                List<ComputerFile> lstFilesExists = new List<ComputerFile>();
                List<ComputerFile> lstFileInfo = new List<ComputerFile>();
               
                if (files.Count > 0)
                {
                    //foreach (var file in files)
                    //{
                    //    lstFileInfo.Add(new ComputerFile()
                    //    {
                    //        FileName = file.FileName,
                    //        Url = Const.FILE_UPLOAD_DIR + file.FileName,
                    //        PageNumber = GetNumberOfPdfPages(Const.FILE_UPLOAD_DIR + file.FileName)
                    //    });
                    //}

                    //List<ComputerFile> lstFiles = new List<ComputerFile>();
                    //foreach (var fileUrl in lstDirFilesUploaded)
                    //{
                    //    foreach (var file in files)
                    //    {
                    //        if (fileUrl.IndexOf(file.FileName) > -1)
                    //        {
                    //            lstFiles.Add(new ComputerFile()
                    //            {
                    //                FileName = file.FileName,
                    //                Url = fileUrl,
                    //                PageNumber = GetNumberOfPdfPages(fileUrl)
                    //            });
                    //        }
                    //    }
                    //}

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
                            //foreach (var file in files)
                            //{
                            //    var filePath = FilesUtillities.GetFilePath(file);
                            //    //    await FilesUtillities.CopyFileToPhysicalDisk(file, filePath);
                            //    FilesUtillities.CopyFileToPhysicalDiskSync(file, filePath);
                            //}

                            //for (int i = 0; i < files.Count; i++)
                            //{
                            //    string filePath = FilesUtillities.GetFilePath(files[i]);

                            //    using (var stream = new FileStream(filePath, FileMode.CreateNew))
                            //    {
                            //        files[i].CopyTo(stream);
                            //        stream.Close();
                            //    }
                            //}
                            foreach (var file in files)
                            {
                                
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
                            //   await FilesUtillities.CopyFileToPhysicalDisk(file, filePath);
                            FilesUtillities.CopyFileToPhysicalDiskSync(file, filePath);
                        }
                    }

                    // lấy lại danh sách file đã được tải lên
                    string[] lstDirFilesUploaded = Directory.GetFiles(Const.FILE_UPLOAD_DIR);
                    List<ComputerFile> lstFiles = new List<ComputerFile>();
                    foreach (var fileUrl in lstDirFilesUploaded)
                    {
                        foreach (var file in files)
                        {
                            if (fileUrl.IndexOf(file.FileName) > -1)
                            {
                                lstFiles.Add(new ComputerFile()
                                {
                                    FileName = file.FileName,
                                    Url = fileUrl,
                                    PageNumber = GetNumberOfPdfPages(fileUrl)
                                });
                            }
                        }
                    }

                    result = profileBUS.Create(profile, lstFileInfo);
                }
                else
                {
                    // không tải file lên thì chỉ send thông tin hồ sơ
                    result = profileBUS.Create(profile);
                }
            }
            catch (Exception ex)
            {
                return Ok(new ErrorObject(1, ex.Message));
            }
            return Ok(result);
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

        [HttpGet]
        public IActionResult GetAllProfileTypeAndGearBox ()
        {
            //ProfileNew profileNew = new ProfileNew();
            //profileNew.lstGearBox = gearBoxBUS.GetAllGearBox().ItemList;
            //profileNew.lstProfileTypes = profileBUS.GetAllProfileTypes().ItemList;
            return Ok(new ProfileNew()
            {
                lstGearBox = gearBoxBUS.GetAllGearBox().ItemList,
                lstProfileTypes = profileBUS.GetAllProfileTypes().ItemList
            });
        }
    }
}
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
        //private static readonly string FILE_UPLOAD_DIR = Environment.CurrentDirectory + @"\FilesUpload";
        //private static readonly string CURRENT_DIRECTORY = Environment.CurrentDirectory;

        [HttpGet]
        public IActionResult GetPagingWithSearchResults(BaseCondition<Profiles> condition)
        {
            var result = profileBUS.GetPagingWithSearchResults(condition);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetProfilesById([FromQuery] int profileId)
        {
            var result = profileBUS.GetProfileByID(profileId);
            return Ok(result);
        }
        [HttpGet("{gearboxID}")]
        public IActionResult GetProfileByGeaBoxID(int gearboxID)
        {
            var result = profileBUS.GetProfileByGearBoxID(gearboxID);
            return Ok(result);
        }
        // For select2
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetProfileByGearBoxId(string id)
        {
            var result = profileBUS.GetProfileByGearBoxId(id);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult CreateProfile(Profiles profile)
        {
            var result = profileBUS.CreateProfile(profile);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult UpdateProfile(Profiles profile)
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

                // directory of profile
                // mỗi hồ sơ có thư mục lưu trữ riêng ứng với FileCode
                string directoryPathFileUpload = Const.FILE_UPLOAD_DIR + profile.FileCode;
                var profileCheck = profileBUS.GetProfileByFileCode(profile.FileCode);
                if (Directory.Exists(directoryPathFileUpload) || profile.FileCode == profileCheck.Item.FileCode)
                {
                    result.Failed("-3", "Tồn tại hồ sơ có mã hồ sơ " + profile.FileCode + " trên hệ thống, vui lòng thử lại.");
                    return Ok(result);
                }
                Directory.CreateDirectory(directoryPathFileUpload);
                if (files.Count > 0)
                {
                   
                    // upload file lên server
                    foreach (var file in files)
                    {
                        var filePath = Path.Combine(directoryPathFileUpload, file.FileName);
                        FilesUtillities.CopyFileToPhysicalDiskSync(file, filePath);
                    }

                    //string[] lstDirFilesUpload = Directory.GetFiles(Const.FILE_UPLOAD_DIR);
                    //foreach (var fileAlreadyExsists in lstDirFilesUpload)
                    //{
                    //    foreach (var file in files)
                    //    {
                    //        if (fileAlreadyExsists.IndexOf(file.FileName) > -1)
                    //        {
                    //            lstFilesExists.Add(new ComputerFile() { 
                    //                FileName = fileAlreadyExsists
                    //            });
                    //        }
                    //    }
                    //}

                    #region overwrite
                    //string overwrite = Request.Form["overwrite"].ToString();
                    //if (lstFilesExists.Count > 0)
                    //{
                    //    if (overwrite == "accept")
                    //    {
                    //        foreach (var fileAlreadyExists in lstFilesExists)
                    //        {
                    //            System.IO.File.Delete(fileAlreadyExists.FileName);
                    //        }

                    //        // cập nhật lại trạng thái danh sách file được ghi đè


                    //        //    overwrite file already exists
                    //        foreach (var file in files)
                    //        {
                    //            var filePath = FilesUtillities.GetFilePath(file);
                    //            //    await FilesUtillities.CopyFileToPhysicalDisk(file, filePath);
                    //            FilesUtillities.CopyFileToPhysicalDiskSync(file, filePath);
                    //        }

                    //        //for (int i = 0; i < files.Count; i++)
                    //        //{
                    //        //    string filePath = FilesUtillities.GetFilePath(files[i]);

                    //        //    using (var stream = new FileStream(filePath, FileMode.CreateNew))
                    //        //    {
                    //        //        files[i].CopyTo(stream);
                    //        //        stream.Close();
                    //        //    }
                    //        //}
                    //    }
                    //    else
                    //    {
                    //        var fileResult = new ReturnResult<ComputerFile>()
                    //        {
                    //            ReturnValue = Libs.SerializeObject(lstFilesExists.Select(item => item.FileName))
                    //        };

                    //        fileResult.Failed("-2", "Tồn tại file đã được upload lên hệ thống.");
                    //        return Ok(fileResult);
                    //    }
                    //}
                    //else
                    //{
                    //    foreach (var file in files)
                    //    {
                    //        var filePath = FilesUtillities.GetFilePath(file);
                    //        //   await FilesUtillities.CopyFileToPhysicalDisk(file, filePath);
                    //        FilesUtillities.CopyFileToPhysicalDiskSync(file, filePath);
                    //    }
                    //}
                    #endregion

                    // lấy lại danh sách file đã được tải lên
                    string[] lstDirFilesUploaded = Directory.GetFiles(directoryPathFileUpload);
                    List<ComputerFile> lstFiles = new List<ComputerFile>();
                    foreach (var fileUrl in lstDirFilesUploaded)
                    {
                        string fileName = Path.GetFileName(fileUrl);
                        foreach (var file in files)
                        {
                            if (fileName.Equals(file.FileName))
                            {
                                lstFiles.Add(new ComputerFile()
                                {
                                    FileName = file.FileName,
                                    Size = (Math.Round((double)(file.Length / 1000000.0), 6)).ToString(),
                                    Url = fileUrl,
                                    PageNumber = GetNumberOfPdfPages(fileUrl),
                                    CreatedBy = profile.CreatedBy,
                                    FolderPath = directoryPathFileUpload
                                });
                            }
                        }
                    }

                    result = profileBUS.Create(profile, lstFiles);
                }
                else
                {
                    // không tải file lên thì chỉ send thông tin hồ sơ
                    result = profileBUS.Create(profile);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.Failed("1", ex.Message);
                return Ok(result);
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> ProfileUpdate()
        {
            IFormCollection form;
            object obj3 = Request.Form["profile"]; // object
            Profiles profile = Libs.DeserializeObject<Profiles>(obj3.ToString());
            ReturnResult<Profiles> result = new ReturnResult<Profiles>();
            try
            {
                ICollection<IFormFile> files = Request.Form.Files.ToList();
                List<ComputerFile> lstFilesExists = new List<ComputerFile>();
                List<ComputerFile> lstFileInfo = new List<ComputerFile>();
                List<ComputerFile> lstFiles = new List<ComputerFile>();
                string directoryPathFileUpload = Const.FILE_UPLOAD_DIR + profile.FileCode;

                if (!Directory.Exists(directoryPathFileUpload))
                {
                    // FileCode changed
                    //    string[] directories = Directory.GetDirectories(Const.FILE_UPLOAD_DIR);
                    var profileCheck = profileBUS.GetProfileByID(profile.ProfileId);
                    string directoryOfProfile = Const.FILE_UPLOAD_DIR + profileCheck.Item.FileCode;
                    if (Directory.Exists(directoryOfProfile))
                    {
                        Directory.Move(directoryOfProfile, directoryPathFileUpload);
                    }
                    else
                    {
                        Directory.CreateDirectory(directoryPathFileUpload);
                    }
                    if (files.Count > 0)
                    {
                        string[] lstFilesDir = Directory.GetFiles(directoryPathFileUpload);
                        if (lstFilesDir.Length > 0)
                        {
                            string[] lstDirFilesUpload = lstFilesDir;
                            foreach (var fileAlreadyExsists in lstDirFilesUpload)
                            {
                                foreach (var file in files)
                                {
                                    if (fileAlreadyExsists.IndexOf(file.FileName) > -1)
                                    {
                                        lstFilesExists.Add(new ComputerFile()
                                        {
                                            FileName = Path.GetFileName(fileAlreadyExsists),
                                            ProfileId = profile.ProfileId,
                                            Url = fileAlreadyExsists
                                        });
                                    }
                                }
                            }
                                string overwrite = Request.Form["overwrite"].ToString();
                                if (lstFilesExists.Count > 0)
                                {
                                    if (overwrite == "accept")
                                    {
                                        foreach (var fileAlreadyExists in lstFilesExists)
                                        {
                                            System.IO.File.Delete(fileAlreadyExists.Url);
                                        }
                                        //    overwrite file already exists
                                        foreach (var file in files)
                                        {
                                            string filePath = Path.Combine(directoryPathFileUpload, file.FileName);
                                            FilesUtillities.CopyFileToPhysicalDiskSync(file, filePath);
                                        }
                                    }
                                    else
                                    {
                                        var fileResult = new ReturnResult<ComputerFile>()
                                        {
                                            ReturnValue = Libs.SerializeObject(lstFilesExists.Select(item => item.FileName))
                                        };
                                        fileResult.Failed("-2", "Tồn tại file đã được upload lên hệ thống.");
                                        return Ok(fileResult);
                                    }
                                }
                                else
                                {
                                    foreach (var file in files)
                                    {
                                        var filePath = Path.Combine(directoryPathFileUpload, file.FileName);
                                        FilesUtillities.CopyFileToPhysicalDiskSync(file, filePath);
                                    }
                                }
                            }
                        else
                            {
                                foreach (var file in files)
                                {
                                    var filePath = Path.Combine(directoryPathFileUpload, file.FileName);
                                    FilesUtillities.CopyFileToPhysicalDiskSync(file, filePath);
                                }
                            }
                            // lấy lại danh sách file đã được tải lên
                            string[] lstDirFilesUploaded = Directory.GetFiles(directoryPathFileUpload);
                            foreach (var fileUrl in lstDirFilesUploaded)
                            {
                                string fileName = Path.GetFileName(fileUrl);
                                foreach (var file in files)
                                {
                                    if (fileName.Equals(file.FileName))
                                    {
                                        lstFiles.Add(new ComputerFile()
                                        {
                                            FileName = file.FileName,
                                            Size = (Math.Round((double)(file.Length / 1000000.0), 6)).ToString(),
                                            Url = fileUrl,
                                            PageNumber = GetNumberOfPdfPages(fileUrl),
                                            CreatedBy = profile.CreatedBy,
                                            FolderPath = directoryPathFileUpload
                                        });
                                    }
                                }
                            }

                            result = profileBUS.Update(profile, lstFiles, lstFilesExists, directoryPathFileUpload);
                        }
                        else
                        {
                            // không tải file lên thì chỉ send thông tin hồ sơ
                            result = profileBUS.Update(profile, null, null, directoryPathFileUpload);
                        }
                    }
                    else
                    {
                        if (files.Count > 0)
                        {
                            string[] lstFilesDir = Directory.GetFiles(directoryPathFileUpload);
                            if (lstFilesDir.Length > 0)
                            {
                                string[] lstDirFilesUpload = lstFilesDir;
                                foreach (var fileAlreadyExsists in lstDirFilesUpload)
                                {
                                    foreach (var file in files)
                                    {
                                        if (fileAlreadyExsists.IndexOf(file.FileName) > -1)
                                        {
                                            lstFilesExists.Add(new ComputerFile()
                                            {
                                                FileName = Path.GetFileName(fileAlreadyExsists),
                                                ProfileId = profile.ProfileId,
                                                Url = fileAlreadyExsists
                                            });
                                        }
                                    }
                                }
                                string overwrite = Request.Form["overwrite"].ToString();
                                if (lstFilesExists.Count > 0)
                                {
                                    if (overwrite == "accept")
                                    {
                                        foreach (var fileAlreadyExists in lstFilesExists)
                                        {
                                            System.IO.File.Delete(fileAlreadyExists.Url);
                                        }

                                        //    overwrite file already exists
                                        foreach (var file in files)
                                        {
                                            string filePath = Path.Combine(directoryPathFileUpload, file.FileName);
                                            FilesUtillities.CopyFileToPhysicalDiskSync(file, filePath);
                                        }
                                    }
                                    else
                                    {
                                        var fileResult = new ReturnResult<ComputerFile>()
                                        {
                                            ReturnValue = Libs.SerializeObject(lstFilesExists.Select(item => item.FileName))
                                        };

                                        fileResult.Failed("-2", "Tồn tại file đã được upload lên hệ thống.");
                                        return Ok(fileResult);
                                    }
                                }
                                else
                                {
                                    foreach (var file in files)
                                    {
                                        var filePath = Path.Combine(directoryPathFileUpload, file.FileName);
                                        FilesUtillities.CopyFileToPhysicalDiskSync(file, filePath);
                                    }
                                }
                            }
                            else
                            {
                                foreach (var file in files)
                                {
                                    var filePath = Path.Combine(directoryPathFileUpload, file.FileName);
                                    FilesUtillities.CopyFileToPhysicalDiskSync(file, filePath);
                                }
                            }
                            // lấy lại danh sách file đã được tải lên
                            string[] lstDirFilesUploaded = Directory.GetFiles(directoryPathFileUpload);
                            foreach (var fileUrl in lstDirFilesUploaded)
                            {
                                string fileName = Path.GetFileName(fileUrl);
                                foreach (var file in files)
                                {
                                    if (fileName.Equals(file.FileName))
                                    {
                                        lstFiles.Add(new ComputerFile()
                                        {
                                            FileName = file.FileName,
                                            Size = (Math.Round((double)(file.Length / 1000000.0), 6)).ToString(),
                                            Url = fileUrl,
                                            PageNumber = GetNumberOfPdfPages(fileUrl),
                                            CreatedBy = profile.CreatedBy,
                                            FolderPath = directoryPathFileUpload
                                        });
                                    }
                                }
                            }
                            result = profileBUS.Update(profile, lstFiles, lstFilesExists, directoryPathFileUpload);
                        }
                        else
                        {
                            // không tải file lên thì chỉ send thông tin hồ sơ
                            result = profileBUS.Update(profile, null, null, directoryPathFileUpload);
                        }
                    }
                    // check FileCode was edit ?
                    #region FILE UPLOAD
                    //if (files.Count > 0)
                    //{
                    //    string[] lstFilesDir = Directory.GetFiles(directoryPathFileUpload);
                    //    if (lstFilesDir.Length > 0)
                    //    {
                    //        string[] lstDirFilesUpload = lstFilesDir;
                    //        foreach (var fileAlreadyExsists in lstDirFilesUpload)
                    //        {
                    //            foreach (var file in files)
                    //            {
                    //                if (fileAlreadyExsists.IndexOf(file.FileName) > -1)
                    //                {
                    //                    lstFilesExists.Add(new ComputerFile()
                    //                    {
                    //                        FileName = fileAlreadyExsists,
                    //                        ProfileId = profile.ProfileId,
                    //                        Url = fileAlreadyExsists
                    //                    });
                    //                }
                    //            }
                    //        }
                    //        string overwrite = Request.Form["overwrite"].ToString();
                    //        if (lstFilesExists.Count > 0)
                    //        {
                    //            if (overwrite == "accept")
                    //            {
                    //                foreach (var fileAlreadyExists in lstFilesExists)
                    //                {
                    //                    System.IO.File.Delete(fileAlreadyExists.FileName);
                    //                }

                    //                //    overwrite file already exists
                    //                foreach (var file in files)
                    //                {
                    //                    string filePath = Path.Combine(directoryPathFileUpload, file.FileName);
                    //                    FilesUtillities.CopyFileToPhysicalDiskSync(file, filePath);
                    //                }
                    //            }
                    //            else
                    //            {
                    //                var fileResult = new ReturnResult<ComputerFile>()
                    //                {
                    //                    ReturnValue = Libs.SerializeObject(lstFilesExists.Select(item => item.FileName))
                    //                };

                    //                fileResult.Failed("-2", "Tồn tại file đã được upload lên hệ thống.");
                    //                return Ok(fileResult);
                    //            }
                    //        }
                    //        else
                    //        {
                    //            foreach (var file in files)
                    //            {
                    //                var filePath = Path.Combine(directoryPathFileUpload, file.FileName);
                    //                FilesUtillities.CopyFileToPhysicalDiskSync(file, filePath);
                    //            }
                    //        }
                    //    }
                    //    else
                    //    {
                    //        foreach (var file in files)
                    //        {
                    //            var filePath = Path.Combine(directoryPathFileUpload, file.FileName);
                    //            FilesUtillities.CopyFileToPhysicalDiskSync(file, filePath);
                    //        }
                    //    }
                    //    // lấy lại danh sách file đã được tải lên
                    //    string[] lstDirFilesUploaded = Directory.GetFiles(directoryPathFileUpload);

                    //    foreach (var fileUrl in lstDirFilesUploaded)
                    //    {
                    //        string fileName = Path.GetFileName(fileUrl);
                    //        foreach (var file in files)
                    //        {
                    //            if (fileName.Equals(file.FileName))
                    //            {
                    //                lstFiles.Add(new ComputerFile()
                    //                {
                    //                    FileName = file.FileName,
                    //                    Size = (Math.Round((double)(file.Length / 1000000.0), 6)).ToString(),
                    //                    Url = fileUrl,
                    //                    PageNumber = GetNumberOfPdfPages(fileUrl),
                    //                    CreatedBy = profile.CreatedBy,
                    //                    FolderPath = directoryPathFileUpload
                    //                });
                    //            }
                    //        }
                    //    }

                    //    result = profileBUS.Update(profile, lstFiles, lstFilesExists, directoryPathFileUpload);
                    //}
                    //else
                    //{
                    //    // không tải file lên thì chỉ send thông tin hồ sơ
                    //    result = profileBUS.Update(profile, null, null, directoryPathFileUpload);
                    //}
                    #endregion
                    return Ok(result);
                }
            catch (Exception ex)
            {
                result.Failed("1", ex.Message);
                return Ok(result);
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
        [HttpPost]
        public IActionResult GetListFilesByProfileId([FromBody] BaseCondition<Profiles> condition)
        {
            var result = profileBUS.GetListFilesByProfileId(condition);
            return Ok(result);
        }
        [HttpGet]
        [Route("{profileId}")]
        public IActionResult GetComputerFileByProfileId(string profileId)
        {
            var result = profileBUS.GetComputerFileByProfileId(profileId);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult GetDocumentsByProfileId (BaseCondition<Profiles> condition)
        {
            return Ok(profileBUS.GetDocumentsByProfileId(condition));
        }
    }
    
}
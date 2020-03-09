using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.BUS;
using DocumentManagement.Common;
using DocumentManagement.Models.Entity.ComputerFile;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ComputerFileController : ControllerBase
    {
        public async Task<OkObjectResult> UploadFilesAsync([FromForm(Name = "files")]List<IFormFile> files)
        {
            List<ReturnResult<ComputerFile>> resultList = new List<ReturnResult<ComputerFile >>();
            
            
            long size = files.Sum(f => f.Length);
            files = files.OrderBy(s => s.FileName).ToList();
            var filePaths = new List<string>();
            foreach (var file in files)
            {
                ReturnResult<ComputerFile> result = new ReturnResult<ComputerFile>();
                if (file.Length > 0)
                {
                    try
                    {
                        var filePath = GetFilePath(file);
                        await CopyFileToPhysicalDisk(file, filePath);
                        result = InsertFileInfoToDatabase(file, filePath);
                    }
                    catch (Exception ex)
                    {
                        result = new ReturnResult<ComputerFile>()
                        {
                            ErrorCode = "1",
                            ErrorMessage = ex.Message
                        };
                    }
                }
                else
                {
                    result = new ReturnResult<ComputerFile>()
                    {
                        ErrorCode = "1",
                        ErrorMessage = "File rỗng"
                    };
                }
                resultList.Add(result);
            }
            return Ok(resultList);
        }

        private async Task CopyFileToPhysicalDisk(IFormFile file, string filePath)
        {
            //filePaths.Add(filePath);
            using (var stream = new FileStream(filePath, FileMode.CreateNew))
            {
                await file.CopyToAsync(stream);
            } 
        }

        private string GetFilePath(IFormFile file)
        {
            string FILE_DIRECTORY_PATH = @"E:\New folder\";
        //    string FILE_DIRECTORY_PATH = @"~/FilesUpload";
            string filePath = FILE_DIRECTORY_PATH + file.FileName;
            return filePath;
        }
        
        private ReturnResult<ComputerFile> InsertFileInfoToDatabase(IFormFile file, string filePath)
        {
            ComputerFileBUS computerFileBUS = new ComputerFileBUS();
            var result = computerFileBUS.UploadFile(new ComputerFile()
            {
                FileName = file.FileName,
                Url = filePath,
                CreatedBy = "Nam",
                CreatedDate = DateTime.Now
            });
            return result;
        }

       
    }
}
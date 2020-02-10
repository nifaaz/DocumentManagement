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
            ComputerFileBUS computerFileBUS = new ComputerFileBUS();
            string FILE_DIRECTORY_PATH = @"E:\New folder\";
            long size = files.Sum(f => f.Length);
            files = files.OrderBy(s => s.FileName).ToList();
            var filePaths = new List<string>();
            foreach (var file in files)
            {
                ReturnResult<ComputerFile> result;
                if (file.Length > 0)
                {
                    try
                    {
                        string filePath = FILE_DIRECTORY_PATH + file.FileName;
                        filePaths.Add(filePath);
                        using (var stream = new FileStream(filePath, FileMode.CreateNew))
                        {
                            await file.CopyToAsync(stream);
                            result = computerFileBUS.UploadFile(new ComputerFile()
                            {
                                FileName = file.FileName,
                                Url = filePath,
                                CreatedBy = "Nam",
                                CreatedDate = DateTime.Now
                            }); ;

                        }
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
    }
}
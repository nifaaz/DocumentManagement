using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Common.Common;
using DocumentManagement.BUS;
using DocumentManagement.Common;
using DocumentManagement.FrameWork;
using DocumentManagement.Models.Entity;
using DocumentManagement.Models.Entity.ComputerFile;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DigitalSignatureController : ControllerBase
    {
        private readonly DigitalSignatureBUS digitalSignatureBUS = DigitalSignatureBUS.GetDigitalSignatureBUSInstance;

        [HttpPost]
        public IActionResult GetPaging ([FromBody]BaseCondition<DigitalSignature> condition)
        {
            var result = digitalSignatureBUS.GetPaging(condition);
            
            for (int i = 0; i < result.ItemList.Count; i++)
            {
                result.ItemList[i].Base64String = FilesUtillities.ConvertImageToBase64String(result.ItemList[i].Path);
            }
            return Ok(result);
        }


        [HttpGet]
        public IActionResult GetDigitalById ([FromQuery] int id)
        {
            return Ok(digitalSignatureBUS.GetById(id));
        }


        [HttpPost]
        public IActionResult Delete([FromQuery] int id, [FromQuery] string name)
        {
            ReturnResult<DigitalSignature> result = digitalSignatureBUS.Delete(id);
            if (result.IsSuccess)
            {
                if (!string.IsNullOrEmpty(name))
                {
                    string[] lstFile = Directory.GetFiles(Const.FILE_UPLOAD_DIGITAL_SIGNATURE);
                    if (lstFile.Length > 0)
                    {
                        foreach (var item in lstFile)
                        {
                            if (Path.GetFileName(item) == name)
                            {
                                System.IO.File.Delete(item);
                                break;
                            }
                        }
                    }
                }
            }
            return Ok(result);
        }


        [HttpPost]
        public IActionResult CreateSignature ()
        {
            var signature = Request.Form["signatureInfo"];
            DigitalSignature digitalSignature = Libs.DeserializeObject<DigitalSignature>(signature.ToString());
            ReturnResult<DigitalSignature> result = new ReturnResult<DigitalSignature>();
            string fileNameExists = string.Empty;

            try
            {
                IFormFile file = Request.Form.Files["file"]; // danh sách file
                string overwrite = Request.Form["overwrite"].ToString();
                string filePath = Path.Combine(Const.FILE_UPLOAD_DIGITAL_SIGNATURE, file.FileName);
                int overwriteValue = 0;

                if (file.Length > 0)
                {
                    // check exists
                    string[] lstFileAlreadyExists = Directory.GetFiles(Const.FILE_UPLOAD_DIGITAL_SIGNATURE);
                    if (lstFileAlreadyExists.Length > 0)
                    {
                        foreach (var item in lstFileAlreadyExists)
                        {
                            if (Path.GetFileName(item) == file.FileName)
                            {
                                fileNameExists = file.FileName;
                            }
                        }

                        if (!string.IsNullOrEmpty(fileNameExists))
                        {
                            
                            if (overwrite == "1")
                            {
                                overwriteValue = 1;
                                System.IO.File.Delete(filePath);
                                FilesUtillities.CopyFileToPhysicalDiskSync(file, filePath);
                            }
                            else
                            {
                                var fileResult = new ReturnResult<ComputerFile>()
                                {
                                    ReturnValue = file.FileName
                                };
                                fileResult.Failed("-2", "Tồn tại file đã được upload lên hệ thống.");
                                return Ok(fileResult);
                            }
                        }
                        else
                        {
                            FilesUtillities.CopyFileToPhysicalDiskSync(file, filePath);
                        }
                    }
                    else
                    {
                        FilesUtillities.CopyFileToPhysicalDiskSync(file, filePath);
                    }
                    digitalSignature.Path = filePath;
                    digitalSignature.ServerPath = Const.DIGITAL_SIGNATURE_FOLDER + file.FileName;
                }
                
                // send information
                result = digitalSignatureBUS.Create(digitalSignature, overwriteValue);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost]
        public IActionResult UpdateStatus ([FromQuery] int id)
        {
            return Ok(digitalSignatureBUS.UpdateStatus(id));
        }

        [HttpGet]
        public IActionResult GetStatus ()
        {
            ReturnResult<int> status = new ReturnResult<int>();
            try
            {
                var result = digitalSignatureBUS.GetAll();
                //List<int> lstStatus;
                //if (result.IsSuccess)
                //{
                //    lstStatus = result.ItemList.Select(item => item.Status)
                //        .Distinct().ToList();
                //    status.ItemList = lstStatus;
                //    status.ErrorCode = "0";
                //    status.ErrorMessage = "";
                //}
                //else
                //{
                //    status.Failed(result.ErrorCode, result.ErrorMessage);
                //}
                return Ok(result);
            }
            catch (Exception ex)
            {
                status.Failed("-1", ex.Message);
                return Ok(status);
            }
        }
    }
}
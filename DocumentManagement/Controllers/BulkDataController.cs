using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DocumentManagement.BUS;
using DocumentManagement.Common;
using DocumentManagement.Common.Utils;
using DocumentManagement.FrameWork;
using DocumentManagement.Models.DTO;
using DocumentManagement.Services.Common;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace DocumentManagement.Controllers
{

    public class BulkDataController : BaseApiController
    {
        private readonly IFileService _fileService;
        private readonly IMultipartRequestHelperService _multipartRequestHelperService;
        public BulkDataController(IFileService fileService, IMultipartRequestHelperService multipartRequestHelperService)
        {
            _fileService = fileService;
            _multipartRequestHelperService = multipartRequestHelperService;
        }

        [HttpGet]
        public HttpResponseMessage DownloadFile([FromQuery] int fileId)
        {
            var profileBUS = ProfileBUS.GetProfileBUSInstance;
            var computerFile = profileBUS.GetComputerFileById(fileId).Item;
            if (computerFile == null)
                return new HttpResponseMessage(HttpStatusCode.BadRequest);

            HttpResponseMessage result = _fileService.DownloadFile(computerFile.Url, computerFile.FileName, computerFile.FileName);

            return result;
        }

        [HttpGet]
        public async Task<IActionResult> DownloadImportTemplate()
        {
            var directoryFolderImportTemplate = Path.Combine(Const.FILE_IMPORT_TEMPLATE_DIR);
            if (!Directory.Exists(directoryFolderImportTemplate))
            {
                Directory.CreateDirectory(directoryFolderImportTemplate);
            }

            var directoryFileImportTemplate = Path.Combine(Const.FILE_IMPORT_TEMPLATE_DIR, "ImportTemplate.xls");
            if (_fileService.FileExist(directoryFileImportTemplate))
            {
                var memory = new MemoryStream();
                using (var stream = new FileStream(directoryFileImportTemplate, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return File(memory, _fileService.GetContentType(directoryFileImportTemplate), Path.GetFileName(directoryFileImportTemplate));
            }
            else
                return Content("Filename not present");
        }



        [HttpPost]
        public async Task<IActionResult> ValidateBulkInsert()
        {
            IFormCollection form;
            form = await Request.ReadFormAsync();
            object obj = Request.Form["validData"]; // object
            ValidaImportDto data = Libs.DeserializeObject<ValidaImportDto>(obj.ToString());
            ReturnResult<ValidaImportDto> result = new ReturnResult<ValidaImportDto>();
            IFormFile file = Request.Form.Files.FirstOrDefault();
            result.Item.ImportDataDTOs = new List<ImportDataDTO>();
            string directoryPathFileUpload = Path.Combine(Const.FILE_UPLOAD_DIR, Const.FILE_IMPORT);

            if (!_fileService.FileExist(directoryPathFileUpload))
            {
                Directory.CreateDirectory(directoryPathFileUpload);
                _fileService.Delete(directoryPathFileUpload, file.FileName);
            }

            var fileExtension = file.FileName.Split('.');
            if (fileExtension.Length != 0)
            {
                if (fileExtension[1].Equals("xls") || fileExtension[1].Equals("xlsx") || fileExtension[1].Equals("csv"))
                {
                    string path = Path.Combine(directoryPathFileUpload, file.FileName);
                    FilesUtillities.CopyFileToPhysicalDiskSync(file, path);
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    using (var stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            int column = 1;
                            do
                            {
                                while (reader.Read())
                                {
                                    if (column == 1 || column == 2)
                                    {
                                        UpdateDataCustom(reader, column, result.Item);
                                    }
                                    else if (column == 4 || column == 5 || column == 6)
                                    {
                                        result.Item.ProfileTitle = ReadDataProfileTitle(reader, column);
                                    }
                                    else if (column == 3)
                                    {
                                        column++;
                                        continue;
                                    }
                                    else
                                    {
                                        var importData = GetImportDataDTO(reader, column);
                                        if (importData != null && (!string.IsNullOrEmpty(importData.NameAndCompendium) || importData.Order != 0))
                                        {
                                            result.Item.ImportDataDTOs.Add(importData);
                                        }
                                    }
                                    column++;
                                }
                            } while (reader.NextResult());
                        }
                    }
                    _fileService.Delete(directoryPathFileUpload, file.FileName);
                }
            }

            return Ok();
        }
        private void UpdateDataCustom(IExcelDataReader reader, int step = 0, ValidaImportDto validaImportDto = null)
        {
            // read the data of the record and gearbox
            if (step == 1)
            {
                for (int column = 0; column < reader.FieldCount; column++)
                {
                    if (column == 0)
                    {
                        var value = reader.GetValue(column).ToString().Split('-');
                        if (value != null && value.Count() != 0)
                        {
                            validaImportDto.GearBoxNumber = value[0];
                            validaImportDto.ProfileNumber = value[1];
                            break;
                        }
                    }
                }
            }
            else
            {
                //read the data of the table of content
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (i == 0)
                    {
                        var value = reader.GetValue(i).ToString();
                        validaImportDto.TableOfContentName = !string.IsNullOrEmpty(value) ? value : string.Empty;
                        break;
                    }
                }
            }
        }

        private ImportDataDTO GetImportDataDTO(IExcelDataReader reader, int step = 0)
        {
            var importData = new ImportDataDTO();
            for (int column = 0; column < reader.FieldCount; column++)
            {
                var value = reader.GetValue(column);
                if (column == (int)Type.Order)
                {
                    importData.Order = value != null ? int.Parse(value.ToString()) : 0;
                }
                else  if (column == (int)Type.Symbol)
                {
                    importData.Symbol = value != null ? value.ToString() : string.Empty;
                }
                else if (column == (int)Type.Date)
                {
                    importData.Date = value != null ? value.ToString() : string.Empty;
                }
                else if (column == (int)Type.NameAndCompendium)
                {
                    importData.NameAndCompendium = value != null ? value.ToString() : string.Empty;
                }
                else if (column == (int)Type.Author)
                {
                    importData.Author = value != null ? value.ToString() : string.Empty;
                }
                else if (column == (int)Type.Original || (column == (int)Type.Original + 1))
                {
                    if (column == (int)Type.Original)
                    {
                        importData.Original = value != null ? true : false;
                    }
                    else
                        importData.Original = value != null ? true : false;
                }
                else if (column == (int)Type.PageNumber)
                {
                    importData.PageNumber = value != null ? value.ToString() : string.Empty;
                }
                else
                {
                    importData.Detail = value != null ? value.ToString() : string.Empty;
                }
            }
            return importData;
        }

        private string ReadDataProfileTitle(IExcelDataReader reader, int step = 0)
        {
            var title = string.Empty;
            for (int column = 0; column < reader.FieldCount; column++)
            {
                if (column == 3 && step == 4)
                {
                    title = reader.GetValue(column).ToString();
                    break;
                }
            }
            return title;
        }


        enum Type
        {
            Order = 0,
            Symbol = 1,
            Date = 2,
            NameAndCompendium = 3,
            Author = 4,
            Original = 5,
            PageNumber = 7,
            Detail = 8
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DocumentManagement.BUS;
using DocumentManagement.FrameWork;
using DocumentManagement.Services.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controllers
{
    public class FileController : BaseApiController
    {
        private readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet]
        public HttpResponseMessage DownloadFile([FromQuery] int fileId)
        {
            var profileBUS = ProfileBUS.GetProfileBUSInstance;
            var computerFile = profileBUS.GetComputerFileById(fileId).Item;
            if (computerFile == null)
                return new HttpResponseMessage(HttpStatusCode.BadRequest);

            HttpResponseMessage result = _fileService.DownloadFile(computerFile.Url,computerFile.FileName, computerFile.FileName);

            return result;
        }

        [HttpGet]
        public async Task<IActionResult> DownloadProfileAttachment([FromQuery] int fileId)
        {
            var profileBUS = ProfileBUS.GetProfileBUSInstance;
            var computerFile = profileBUS.GetComputerFileById(fileId).Item;
            if (computerFile == null)
                return Content("Filename not present");
            var path = Path.Combine(computerFile.FolderPath, computerFile.FileName);
            if (!_fileService.FileExist(path))
            {
                return Content("File not exits");
            }
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, _fileService.GetContentType(path), Path.GetFileName(path));
        }

    }
}
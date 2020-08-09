using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DocumentManagement.Services.Common
{
    public interface IFileService
    {
        HttpResponseMessage DownloadFile(string fileFolder,string fileName, string originalFileName);
        bool FileExist(string path);
        string GetContentType(string path);
    }
}

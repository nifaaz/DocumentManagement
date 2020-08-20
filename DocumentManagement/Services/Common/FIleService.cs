using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DocumentManagement.Services.Common
{
    public class FileService : IFileService
    {
        public HttpResponseMessage DownloadFile(string fileFolder,string fileName, string originalFileName)
        {
            string path = Path.Combine(fileFolder);

            if (!File.Exists(path))
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            string mimetype;
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out mimetype);
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = originalFileName
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(mimetype);
            result.Content.Headers.ContentLength = stream.Length;
            return result;
        }

        public bool FileExist(string path)
        {
            if (File.Exists(path))
                return true;
            return false;
        }

        public void Delete(string folderPath, string fileName)
        {
            var filePath = Path.Combine(folderPath, fileName);
            
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        public string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},  
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}

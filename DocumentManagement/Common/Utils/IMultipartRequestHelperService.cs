using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Common.Utils
{
    public interface IMultipartRequestHelperService
    {
        string GetBoundary(MediaTypeHeaderValue contentType, int lengthLimit);
        bool IsMultipartContentType(string contentType);
        bool HasFormDataContentDisposition(ContentDispositionHeaderValue contentDisposition);
        bool HasFileContentDisposition(ContentDispositionHeaderValue contentDisposition);
        FileStream OpenFile(string fileFolder, string fileName);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Common
{
    public static class Const
    {
        public static readonly string FILE_UPLOAD_DIR = Environment.CurrentDirectory + @"\FilesUpload\";
        public static readonly string CURRENT_DIRECTORY = Environment.CurrentDirectory;
        public static readonly string FILE_UPLOAD_DIGITAL_SIGNATURE = FILE_UPLOAD_DIR + @"SignaturesImage\";
    }
}

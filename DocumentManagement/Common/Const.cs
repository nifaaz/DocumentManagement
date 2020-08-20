using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Common
{
    public static class Const
    {
        public static readonly string API_URL = @"https://localhost:44357";


        public static readonly string FILE_UPLOAD_DIR = Environment.CurrentDirectory + @"\FilesUpload\";
        public static readonly string FILE_IMPORT = @"FileImport";
        public static readonly string CURRENT_DIRECTORY = Environment.CurrentDirectory;
        public static readonly string FILE_UPLOAD_DIGITAL_SIGNATURE = FILE_UPLOAD_DIR + @"SignaturesImage\";
        public static readonly string CLIENT_PATH_UPLOAD_FILE = @"http://localhost:4200/assets/pdf/";
        public static readonly string FILE_SERVER_FOLDER = @"/FilesUpload/";
        public static readonly string FILE_IMPORT_TEMPLATE_DIR = Environment.CurrentDirectory + @"\FilesUpload\ImportTemplate";

        public static readonly string DIGITAL_SIGNATURE_FOLDER = @"/FilesUpload/SignaturesImage/";
    }
}

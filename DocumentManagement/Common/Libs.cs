using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DocumentManagement.Common
{
    public static class Libs
    {
        /// <summary>
        /// hàm chuyển đổi json sang object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T DeserializeObject<T> (string jsonString) where T : new ()
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        /// <summary>
        /// convert object sang json string
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>string</returns>
        public static string SerializeObject (object obj)
        {
            if (obj == null)
            {
                return "";
            }
            return JsonConvert.SerializeObject(obj);
        }

        public static string GetFullPathDirectoryFileUpload ()
        {
            string currentPath = Environment.CurrentDirectory;
            DirectoryInfo di = Directory.GetParent(currentPath);
            string parentBothPath = di.Parent.FullName;
            string clientFileUploadPath = @"SoHoa_v1\SoHoaV1\src\assets\pdf";
            return Path.Combine(parentBothPath, clientFileUploadPath);
        }
    }
}

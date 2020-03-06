using System;
using System.Collections.Generic;
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

        public static string SerializeObject (object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}

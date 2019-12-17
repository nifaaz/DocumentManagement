using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Common
{
    public class ReturnResult<T> where T:new()
    {
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }
        public T Item { get; set; } = new T();
        public List<T> ItemList { get; set; } = new List<T>();
        public bool HasData
        {
            get
            {
                return Item == null || Item == null;
            }
        }

        public bool HasError
        {
            get
            {
                return string.IsNullOrEmpty(ErrorCode) && ErrorCode != "0";
            }
        }

        public int TotalRows { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models.DTO
{
    public class DataValidationDTO
    {
        public string Value { get; set; }
        public int DataType { get; set; }
        public int RowNumber { get; set; }
        public bool IsCorrect { get; set; }
        public string Message { get; set; }
    }
}

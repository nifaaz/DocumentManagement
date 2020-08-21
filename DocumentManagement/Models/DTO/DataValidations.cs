using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models.DTO
{
    public class DataValidations
    {
        public bool IsCorrect { get; set; }
        public List<DataValidationDTO> dataValidationDTOs { get; set; }
    }
}

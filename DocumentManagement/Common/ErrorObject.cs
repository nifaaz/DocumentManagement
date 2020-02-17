using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Common
{
    public class ErrorObject
    {
        public int ErrorNumber { get; set; }
        public string ErrorMessage { get; set; }

        public ErrorObject()
        {

        }
        public ErrorObject(int errorNumber, string errorMessage)
        {
            this.ErrorNumber = errorNumber;
            this.ErrorMessage = errorMessage;
        }
    }
}

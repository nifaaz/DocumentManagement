using Common.Common;
using DocumentManagement.Common;
using DocumentManagement.DAL;
using DocumentManagement.Models.Entity.ComputerFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.BUS
{
    public class ComputerFileBUS
    {
        private ComputerFileDAL _computerFileDAL;
        private ComputerFileDAL ComputerDAL
        {
            get
            {
                _computerFileDAL = new ComputerFileDAL();
                return _computerFileDAL;
            }
        }
        public ReturnResult<ComputerFile> UploadFile(ComputerFile fileList)
        {
            var result = ComputerDAL.UploadFile(fileList);
            return result;
        }
        
    }
}

using Common.Common;
using DocumentManagement.Common;
using DocumentManagement.Model;
using DocumentManagement.Models.Entity.ComputerFile;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DocumentManagement.DAL
{
    public class ComputerFileDAL
    {
        public ReturnResult<ComputerFile> UploadFile(ComputerFile file)
        {
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            
            dbProvider.SetQuery("COMPUTER_FILE_UPLOAD", CommandType.StoredProcedure)
            .SetParameter("FileName", SqlDbType.NVarChar, file.FileName, ParameterDirection.Input)
            .SetParameter("Url", SqlDbType.NVarChar, file.Url, ParameterDirection.Input)
            .SetParameter("CreatedBy", SqlDbType.NVarChar, file.CreatedBy, ParameterDirection.Input)
            .SetParameter("CreatedDate", SqlDbType.Date, file.CreatedDate, ParameterDirection.Input)
            .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
            .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
            .ExcuteNonQuery()
            .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<ComputerFile>()
            {
                ErrorCode = outCode,
                ErrorMessage = outMessage,
            };
        }
    }
}

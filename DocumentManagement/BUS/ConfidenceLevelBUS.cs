using ConfidenceLevelManagement.DAL;
using DocumentManagement.Common;
using DocumentManagement.Models.Entity.ConfidenceLevel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.BUS
{
    public class ConfidenceLevelBUS
    {
        private ConfidenceLevelDAL _confidenceLevelDAL;
        private ConfidenceLevelDAL ConfidenceLevelDAL
        {
            get
            {
                _confidenceLevelDAL = new ConfidenceLevelDAL();
                return _confidenceLevelDAL;
            }
        }
        public ReturnResult<ConfidenceLevel> GetAllConfidenceLevel()
        {
            var result = ConfidenceLevelDAL.GetAllConfidenceLevel();
            return result;
        }
    }
}

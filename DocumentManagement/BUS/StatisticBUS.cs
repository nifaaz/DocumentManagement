using DocumentManagement.Common;
using DocumentManagement.DAL;
using DocumentManagement.Models.Entity.Statistic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.BUS
{
    public class StatisticBUS
    {
        private StatisticDAL _statisticDAL;
        private StatisticDAL StatisticDAL
        {
            get
            {
                _statisticDAL = new StatisticDAL();
                return _statisticDAL;
            }
        }
        public ReturnResult<Statistic> GetStatisticByNumberOfFiles()
        {
            var result = StatisticDAL.GetStatisticByNumberOfFiles();
            return result;
        }
    }
}

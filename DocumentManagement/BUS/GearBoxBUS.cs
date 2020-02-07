using Common.Common;
using DocumentManagement.Common;
using DocumentManagement.DAL;
using DocumentManagement.Model.Entity.GearBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.BUS
{
    public class GearBoxBUS
    {
        private static GearBoxDAL gearBoxDAL = GearBoxDAL.GetGearBoxDALInstance;
        private GearBoxBUS() { }

        private static volatile GearBoxBUS _instance;

        static object key = new object();

        public static GearBoxBUS GetGearBoxBUSInstance
        {
            get
            {
                lock (key)
                {
                    if (_instance == null)
                    {
                        _instance = new GearBoxBUS();
                    }
                }

                return _instance;
            }

            private set
            {
                _instance = value;
            }
        }
        public ReturnResult<GearBox> GetPagingWithSearchResults(BaseCondition<GearBox> condition)
        {
            var result = gearBoxDAL.GetPagingWithSearchResults(condition);
            return result;
        }
        public ReturnResult<GearBox> GearBoxExport()
        {
            var result = gearBoxDAL.GearBoxExport();
            return result;
        }
        public ReturnResult<GearBox> GetAllGearBox()
        {
            var result = gearBoxDAL.GetAllGearBox();
            return result;
        }
        public ReturnResult<GearBox> GearBoxSearch(string serachStr)
        {
            var result = gearBoxDAL.GearBoxSearch(serachStr);
            return result;
        }
        public ReturnResult<GearBox> GetGearBoxByID(int gearBoxID)
        {
            var result = gearBoxDAL.GetGearBoxByID(gearBoxID);
            return result;
        }
        public ReturnResult<GearBox> GetGearBoxByTabOfContID(int tabOfContID)
        {
            var result = gearBoxDAL.GetGearBoxByTableOfContentsID(tabOfContID);
            return result;
        }
        public ReturnResult<GearBox> CreateGearBox(GearBox gearBox)
        {
            var result = gearBoxDAL.InsertGearBox(gearBox);
            return result;
        }
        public ReturnResult<GearBox> DeleteGearBox(int gearBoxId)
        {
            var result = gearBoxDAL.DeleteGearBox(gearBoxId);
            return result;
        }
        public ReturnResult<GearBox> UpdateGearBox(GearBox gearBox)
        {
            var result = gearBoxDAL.UpdateGearBox(gearBox);
            return result;
        }
    }
}

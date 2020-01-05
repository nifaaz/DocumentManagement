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
        private GearBoxDAL _GearBoxDAL;
        private GearBoxDAL GearBoxDAL
        {
            get
            {
                _GearBoxDAL = new GearBoxDAL();
                return _GearBoxDAL;
            }
        }
        public ReturnResult<GearBox> GetAllGearBox()
        {
            var result = GearBoxDAL.GetAllGearBox();
            return result;
        }
        public ReturnResult<GearBox> GearBoxSearch(string serachStr)
        {
            var result = GearBoxDAL.GearBoxSearch(serachStr);
            return result;
        }
        public ReturnResult<GearBox> GetGearBoxByID(int gearBoxID)
        {
            var result = GearBoxDAL.GetGearBoxByID(gearBoxID);
            return result;
        }
        public ReturnResult<GearBox> GetGearBoxByTabOfContID(int tabOfContID)
        {
            var result = GearBoxDAL.GetGearBoxByTableOfContentsID(tabOfContID);
            return result;
        }
        public ReturnResult<GearBox> CreateGearBox(GearBox gearBox)
        {
            var result = GearBoxDAL.InsertGearBox(gearBox);
            return result;
        }
        public ReturnResult<GearBox> DeleteGearBox(int gearBoxId)
        {
            var result = GearBoxDAL.DeleteGearBox(gearBoxId);
            return result;
        }
        public ReturnResult<GearBox> UpdateGearBox(GearBox gearBox)
        {
            var result = GearBoxDAL.UpdateGearBox(gearBox);
            return result;
        }
    }
}

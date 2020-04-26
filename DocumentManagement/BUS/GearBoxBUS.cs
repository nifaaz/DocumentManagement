using Common.Common;
using DocumentManagement.Common;
using DocumentManagement.DAL;
using DocumentManagement.Model.Entity;
using DocumentManagement.Model.Entity.GearBox;
using DocumentManagement.Model.Entity.TableOfContens;
using DocumentManagement.Models.DTO;
using DocumentManagement.Models.Entity.Profile;
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
        public async Task<ReturnResult<GearBox>> GetPagingWithSearchResults(BaseCondition<GearBox> condition)
        {
            var result = await gearBoxDAL.GetPagingWithSearchResults(condition);
            return result;
        }
        public ReturnResult<GearBox> GearBoxExport()
        {
            var result = gearBoxDAL.GearBoxExport();
            return result;
        }
        public async Task<ReturnResult<GearBox>> GetGearBoxByTabOfContID(string id)
        {
            var result = await gearBoxDAL.GetGearBoxByTabOfContID(id);
            return result;
        }
        
        public async Task<ReturnResult<GearBox>> GetAllGearBox()
        {
            var result =await gearBoxDAL.GetAllGearBox();
            return result;
        }
        public ReturnResult<GearBox> GearBoxSearch(string serachStr)
        {
            var result = gearBoxDAL.GearBoxSearch(serachStr);
            return result;
        }
        public async Task<ReturnResult<GearBox>> GetGearBoxByID(int gearBoxID)
        {
            var result = await gearBoxDAL.GetGearBoxByID(gearBoxID);
            return result;
        }
        public async Task<ReturnResult<GearBox>> GetGearBoxByTabOfContID(BaseCondition<GearBox> condition)
        {
            var result = await gearBoxDAL.GetGearBoxByTableOfContentsID(condition);
            return result;
        }
        public async Task<ReturnResult<Profiles>> GetProfileByGearBoxID(BaseCondition<Profiles> condition)
        {
            var result =await gearBoxDAL.GetProfileByGearBoxID(condition);
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
        /// <summary>
        /// get the list font by organID
        /// </summary>
        /// <param name="organID"></param>
        /// <returns></returns>
        public ReturnResult<Font> GetFontsByOrganID(int organID)
        {
            var rs = gearBoxDAL.GetFontsByOrganID(organID);
            return rs;
        }

        public async Task<ReturnResult<FontSelect2>> GetFontsByOrganIDSelect2(int organID)
        {
            var rs = await gearBoxDAL.GetFontsByOrganIDSelect2(organID);
            return rs;
        }

        public async Task<ReturnResult<TableOfContSelect2>> GetTableOfContentsByFontIDSelect2(int fontID)
        {
            var rs =await gearBoxDAL.GetTableOfContentsByFontIDSelect2(fontID);
            return rs;
        }



        /// <summary>
        /// get the list tableofcontent by fontID
        /// </summary>
        /// <param name="fontID"></param>
        /// <returns></returns>
        public ReturnResult<TableOfContents> GetTableOfContentsByFontID(int fontID)
        {
            var rs = gearBoxDAL.GetTableOfContentsByFontID(fontID);
            return rs;
        }
    }
}

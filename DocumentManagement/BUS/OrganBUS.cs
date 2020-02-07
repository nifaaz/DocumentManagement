using DocumentManagement.Common;
using DocumentManagement.DAL;
using DocumentManagement.Model.Entity.Organ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.BUS
{
    public class OrganBUS
    {
        private static OrganDAL organDAL = OrganDAL.GetOrganDALInstance;
        private OrganBUS() { }

        private static volatile OrganBUS _instance;

        static object key = new object();

        public static OrganBUS GetOrganBUSInstance
        {
            get
            {
                lock (key)
                {
                    if (_instance == null)
                    {
                        _instance = new OrganBUS();
                    }
                }

                return _instance;
            }

            private set
            {
                _instance = value;
            }
        }
        public ReturnResult<Organ> GetAllOrgan()
        {
            var result = organDAL.GetAllOrgan();
            return result;
        }
        public ReturnResult<Organ> GetOrganByID(int OrganID)
        {
            var rs = organDAL.GetOrganByID(OrganID);
            return rs;
        }
        public ReturnResult<Organ> GetOrganByAddressID(int addressID)
        {
            var rs = organDAL.GetOrganByAddressID(addressID);
            return rs;
        }
        public ReturnResult<Organ> GetOrganByOrganTypeID(int organTypeID)
        {
            var rs = organDAL.GetOrganByOrganTypeID(organTypeID);
            return rs;
        }
        public ReturnResult<Organ> OrganSearch(string searchStr)
        {
            var rs = organDAL.OrganSearch(searchStr);
            return rs;
        }
        public ReturnResult<Organ> DeleteOrgan(int OrganID)
        {
            var rs = organDAL.DeleteOrgan(OrganID);
            return rs;
        }
        public ReturnResult<Organ> UpdateOrgan(Organ Organ)
        {
            var rs = organDAL.UpdateOrgan(Organ);
            return rs;
        }
        public ReturnResult<Organ> InsertOrgan(Organ Organ)
        {
            var rs = organDAL.InsertOrgan(Organ);
            return rs;
        }
    }
}

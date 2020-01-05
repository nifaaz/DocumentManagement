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
        private OrganDAL _OrganDAL;
        private OrganDAL OrganDAL
        {
            get
            {
                _OrganDAL = new OrganDAL();
                return _OrganDAL;
            }
        }
        public ReturnResult<Organ> GetAllOrgan()
        {
            var result = OrganDAL.GetAllOrgan();
            return result;
        }
        public ReturnResult<Organ> GetOrganByID(int OrganID)
        {
            var rs = OrganDAL.GetOrganByID(OrganID);
            return rs;
        }
        public ReturnResult<Organ> GetOrganByAddressID(int addressID)
        {
            var rs = OrganDAL.GetOrganByAddressID(addressID);
            return rs;
        }
        public ReturnResult<Organ> GetOrganByOrganTypeID(int organTypeID)
        {
            var rs = OrganDAL.GetOrganByOrganTypeID(organTypeID);
            return rs;
        }
        public ReturnResult<Organ> OrganSearch(string searchStr)
        {
            var rs = OrganDAL.OrganSearch(searchStr);
            return rs;
        }
        public ReturnResult<Organ> DeleteOrgan(int OrganID)
        {
            var rs = OrganDAL.DeleteOrgan(OrganID);
            return rs;
        }
        public ReturnResult<Organ> UpdateOrgan(Organ Organ)
        {
            var rs = OrganDAL.UpdateOrgan(Organ);
            return rs;
        }
        public ReturnResult<Organ> InsertOrgan(Organ Organ)
        {
            var rs = OrganDAL.InsertOrgan(Organ);
            return rs;
        }
    }
}

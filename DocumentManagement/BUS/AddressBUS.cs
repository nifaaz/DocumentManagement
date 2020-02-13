using DocumentManagement.Common;
using DocumentManagement.DAL;
using DocumentManagement.Model.Entity.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.BUS
{
    public class AddressBUS
    {
        private static AddressDAL addressDAL = AddressDAL.GetAddressDALInstance;
        private AddressBUS() { }

        private static volatile AddressBUS _instance;

        static object key = new object();

        public static AddressBUS GetAddressBUSInstance
        {
            get
            {
                lock (key)
                {
                    if (_instance == null)
                    {
                        _instance = new AddressBUS();
                    }
                }

                return _instance;
            }

            private set
            {
                _instance = value;
            }
        }
        //public ReturnResult<Organ> GetPagingWithSearchResults(BaseCondition<Organ> condition)
        //{
        //    var result = organDAL.GetPagingWithSearchResults(condition);
        //    return result;
        //}
        //public ReturnResult<Organ> GetAllOrgan()
        //{
        //    var result = organDAL.GetAllOrgan();
        //    return result;
        //}
        //public ReturnResult<Organ> GetOrganByID(int OrganID)
        //{
        //    var rs = organDAL.GetOrganByID(OrganID);
        //    return rs;
        //}
        public ReturnResult<Provincial> GetAllTinh()
        {
            var rs = addressDAL.GetAllTinh();
            return rs;
        }
        public ReturnResult<District> GetHuyenByTinhID(int tinhID)
        {
            var rs = addressDAL.GetHuyenByTinhID(tinhID);
            return rs;
        }
        public ReturnResult<Wards> GetXaPhuongByHuyenID(int huyenID)
        {
            var rs = addressDAL.GetXaPhuongByHuyenID(huyenID);
            return rs;
        }
    }
}

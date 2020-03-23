using Common.Common;
using DocumentManagement.Common;
using DocumentManagement.DAL;
using DocumentManagement.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.BUS
{
    public class DigitalSignatureBUS
    {
        private DigitalSignatureDAL digitalSignatureDAL = DigitalSignatureDAL.GetDigitalSignatureDALInstance;
        private DigitalSignatureBUS() { }

        private static volatile DigitalSignatureBUS _instance;

        static readonly object key = new object();

        public static DigitalSignatureBUS GetDigitalSignatureBUSInstance
        {
            get
            {
                lock (key)
                {
                    if (_instance == null)
                    {
                        _instance = new DigitalSignatureBUS();
                    }
                }

                return _instance;
            }

            private set
            {
                _instance = value;
            }
        }

        // function
        public ReturnResult<DigitalSignature> GetPaging (BaseCondition<DigitalSignature> condition)
        {
            return digitalSignatureDAL.GetPaging(condition);
        }

        public ReturnResult<DigitalSignature> Create(DigitalSignature digital, int overwrite = 0)
        {
            return digitalSignatureDAL.Create(digital, overwrite);
        }

        public ReturnResult<DigitalSignature> Delete(int id)
        {
            return digitalSignatureDAL.Delete(id);
        }

        public ReturnResult<DigitalSignature> GetById(int id)
        {
            return digitalSignatureDAL.GetById(id);
        }
    }
}

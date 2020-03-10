using DocumentManagement.Common;
using DocumentManagement.DAL;
using DocumentManagement.Models.Entity.DocumentType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.BUS
{
    public class DocumentTypeBUS
    {
        private DocumentTypeDAL _documentTypeDAL;
        private DocumentTypeDAL DocumentDAL
        {
            get
            {
                _documentTypeDAL = new DocumentTypeDAL();
                return _documentTypeDAL;
            }
        }

        public ReturnResult<DocumentType> GetAllDocumentType()
        {
            var result = DocumentDAL.GetAllDocumentType();
            return result;
        }
    }
}

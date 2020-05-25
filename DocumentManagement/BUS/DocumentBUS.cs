using Common.Common;
using DocumentManagement.Common;
using DocumentManagement.DAL;
using DocumentManagement.Models.Entity.Document;
using DocumentManagement.Models.Entity.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.BUS
{
    public class DocumentBUS
    {
        private DocumentDAL _documentDAL;
        private DocumentDAL DocumentDAL
        {
            get
            {
                _documentDAL = new DocumentDAL();
                return _documentDAL;
            }
        }
        public ReturnResult<DocumentPaging> GetPagingWithSearchResults(BaseCondition<DocumentPaging> condition)
        {
            var result = DocumentDAL.GetPagingWithSearchResults(condition);
            return result;
        }
        public ReturnResult<Document> GetAllDocument()
        {
            var result = DocumentDAL.GetAllDocument();
            return result;
        }
        
        public ReturnResult<Document> CreateDocument(Document document)
        {
            var result = DocumentDAL.CreateDocument(document);
            return result;
        }
        public ReturnResult<Document> DeleteDocument(Document document)
        {
            var result = DocumentDAL.DeleteDocument(document);
            return result;
        }
        public ReturnResult<Document> UpdateDocument(Document document)
        {
            var result = DocumentDAL.UpdateDocument(document);
            return result;
        }
        public ReturnResult<DocumentDetail> GetDocumentById(int documentId)
        {
            var result = DocumentDAL.GetDocumentById(documentId);
            return result;
        }
        public ReturnResult<Document> GetListByProfileId(Profiles profile)
        {
            var result = DocumentDAL.GetListByProfileId(profile);
            return result;
        }

        public ReturnResult<Document> GetDocumentPaging(BaseCondition<DocumentSearch> condition)
        {
            var result = DocumentDAL.GetDocumentPaging(condition);
            return result;
        }
    }
}

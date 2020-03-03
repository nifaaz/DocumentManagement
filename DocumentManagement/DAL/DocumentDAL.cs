using Common.Common;
using DocumentManagement.Common;
using DocumentManagement.Model;
using DocumentManagement.Models.Entity.Document;
using DocumentManagement.Models.Entity.Profile;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.DAL
{
    public class DocumentDAL
    {
        public ReturnResult<DocumentPaging> GetPagingWithSearchResults(BaseCondition<DocumentPaging> condition)
        {
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            List<DocumentPaging> documentList;
            dbProvider.SetQuery("DOCUMENT_GET_PAGING", CommandType.StoredProcedure)
                .SetParameter("FromRecord", SqlDbType.NVarChar, condition.FromRecord,  ParameterDirection.Input)
                .SetParameter("PageSize", SqlDbType.NVarChar, condition.PageSize,  ParameterDirection.Input)
                .SetParameter("InWhere", SqlDbType.NVarChar, condition.IN_WHERE, ParameterDirection.Input)
                .SetParameter("InSort", SqlDbType.NVarChar, condition. IN_SORT, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<DocumentPaging>(out documentList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<DocumentPaging>()
            {
                ItemList = documentList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
            };
        }
        public ReturnResult<Document> GetAllDocument()
        {
            List<Document> documentList = new List<Document>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("DOCUMENT_GET_ALL", CommandType.StoredProcedure)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 255, ParameterDirection.Output)
                .GetList<Document>(out documentList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Document>()
            {
                ItemList = documentList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }

        public ReturnResult<Document> CreateDocument(Document document)
        {
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            dbProvider.SetQuery("DOCUMENT_CREATE", CommandType.StoredProcedure)
                .SetParameter("DocumentCode", SqlDbType.NVarChar, document.DocumentCode, ParameterDirection.Input)
                .SetParameter("DocOrdinal", SqlDbType.Int, document.DocOrdinal, ParameterDirection.Input)
                .SetParameter("FileId", SqlDbType.Int, document.FileId, ParameterDirection.Input)
                .SetParameter("DocTypeId", SqlDbType.Int, document.DocTypeId, ParameterDirection.Input)
                .SetParameter("CodeNumber", SqlDbType.NVarChar, document.CodeNumber, ParameterDirection.Input)
                .SetParameter("CodeNotation", SqlDbType.NVarChar, document.CodeNotation, ParameterDirection.Input)
                .SetParameter("IssuedDate", SqlDbType.Date, document.IssuedDate, ParameterDirection.Input)
                .SetParameter("Subject", SqlDbType.NVarChar, document.Subject, ParameterDirection.Input)
                .SetParameter("LanguageId", SqlDbType.Int, document.LanguageId, ParameterDirection.Input)
                .SetParameter("PageAmount", SqlDbType.Int, document.PageAmount, ParameterDirection.Input)
                .SetParameter("Description", SqlDbType.NVarChar, document.Description, ParameterDirection.Input)
                .SetParameter("InforSign", SqlDbType.NVarChar, document.InforSign, ParameterDirection.Input)
                .SetParameter("Keyword", SqlDbType.NVarChar, document.Keyword, ParameterDirection.Input)
                .SetParameter("Mode", SqlDbType.NVarChar, document.Mode, ParameterDirection.Input)
                .SetParameter("ConfidenceLevelId", SqlDbType.Int, document.ConfidenceLevelId, ParameterDirection.Input)
                .SetParameter("Autograph", SqlDbType.NVarChar, document.Autograph, ParameterDirection.Input)
                .SetParameter("Format", SqlDbType.NVarChar, document.Format, ParameterDirection.Input)
                .SetParameter("ComputerFileId", SqlDbType.Int, document.ComputerFileId, ParameterDirection.Input)
                .SetParameter("CreatedDate", SqlDbType.Date, document.CreatedDate, ParameterDirection.Input)
                .SetParameter("CreatedBy", SqlDbType.NVarChar, document.CreatedBy, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .ExcuteNonQuery()
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Document>()
            {
                ErrorCode = outCode,
                ErrorMessage = outMessage,
            };
        }

        public ReturnResult<Document> UpdateDocument(Document document)
        {
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            dbProvider.SetQuery("DOCUMENT_UPDATE", CommandType.StoredProcedure)
                .SetParameter("DocumentId", SqlDbType.Int, document.DocumentCode, ParameterDirection.Input)
                .SetParameter("DocumentCode", SqlDbType.NVarChar, document.DocumentCode, ParameterDirection.Input)
                .SetParameter("DocOrdinal", SqlDbType.Int, document.DocOrdinal, ParameterDirection.Input)
                .SetParameter("FileId", SqlDbType.Int, document.FileId, ParameterDirection.Input)
                .SetParameter("DocTypeId", SqlDbType.Int, document.DocTypeId, ParameterDirection.Input)
                .SetParameter("CodeNumber", SqlDbType.NVarChar, document.CodeNumber, ParameterDirection.Input)
                .SetParameter("CodeNotation", SqlDbType.NVarChar, document.CodeNotation, ParameterDirection.Input)
                .SetParameter("IssuedDate", SqlDbType.Date, document.IssuedDate, ParameterDirection.Input)
                .SetParameter("Subject", SqlDbType.NVarChar, document.Subject, ParameterDirection.Input)
                .SetParameter("LanguageId", SqlDbType.Int, document.LanguageId, ParameterDirection.Input)
                .SetParameter("PageAmount", SqlDbType.Int, document.PageAmount, ParameterDirection.Input)
                .SetParameter("Description", SqlDbType.NVarChar, document.Description, ParameterDirection.Input)
                .SetParameter("InforSign", SqlDbType.NVarChar, document.InforSign, ParameterDirection.Input)
                .SetParameter("Keyword", SqlDbType.NVarChar, document.Keyword, ParameterDirection.Input)
                .SetParameter("Mode", SqlDbType.NVarChar, document.Mode, ParameterDirection.Input)
                .SetParameter("ConfidenceLevelId", SqlDbType.Int, document.ConfidenceLevelId, ParameterDirection.Input)
                .SetParameter("Autograph", SqlDbType.NVarChar, document.Autograph, ParameterDirection.Input)
                .SetParameter("Format", SqlDbType.NVarChar, document.Format, ParameterDirection.Input)
                .SetParameter("ComputerFileId", SqlDbType.Int, document.ComputerFileId, ParameterDirection.Input)
                .SetParameter("CreatedDate", SqlDbType.Date, document.CreatedDate, ParameterDirection.Input)
                .SetParameter("UpdatedDate", SqlDbType.Date, document.UpdatedDate, ParameterDirection.Input)
                .SetParameter("UpdatedBy", SqlDbType.NVarChar, document.UpdatedBy, ParameterDirection.Input)
                .SetParameter("IsDeleted", SqlDbType.NVarChar, document.IsDeleted, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .ExcuteNonQuery()
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Document>()
            {
                ErrorCode = outCode,
                ErrorMessage = outMessage,
            };
        }

        public ReturnResult<Document> DeleteDocument(Document document)
        {
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            dbProvider.SetQuery("DOCUMENT_DELETE", CommandType.StoredProcedure)
                .SetParameter("DocumentId", SqlDbType.Int, document.DocumentId, ParameterDirection.Input)
                .SetParameter("UpdatedDate", SqlDbType.Date, document.UpdatedDate, ParameterDirection.Input)
                .SetParameter("UpdatedBy", SqlDbType.NVarChar, document.UpdatedBy, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .ExcuteNonQuery()
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Document>()
            {
                ErrorCode = outCode,
                ErrorMessage = outMessage,
            };
        }

        public ReturnResult<Document> GetListByProfileId(Profile profile) {
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            var resultList = new List<Document>();
            dbProvider.SetQuery("DOCUMENT_GET_LIST_BY_PROFILE_ID", CommandType.StoredProcedure)
                .SetParameter("ProfileId", SqlDbType.Int, profile.ProfileID, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList(out resultList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Document>()
            {
                ItemList = resultList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
            };
        }
        public ReturnResult<Document> GetDocumentById(int documentId)
        {
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            var resultList = new List<Document>();
            dbProvider.SetQuery("DOCUMENT_GET_BY_ID", CommandType.StoredProcedure)
                .SetParameter("DocumentId", SqlDbType.Int, documentId, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList(out resultList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Document>()
            {
                ItemList = resultList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
            };
        }
        
    }
}

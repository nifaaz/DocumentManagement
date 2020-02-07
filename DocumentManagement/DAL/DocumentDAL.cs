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


        public ReturnResult<Document> GetPagingWithSearchResults(BaseCondition<Document> condition)
        {
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            dbProvider.SetQuery("DOCUMENT_GET_PAGING", CommandType.StoredProcedure)
                .SetParameter("FromRecord", SqlDbType.NVarChar, condition.FromRecord,  ParameterDirection.Input)
                .SetParameter("PageSize", SqlDbType.NVarChar, condition.PageSize,  ParameterDirection.Input)
                .SetParameter("InWhere", SqlDbType.NVarChar, condition.IN_WHERE, ParameterDirection.Input)
                .SetParameter("InSort", SqlDbType.NVarChar, condition. IN_SORT, ParameterDirection.Input)
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
        public ReturnResult<Document> GetAllDocument()
        {
            List<Document> documentList = new List<Document>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("PROFILE_GET_ALL", CommandType.StoredProcedure)
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
                .SetParameter("FolderID", SqlDbType.Int, document.FolderID, ParameterDirection.Input)
                .SetParameter("DocumentTitle", SqlDbType.NVarChar, document.DocumentTitle, ParameterDirection.Input)
                .SetParameter("DocumentNo", SqlDbType.NVarChar, document.DocumentNumber, ParameterDirection.Input)
                .SetParameter("DocumentOrderNum", SqlDbType.Float, document.DocumentOrderNum, ParameterDirection.Input)
                .SetParameter("NumberOfPapers", SqlDbType.Int, document.NumberOfPapers, ParameterDirection.Input)
                .SetParameter("DocumentContent", SqlDbType.NVarChar, document.DocumentContent, ParameterDirection.Input)
                .SetParameter("DocumentStatusId", SqlDbType.Int, document.DocumentStatusId, ParameterDirection.Input)
                .SetParameter("StartingDate", SqlDbType.Date, document.StartingDate, ParameterDirection.Input)
                .SetParameter("EndingDate", SqlDbType.Date, document.EndingDate, ParameterDirection.Input)
                .SetParameter("PreservationDate", SqlDbType.Date, document.PreservationDate, ParameterDirection.Input)
                .SetParameter("CreatedDate", SqlDbType.Date, document.CreatedDate, ParameterDirection.Input)
                .SetParameter("UpdatedDate", SqlDbType.Date, document.UpdatedDate, ParameterDirection.Input)
                .SetParameter("CreatedBy", SqlDbType.NVarChar, document.CreatedBy, ParameterDirection.Input)
                .SetParameter("TrustLevelId", SqlDbType.Int, document.TrustLevelId, ParameterDirection.Input)
                .SetParameter("FileId", SqlDbType.Int, document.FileId, ParameterDirection.Input)

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
            dbProvider.SetQuery("DOCUMENT_CREATE", CommandType.StoredProcedure)
                .SetParameter("DocumentId", SqlDbType.Int, document.DocumentId, ParameterDirection.Input)
                .SetParameter("FolderID", SqlDbType.Int, document.FolderID, ParameterDirection.Input)
                .SetParameter("DocumentTitle", SqlDbType.NVarChar, document.DocumentTitle, ParameterDirection.Input)
                .SetParameter("DocumentNo", SqlDbType.NVarChar, document.DocumentNumber, ParameterDirection.Input)
                .SetParameter("DocumentOrderNum", SqlDbType.Float, document.DocumentOrderNum, ParameterDirection.Input)
                .SetParameter("NumberOfPapers", SqlDbType.Int, document.NumberOfPapers, ParameterDirection.Input)
                .SetParameter("DocumentContent", SqlDbType.NVarChar, document.DocumentContent, ParameterDirection.Input)
                .SetParameter("DocumentStatusId", SqlDbType.Int, document.DocumentStatusId, ParameterDirection.Input)
                .SetParameter("StartingDate", SqlDbType.Date, document.StartingDate, ParameterDirection.Input)
                .SetParameter("EndingDate", SqlDbType.Date, document.EndingDate, ParameterDirection.Input)
                .SetParameter("PreservationDate", SqlDbType.Date, document.PreservationDate, ParameterDirection.Input)
                .SetParameter("UpdatedDate", SqlDbType.Date, document.UpdatedDate, ParameterDirection.Input)
                .SetParameter("UpdatedBy", SqlDbType.NVarChar, document.UpdatedBy, ParameterDirection.Input)
                .SetParameter("TrustLevelId", SqlDbType.Int, document.TrustLevelId, ParameterDirection.Input)
                .SetParameter("FileId", SqlDbType.Int, document.FileId, ParameterDirection.Input)

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
    }
}

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
            int totalRecords = 0;
            List<DocumentPaging> documentList = new List<DocumentPaging>();
            dbProvider.SetQuery("DOCUMENT_GET_PAGING", CommandType.StoredProcedure)
                .SetParameter("FromRecord", SqlDbType.NVarChar, condition.FromRecord,  ParameterDirection.Input)
                .SetParameter("PageSize", SqlDbType.NVarChar, condition.PageSize,  ParameterDirection.Input)
                .SetParameter("InWhere", SqlDbType.NVarChar, condition.IN_WHERE, ParameterDirection.Input)
                .SetParameter("InSort", SqlDbType.NVarChar, condition. IN_SORT, ParameterDirection.Input)
                .SetParameter("TotalRecords", System.Data.SqlDbType.Int, DBNull.Value, System.Data.ParameterDirection.Output)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<DocumentPaging>(out documentList)
                .Complete();
            dbProvider.GetOutValue("ErrorMessage", out outMessage)
                        .GetOutValue("TotalRecords", out totalRecords)
                       .GetOutValue("ErrorCode", out outCode);
                       

            return new ReturnResult<DocumentPaging>()
            {
                ItemList = documentList,
                TotalRows = totalRecords,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
            };
        }

        public ReturnResult<Document> GetDocumentPaging(BaseCondition<DocumentSearch> condition)
        {
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRecords = 0;
            List<Document> documentList = new List<Document>();
            dbProvider.SetQuery("DOCUMENT_GET_SEARCH", CommandType.StoredProcedure)
                .SetParameter("FromRecord", SqlDbType.NVarChar, condition.FromRecord, ParameterDirection.Input)
                .SetParameter("PageSize", SqlDbType.NVarChar, condition.PageSize, ParameterDirection.Input)
                .SetParameter("InWhere", SqlDbType.NVarChar, condition.IN_WHERE, ParameterDirection.Input)
                .SetParameter("InSort", SqlDbType.NVarChar, condition.IN_SORT, ParameterDirection.Input)
                .SetParameter("TotalRecords", System.Data.SqlDbType.Int, DBNull.Value, System.Data.ParameterDirection.Output)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<Document>(out documentList)
                .Complete();
            dbProvider.GetOutValue("ErrorMessage", out outMessage)
                       .GetOutValue("ErrorCode", out outCode)
                        .GetOutValue("TotalRecords", out totalRecords);


            return new ReturnResult<Document>()
            {
                ItemList = documentList,
                TotalRows = totalRecords,
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
            DbProvider dbProvider;
            string outCode = String.Empty;
            string outMessage = String.Empty;
            ReturnResult<Document> result = new ReturnResult<Document>();
            try
            {
                dbProvider = new DbProvider();
                dbProvider.SetQuery("DOCUMENT_CREATE", CommandType.StoredProcedure);
                dbProvider.SetParameter("DocumentCode", SqlDbType.NVarChar, document.DocumentCode, ParameterDirection.Input);
                dbProvider.SetParameter("DocOrdinal", SqlDbType.Int, document.DocOrdinal, ParameterDirection.Input);
                dbProvider.SetParameter("FileId", SqlDbType.Int, document.FileId, ParameterDirection.Input);
                dbProvider.SetParameter("DocTypeId", SqlDbType.Int, document.DocTypeId, ParameterDirection.Input);
                dbProvider.SetParameter("CodeNumber", SqlDbType.NVarChar, document.CodeNumber, ParameterDirection.Input);
                dbProvider.SetParameter("CodeNotation", SqlDbType.NVarChar, document.CodeNotation, ParameterDirection.Input);
                dbProvider.SetParameter("IssuedDate", SqlDbType.Date, document.IssuedDate, ParameterDirection.Input);
                dbProvider.SetParameter("Subject", SqlDbType.NVarChar, document.Subject, ParameterDirection.Input);
                dbProvider.SetParameter("LanguageId", SqlDbType.Int, document.LanguageId, ParameterDirection.Input);
                dbProvider.SetParameter("PageAmount", SqlDbType.Int, document.PageAmount, ParameterDirection.Input);
                dbProvider.SetParameter("Description", SqlDbType.NVarChar, document.Description, ParameterDirection.Input);
                dbProvider.SetParameter("InforSign", SqlDbType.NVarChar, document.InforSign, ParameterDirection.Input);
                dbProvider.SetParameter("Keyword", SqlDbType.NVarChar, document.Keyword, ParameterDirection.Input);
                dbProvider.SetParameter("Mode", SqlDbType.NVarChar, document.Mode, ParameterDirection.Input);
                dbProvider.SetParameter("ConfidenceLevelId", SqlDbType.Int, document.ConfidenceLevelId, ParameterDirection.Input);
                dbProvider.SetParameter("Autograph", SqlDbType.NVarChar, document.Autograph, ParameterDirection.Input);
                dbProvider.SetParameter("FormatId", SqlDbType.Int, document.FormatId, ParameterDirection.Input);
                dbProvider.SetParameter("ComputerFileId", SqlDbType.Int, document.ComputerFileId, ParameterDirection.Input);
                dbProvider.SetParameter("CreatedDate", SqlDbType.Date, document.CreatedDate, ParameterDirection.Input);
                dbProvider.SetParameter("CreatedBy", SqlDbType.NVarChar, document.CreatedBy, ParameterDirection.Input);
                dbProvider.SetParameter("Signature", SqlDbType.Int, document.Signature);
                dbProvider.SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output);
                dbProvider.SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output);
                dbProvider.ExcuteNonQuery();

                dbProvider.Complete();
                dbProvider.GetOutValue("ErrorCode", out outCode)
                           .GetOutValue("ErrorMessage", out outMessage);

                if (outCode != "0")
                {
                    result.Failed(outCode, outMessage);
                }
                else
                {
                    result.ErrorCode = "0";
                    result.ErrorMessage = "";
                }
            }
            catch (Exception ex)
            {
                result.Failed("-1", ex.Message);
            }
            return result;
        }

        public ReturnResult<Document> UpdateDocument(Document document)
        {
            DbProvider dbProvider;
            string outCode = String.Empty;
            string outMessage = String.Empty;
            ReturnResult<Document> result = new ReturnResult<Document>();
            try
            {
                dbProvider = new DbProvider();

                dbProvider.SetQuery("DOCUMENT_UPDATE", CommandType.StoredProcedure);
                    dbProvider.SetParameter("DocumentId", SqlDbType.Int, document.DocumentId, ParameterDirection.Input);
                    dbProvider.SetParameter("DocumentCode", SqlDbType.NVarChar, document.DocumentCode, ParameterDirection.Input);
                    dbProvider.SetParameter("DocOrdinal", SqlDbType.Int, document.DocOrdinal, ParameterDirection.Input);
                    dbProvider.SetParameter("FileId", SqlDbType.Int, document.FileId, ParameterDirection.Input);
                    dbProvider.SetParameter("DocTypeId", SqlDbType.Int, document.DocTypeId, ParameterDirection.Input);
                    dbProvider.SetParameter("CodeNumber", SqlDbType.NVarChar, document.CodeNumber, ParameterDirection.Input);
                    dbProvider.SetParameter("CodeNotation", SqlDbType.NVarChar, document.CodeNotation, ParameterDirection.Input);
                    dbProvider.SetParameter("IssuedDate", SqlDbType.Date, document.IssuedDate, ParameterDirection.Input);
                    dbProvider.SetParameter("Subject", SqlDbType.NVarChar, document.Subject, ParameterDirection.Input);
                    dbProvider.SetParameter("LanguageId", SqlDbType.Int, document.LanguageId, ParameterDirection.Input);
                    dbProvider.SetParameter("PageAmount", SqlDbType.Int, document.PageAmount, ParameterDirection.Input);
                    dbProvider.SetParameter("Description", SqlDbType.NVarChar, document.Description, ParameterDirection.Input);
                    dbProvider.SetParameter("InforSign", SqlDbType.NVarChar, document.InforSign, ParameterDirection.Input);
                    dbProvider.SetParameter("Keyword", SqlDbType.NVarChar, document.Keyword, ParameterDirection.Input);
                    dbProvider.SetParameter("Mode", SqlDbType.NVarChar, document.Mode, ParameterDirection.Input);
                    dbProvider.SetParameter("ConfidenceLevelId", SqlDbType.Int, document.ConfidenceLevelId, ParameterDirection.Input);
                    dbProvider.SetParameter("Autograph", SqlDbType.NVarChar, document.Autograph, ParameterDirection.Input);
                    dbProvider.SetParameter("FormatId", SqlDbType.Int, document.FormatId, ParameterDirection.Input);
                    dbProvider.SetParameter("ComputerFileId", SqlDbType.Int, document.ComputerFileId, ParameterDirection.Input);
                    //dbProvider.SetParameter("CreatedDate", SqlDbType.Date, document.CreatedDate, ParameterDirection.Input);
                    //dbProvider.SetParameter("UpdatedDate", SqlDbType.Date, document.UpdatedDate, ParameterDirection.Input);
                    dbProvider.SetParameter("UpdatedBy", SqlDbType.NVarChar, document.UpdatedBy, ParameterDirection.Input);
                    //dbProvider.SetParameter("IsDeleted", SqlDbType.NVarChar, document.IsDeleted, ParameterDirection.Input);
                    dbProvider.SetParameter("Signature", SqlDbType.TinyInt, document.Signature, ParameterDirection.Input);
                    dbProvider.SetParameter("Confirmed", SqlDbType.Int, document.Confirmed, ParameterDirection.Input);
                    dbProvider.SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output);
                    dbProvider.SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output);
                    dbProvider.ExcuteNonQuery();
                    dbProvider.Complete();

                dbProvider.GetOutValue("ErrorCode", out outCode);
                dbProvider.GetOutValue("ErrorMessage", out outMessage);

                if (outCode != "0")
                {
                    result.Failed(outCode, outMessage);
                }
                else
                {
                    result.ErrorCode = "0";
                    result.ErrorMessage = "";
                }
            }
            catch (Exception ex)
            {
                result.Failed("-1", ex.Message);
            }
            return result;
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

        public ReturnResult<Document> GetListByProfileId(Profiles profile) {
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
        public ReturnResult<DocumentDetail> GetDocumentById(int documentId)
        {
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            var document = new DocumentDetail();
            dbProvider.SetQuery("DOCUMENT_GET_BY_ID", CommandType.StoredProcedure)
                .SetParameter("DocumentId", SqlDbType.Int, documentId, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetSingle<DocumentDetail>(out document)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<DocumentDetail>()
            {
                Item = document,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
            };
        }
        
    }
}

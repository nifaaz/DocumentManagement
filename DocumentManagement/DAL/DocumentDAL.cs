//using DocumentManagement.Common;
//using DocumentManagement.Model;
//using DocumentManagement.Models.Entity.Document;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Threading.Tasks;

//namespace DocumentManagement.DAL
//{
//    public class DocumentDAL
//    {
//        public ReturnResult<Document> GetAllDocument()
//        {
//            List<Document> documentList = new List<Document>();
//            DbProvider dbProvider = new DbProvider();
//            string outCode = String.Empty;
//            string outMessage = String.Empty;
//            int totalRows = 0;
//            dbProvider.SetQuery("PROFILE_GET_ALL", CommandType.StoredProcedure)
//                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
//                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 255, ParameterDirection.Output)
//                .GetList<Document>(out documentList)
//                .Complete();
//            dbProvider.GetOutValue("ErrorCode", out outCode)
//                       .GetOutValue("ErrorMessage", out outMessage);

//            return new ReturnResult<Document>()
//            {
//                ItemList = documentList,
//                ErrorCode = outCode,
//                ErrorMessage = outMessage,
//                TotalRows = totalRows
//            };
//        }

//        public ReturnResult<Document> CreateDocument(Document document)
//        {
//            DbProvider dbProvider = new DbProvider();
//            string outCode = String.Empty;
//            string outMessage = String.Empty;
//            dbProvider.SetQuery("DOCUMENT_CREATE", CommandType.StoredProcedure)
//                .SetParameter("GearBoxId", SqlDbType.Int, document.GearBoxId, ParameterDirection.Input)
//                .SetParameter("DocumentTitle", SqlDbType.NVarChar, document.DocumentTitle, 255, ParameterDirection.Input)
//                .SetParameter("DocumentName", SqlDbType.NVarChar, document.DocumentName, 255, ParameterDirection.Input)
//                .SetParameter("NumberOfDocs", SqlDbType.Int, document.NumberOfDocs, ParameterDirection.Input)
//                .SetParameter("CreatedDate", SqlDbType.DateTime, document.CreatedDate, ParameterDirection.Input)
//                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
//                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
//                .ExcuteNonQuery()
//                .Complete();
//            dbProvider.GetOutValue("ErrorCode", out outCode)
//                       .GetOutValue("ErrorMessage", out outMessage);

//            return new ReturnResult<Document>()
//            {
//                ErrorCode = outCode,
//                ErrorMessage = outMessage,
//            };
//        }

//        public ReturnResult<Document> UpdateDocument(Document document)
//        {
//            DbProvider dbProvider = new DbProvider();
//            string outCode = String.Empty;
//            string outMessage = String.Empty;
//            dbProvider.SetQuery("DOCUMENT_UPDATE", CommandType.StoredProcedure)
//                .SetParameter("DocumentId", SqlDbType.Int, document.DocumentId, ParameterDirection.Input)
//                .SetParameter("GearBoxId", SqlDbType.Int, document.GearBoxId, ParameterDirection.Input)
//                .SetParameter("DocumentTitle", SqlDbType.Int, document.DocumentTitle, ParameterDirection.Input)
//                .SetParameter("DocumentName", SqlDbType.Int, document.DocumentName, ParameterDirection.Input)
//                .SetParameter("NumberOfDocs", SqlDbType.Int, document.NumberOfDocs, ParameterDirection.Input)
//                .SetParameter("CreatedDate", SqlDbType.DateTime, document.CreatedDate, ParameterDirection.Input)
//                .SetParameter("UpdatedDate", SqlDbType.DateTime, document.UpdatedDate, ParameterDirection.Input)
//                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
//                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
//                .ExcuteNonQuery()
//                .Complete();
//            dbProvider.GetOutValue("ErrorCode", out outCode)
//                       .GetOutValue("ErrorMessage", out outMessage);

//            return new ReturnResult<Document>()
//            {
//                ErrorCode = outCode,
//                ErrorMessage = outMessage,
//            };
//        }

//        public ReturnResult<Document> DeleteDocument(Document document)
//        {
//            DbProvider dbProvider = new DbProvider();
//            string outCode = String.Empty;
//            string outMessage = String.Empty;
//            dbProvider.SetQuery("DOCUMENT_DELETE", CommandType.StoredProcedure)
//                .SetParameter("DocumentId", SqlDbType.Int, document.DocumentId, ParameterDirection.Input)
//                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
//                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
//                .ExcuteNonQuery()
//                .Complete();
//            dbProvider.GetOutValue("ErrorCode", out outCode)
//                       .GetOutValue("ErrorMessage", out outMessage);

//            return new ReturnResult<Document>()
//            {
//                ErrorCode = outCode,
//                ErrorMessage = outMessage,
//            };
//        }
//    }
//}

using Common.Common;
using DocumentManagement.Common;
using DocumentManagement.Model;
using DocumentManagement.Models.Entity;
using DocumentManagement.Models.Entity.ComputerFile;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.DAL
{
    public class DigitalSignatureDAL
    {
        private DigitalSignatureDAL() { }

        private static volatile DigitalSignatureDAL _instance;

        static readonly object key = new object();

        public static DigitalSignatureDAL GetDigitalSignatureDALInstance
        {
            get
            {
                lock (key)
                {
                    if (_instance == null)
                    {
                        _instance = new DigitalSignatureDAL();
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
            DbProvider provider;
            List<DigitalSignature> list;
            string outCode = String.Empty;
            string outMessage = String.Empty;
            string totalRecords = String.Empty;
            var result = new ReturnResult<DigitalSignature>();
            try
            {
                provider = new DbProvider();
                list = new List<DigitalSignature>();
                provider.SetQuery("SIGNATURES_GET_PAGING", CommandType.StoredProcedure)
                    .SetParameter("InWhere", SqlDbType.NVarChar, condition.IN_WHERE ?? String.Empty)
                    .SetParameter("InSort", SqlDbType.NVarChar, condition.IN_SORT ?? String.Empty)
                    .SetParameter("StartRow", SqlDbType.Int, condition.PageIndex)
                    .SetParameter("PageSize", SqlDbType.Int, condition.PageSize)
                    .SetParameter("TotalRecords", SqlDbType.Int, DBNull.Value, ParameterDirection.Output)
                    .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                    .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                    .GetList<DigitalSignature>(out list)
                    .Complete();

                if (list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        item.Size = (Math.Round((double)(int.Parse(item.Size) / 1024.0), 4)).ToString() + " KB";
                    }
                    result.ItemList = list;
                }
                provider.GetOutValue("ErrorCode", out outCode)
                           .GetOutValue("ErrorMessage", out outMessage)
                           .GetOutValue("TotalRecords", out string totalRows);

                if (outCode != "0")
                {
                    result.Failed(outCode, outMessage);
                }
                else
                {
                    result.ErrorCode = "";
                    result.ErrorMessage = "";
                    result.TotalRows = int.Parse(totalRows);
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public ReturnResult<DigitalSignature> Create (DigitalSignature digital, int overwrite = 0)
        {
            DbProvider db;
            ReturnResult<DigitalSignature> result = new ReturnResult<DigitalSignature>();
            try
            {
                db = new DbProvider();
                db.SetQuery("SIGNATURES_CREATE", CommandType.StoredProcedure)
                    .SetParameter("FileName", SqlDbType.NVarChar, digital.FileName, 200)
                    .SetParameter("Size", SqlDbType.VarChar, digital.Size, 10)
                    .SetParameter("Path", SqlDbType.NVarChar, digital.Path, 500)
                    .SetParameter("CreateBy", SqlDbType.NVarChar, digital.CreateBy, 50)
                    .SetParameter("ServerPath", SqlDbType.NVarChar, digital.ServerPath, 1000)
                    .SetParameter("Overwrite", SqlDbType.TinyInt, overwrite)
                    .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                    .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                    .ExcuteNonQuery()
                    .Complete();
                db.GetOutValue("ErrorCode", out string outCode)
                    .GetOutValue("ErrorMessage", out string outMessage);
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
        
        /// <summary>
        /// xóa chữ ký số
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ReturnResult<DigitalSignature> Delete (int id)
        {
            ReturnResult<DigitalSignature> result = new ReturnResult<DigitalSignature>();
            DbProvider db;
            try
            {
                db = new DbProvider();
                db.SetQuery("SIGNATURES_DELETE", CommandType.StoredProcedure)
                    .SetParameter("Id", SqlDbType.Int, id)
                    .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                    .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                    .ExcuteNonQuery()
                    .Complete();
                db.GetOutValue("ErrorCode", out string outCode)
                    .GetOutValue("ErrorMessage", out string outMessage);
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

        public ReturnResult<DigitalSignature> GetById(int id)
        {
            ReturnResult<DigitalSignature> result = new ReturnResult<DigitalSignature>();
            DbProvider db;
            DigitalSignature digital;
            try
            {
                digital = new DigitalSignature();
                db = new DbProvider();
                db.SetQuery("SIGNATURES_GET_BY_ID", CommandType.StoredProcedure)
                    .SetParameter("Id", SqlDbType.Int, id)
                    .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                    .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                    .GetSingle<DigitalSignature>(out digital)
                    .Complete();

                db.GetOutValue("ErrorCode", out string outCode)
                    .GetOutValue("ErrorMessage", out string outMessage);
                if (outCode != "0")
                {
                    result.Failed(outCode, outMessage);
                }
                else
                {
                    result.Item = digital;
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

        public ReturnResult<DigitalSignature> GetAll ()
        {
            ReturnResult<DigitalSignature> result = new ReturnResult<DigitalSignature>();
            DbProvider db;
            List<DigitalSignature> digitals;
            try
            {
                digitals = new List<DigitalSignature>();
                db = new DbProvider();
                db.SetQuery("SIGNATURES_GET_ALL", CommandType.StoredProcedure)
                    .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                    .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                    .GetList<DigitalSignature>(out digitals)
                    .Complete();

                db.GetOutValue("ErrorCode", out string outCode)
                    .GetOutValue("ErrorMessage", out string outMessage);
                if (outCode != "0")
                {
                    result.Failed(outCode, outMessage);
                }
                else
                {
                    result.ItemList = digitals;
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

        public ReturnResult<DigitalSignature> ChangeStatus (int id, int status = 0)
        {
            ReturnResult<DigitalSignature> result = new ReturnResult<DigitalSignature>();
            DbProvider db;
            try
            {
                db = new DbProvider();
                db.SetQuery("SIGNATURES_UPDATE_STATUS", CommandType.StoredProcedure)
                    .SetParameter("Id", SqlDbType.Int, id)
                    .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                    .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                    .ExcuteNonQuery()
                    .Complete();

                db.GetOutValue("ErrorCode", out string outCode)
                    .GetOutValue("ErrorMessage", out string outMessage);
                if (outCode != "0")
                {
                    result.Failed(outCode, outMessage);
                }
                else
                {
                    result.Item = null;
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
    }
}

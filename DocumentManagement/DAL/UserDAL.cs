using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Common;
using DocumentManagement.Common;
using DocumentManagement.Models.Entity.User;
using DocumentManagement.DAL;
using DocumentManagement.Model;
using System.Data;
using DocumentManagement.BUS;
using DocumentManagement.Services;
using DocumentManagement.Models.DTO;

namespace DocumentManagement.DAL
{
    public class UserDAL
    {
        private UserDAL() { }
        private static volatile UserDAL _instance;
        static object key = new object();

        public static UserDAL GetUserDALInstance
        {
            get
            {
                lock (key)
                {
                    if (_instance == null)
                    {
                        _instance = new UserDAL();
                    }
                }

                return _instance;
            }
            
            private set
            {
                _instance = value;
            }
        }

        public async Task<ReturnResult<User>> GetSearchUserWithPaging (BaseCondition<User> condition)
        {
            DbProvider provider = new DbProvider();
            List<User> list = new List<User>();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            string totalRecords = String.Empty;
            var result = new ReturnResult<User>();
            try
            {
                provider.SetQuery("USER_GET_SEARCH_WITH_PAGING", System.Data.CommandType.StoredProcedure)
                    .SetParameter("InWhere", System.Data.SqlDbType.NVarChar, condition.IN_WHERE ?? String.Empty)
                    .SetParameter("InSort", System.Data.SqlDbType.NVarChar, condition.IN_SORT ?? String.Empty)
                    .SetParameter("StartRow", System.Data.SqlDbType.Int, condition.PageIndex)
                    .SetParameter("PageSize", System.Data.SqlDbType.Int, condition.PageSize)
                    .SetParameter("TotalRecords", System.Data.SqlDbType.Int, DBNull.Value, System.Data.ParameterDirection.Output)
                    .SetParameter("ErrorCode", System.Data.SqlDbType.NVarChar, DBNull.Value, 100, System.Data.ParameterDirection.Output)
                    .SetParameter("ErrorMessage", System.Data.SqlDbType.NVarChar, DBNull.Value, 4000, System.Data.ParameterDirection.Output).GetList<User>(out list).Complete();

                if (list.Count > 0)
                {
                    result.ItemList = list;
                }
                provider.GetOutValue("ErrorCode", out outCode)
                           .GetOutValue("ErrorMessage", out outMessage)
                           .GetOutValue("TotalRecords", out string totalRows);

                if (outCode != "0")
                {
                    result.ErrorCode = outCode;
                    result.ErrorMessage = outMessage;
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

        public ReturnResult<User> GetUserByID(int id)
        {
            var result = new ReturnResult<User>();
            User item = new User();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            try
            {
                dbProvider.SetQuery("USER_GET_BY_ID", CommandType.StoredProcedure)
               .SetParameter("UserID", SqlDbType.Int, id, ParameterDirection.Input)
               .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
               .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
               .GetSingle<User>(out item)
               .Complete();

            }
            catch (Exception ex)
            {

                throw;
            }
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<User>()
            {
                Item = item,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }

        public ReturnResult<User> CreateUser(User user)
        {
            AuthenticationHelper _authenticationHelper = new AuthenticationHelper();
            string passwordSalt = _authenticationHelper.RamdomString(5);
            string password = _authenticationHelper.GetMd5Hash(passwordSalt + user.PasswordNew);
            user.Password = password;
            user.PasswordSalt = passwordSalt;
            DbProvider provider = new DbProvider();
            var result = new ReturnResult<User>();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            string totalRecords = String.Empty;
            try
            {
                provider.SetQuery("USER_CREATE", System.Data.CommandType.StoredProcedure)
                    .SetParameter("UserName", SqlDbType.NVarChar, user.UserName, 50, ParameterDirection.Input)
                    .SetParameter("Password", SqlDbType.NVarChar, user.Password, 50, ParameterDirection.Input)
                    .SetParameter("PasswordSalt", SqlDbType.NVarChar, user.PasswordSalt, 50, ParameterDirection.Input)
                    .SetParameter("NguoiTao", SqlDbType.NVarChar, user.CreateBy, 50, ParameterDirection.Input)
                    .SetParameter("NgayTao", SqlDbType.NVarChar, user.CreateDate.ToString(), 100, ParameterDirection.Input)
                    .SetParameter("Status", SqlDbType.Int, user.Status, ParameterDirection.Input)
                    .SetParameter("RoleID", SqlDbType.Int, user.RoleID, ParameterDirection.Input)
                    .SetParameter("ErrorCode", System.Data.SqlDbType.NVarChar, DBNull.Value, 100, System.Data.ParameterDirection.Output)
                    .SetParameter("ErrorMessage", System.Data.SqlDbType.NVarChar, DBNull.Value, 4000, System.Data.ParameterDirection.Output)
                    .GetSingle<User>(out user).Complete();

                provider.GetOutValue("ErrorCode", out outCode)
                          .GetOutValue("ErrorMessage", out outMessage);

                if (outCode != "0" || outCode == "")
                {
                    result.ErrorCode = outCode;
                    result.ErrorMessage = outMessage;
                }
                else
                {
                    result.Item = user;
                    result.ErrorCode = outCode;
                    result.ErrorMessage = outMessage;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public ReturnResult<User> EditUser(User user)
        {
            AuthenticationHelper _authenticationHelper = new AuthenticationHelper();
            if (user.PasswordNew != null && !user.PasswordNew.Equals(""))
            {
                string passwordSalt = _authenticationHelper.RamdomString(5);
                string password = _authenticationHelper.GetMd5Hash(passwordSalt + user.PasswordNew);
                user.Password = password;
                user.PasswordSalt = passwordSalt;
            }
            else
            {
                var accountBusiness = new AccountBUS();
                var userDTO = accountBusiness.GetUserToCheck(user.Id);
                user.Password = userDTO.Password;
                user.PasswordSalt = userDTO.PasswordSalt;
            }

            ReturnResult<User> result;
            DbProvider db;
            try
            {
                result = new ReturnResult<User>();
                db = new DbProvider();
                db.SetQuery("USER_EDIT", CommandType.StoredProcedure)
                    .SetParameter("UserID", SqlDbType.Int, user.Id, ParameterDirection.Input)
                    .SetParameter("UserName", SqlDbType.NVarChar, user.UserName, 50, ParameterDirection.Input)
                    .SetParameter("Password", SqlDbType.NVarChar, user.Password, 50, ParameterDirection.Input)
                    .SetParameter("PasswordSalt", SqlDbType.NVarChar, user.PasswordSalt, 50, ParameterDirection.Input)
                    .SetParameter("NguoiCapNhat", SqlDbType.NVarChar, user.UpdatedBy, 50, ParameterDirection.Input)
                    .SetParameter("NgayCapNhat", SqlDbType.NVarChar, user.UpdatedDate.ToString(), 100, ParameterDirection.Input)
                    .SetParameter("status", SqlDbType.Int, user.Status, ParameterDirection.Input)
                    .SetParameter("RoleID", SqlDbType.Int, user.RoleID, ParameterDirection.Input)
                    .SetParameter("ErrorCode", SqlDbType.Int, DBNull.Value, ParameterDirection.Output)
                    .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                    .ExcuteNonQuery()
                    .Complete();
                db.GetOutValue("ErrorCode", out string errorCode)
                    .GetOutValue("ErrorMessage", out string errorMessage);
                if (errorCode.ToString() == "0")
                {
                    result.ErrorCode = "0";
                    result.ErrorMessage = "";
                }
                else
                {
                    result.Failed(errorCode, errorMessage);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public ReturnResult<User> DeleteUser(int id)
        {
            DbProvider provider = new DbProvider();
            var result = new ReturnResult<User>();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            string totalRecords = String.Empty;
            User item = new User();
            try
            {
                provider.SetQuery("USER_DELETE", CommandType.StoredProcedure)
                     .SetParameter("UserID", SqlDbType.Int, id, ParameterDirection.Input)
                    .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, System.Data.ParameterDirection.Output)
                    .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, System.Data.ParameterDirection.Output)
                    .ExcuteNonQuery().Complete();

                provider.GetOutValue("ErrorCode", out outCode)
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
                throw ex;
            }

            return result;
        }

        public ReturnResult<UserSelect2DTO> GetAllUser()
        {
            List<UserSelect2DTO> users = new List<UserSelect2DTO>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            try
            {
                dbProvider.SetQuery("USER_GET_ALL", CommandType.StoredProcedure)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<UserSelect2DTO>(out users)
                .Complete();
            }
            catch (Exception)
            {

                throw;
            }
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<UserSelect2DTO>()
            {
                ItemList = users,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }

    }
}

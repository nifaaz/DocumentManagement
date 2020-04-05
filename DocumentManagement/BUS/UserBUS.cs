using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Common;
using DocumentManagement.Common;
using DocumentManagement.DAL;
using DocumentManagement.Models.DTO;
using DocumentManagement.Models.Entity.User;

namespace DocumentManagement.BUS
{
    public class UserBUS
    {
        UserDAL userDAL = UserDAL.GetUserDALInstance;
        private UserBUS() { }

        private static volatile UserBUS _instance;
        static object key = new object();
        public static UserBUS GetUserBUSInstance
        {
            get
            {
                lock (key)
                {
                    if (_instance == null)
                    {
                        _instance = new UserBUS();
                    }
                }

                return _instance;
            }

            private set
            {
                _instance = value;
            }

        }

        public ReturnResult<User> UserGetSearchWithPaging(BaseCondition<User> condition)
        {
            return userDAL.GetSearchUserWithPaging(condition);
        }

        public ReturnResult<User> GetUserByID (int id)
        {
            var rs = userDAL.GetUserByID(id);
            return rs;
        }

        public ReturnResult<User> CreateUser(User user)
        {
            return userDAL.CreateUser(user);
        }

        public ReturnResult<User> DeleteUser(int id)
        {
            return userDAL.DeleteUser(id);
        }

        public ReturnResult<User> EditUser(User user)
        {
            return userDAL.EditUser(user);
        }

        public ReturnResult<UserSelect2DTO> GetAllUser()
        {
            var result = userDAL.GetAllUser();
            return result;
        }
    }
}

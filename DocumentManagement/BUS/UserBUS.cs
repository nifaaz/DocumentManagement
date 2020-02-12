using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Common;
using DocumentManagement.Common;
using DocumentManagement.DAL;
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

    }
}

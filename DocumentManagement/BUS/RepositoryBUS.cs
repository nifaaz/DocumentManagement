using Common.Common;
using DocumentManagement.Common;
using DocumentManagement.DAL;
using DocumentManagement.Model.Entity.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.BUS
{
    public class RepositoryBUS
    {
        private static RepositoryDAL repositoryDAL = RepositoryDAL.GetRepositoryDALInstance;
        private RepositoryBUS() { }

        private static volatile RepositoryBUS _instance;

        static object key = new object();

        public static RepositoryBUS GetRepositoryBUSInstance
        {
            get
            {
                lock (key)
                {
                    if (_instance == null)
                    {
                        _instance = new RepositoryBUS();
                    }
                }

                return _instance;
            }

            private set
            {
                _instance = value;
            }
        }
        public ReturnResult<Repository> GetPagingWithSearchResults(BaseCondition<Repository> condition)
        {
            var result = repositoryDAL.GetPagingWithSearchResults(condition);
            return result;
        }
        public ReturnResult<Repository> GetAllRepository()
        {
            var result = repositoryDAL.GetAllRepository();
            return result;
        }

        public ReturnResult<Repository> CreateRepository(Repository repository)
        {
            var result = repositoryDAL.CreateRepository(repository);
            return result;
        }
        public ReturnResult<Repository> DeleteRepository(int repositoryId)
        {
            var result = repositoryDAL.DeleteRepository(repositoryId);
            return result;
        }
        public ReturnResult<Repository> UpdateRepository(Repository repository)
        {
            var result = repositoryDAL.UpdateRepository(repository);
            return result;
        }
    }
}

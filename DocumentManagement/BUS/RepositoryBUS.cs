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
        private RepositoryDAL _repositoryDAL;
        private RepositoryDAL RepositoryDAL
        {
            get
            {
                _repositoryDAL = new RepositoryDAL();
                return _repositoryDAL;
            }
        }
        public ReturnResult<Repository> GetAllRepository()
        {
            var result = RepositoryDAL.GetAllRepository();
            return result;
        }

        public ReturnResult<Repository> CreateRepository(Repository repository)
        {
            var result = RepositoryDAL.CreateRepository(repository);
            return result;
        }
        public ReturnResult<Repository> DeleteRepository(int repositoryId)
        {
            var result = RepositoryDAL.DeleteRepository(repositoryId);
            return result;
        }
        public ReturnResult<Repository> UpdateRepository(Repository repository)
        {
            var result = RepositoryDAL.UpdateRepository(repository);
            return result;
        }
    }
}

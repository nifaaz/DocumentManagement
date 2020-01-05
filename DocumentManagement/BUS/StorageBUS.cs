using DocumentManagement.Common;
using DocumentManagement.DAL;
using DocumentManagement.Model.Entity.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.BUS
{
    public class StorageBUS
    {
        private StorageDAL _StorageDAL;
        private StorageDAL StorageDAL
        {
            get
            {
                _StorageDAL = new StorageDAL();
                return _StorageDAL;
            }
        }
        public ReturnResult<Storage> GetAllStorage()
        {
            var result = StorageDAL.GetAllStorage();
            return result;
        }
        //public ReturnResult<Storage> StorageSearch(string serachStr)
        //{
        //    var result = StorageDAL.S(serachStr);
        //    return result;
        //}
        public ReturnResult<Storage> GetStorageByID(int storageID)
        {
            var result = StorageDAL.GetStorageByID(storageID);
            return result;
        }
        public ReturnResult<Storage> GetStorageByFontID(int fontID)
        {
            var result = StorageDAL.GetStorageByFontID(fontID);
            return result;
        }
        public ReturnResult<Storage> GetStorageByRepoID(int frepoID)
        {
            var result = StorageDAL.GetStorageByRepoID(frepoID);
            return result;
        }
        public ReturnResult<Storage> CreateStorage(Storage Storage)
        {
            var result = StorageDAL.CreateStorage(Storage);
            return result;
        }
        public ReturnResult<Storage> DeleteStorage(int StorageId)
        {
            var result = StorageDAL.DeleteStorage(StorageId);
            return result;
        }
        public ReturnResult<Storage> UpdateStorage(Storage Storage)
        {
            var result = StorageDAL.UpdateStorage(Storage);
            return result;
        }
    }
}

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
        private static StorageDAL storageDAL = StorageDAL.GetStorageDALInstance;
        private StorageBUS() { }

        private static volatile StorageBUS _instance;

        static object key = new object();

        public static StorageBUS GetStorageBUSInstance
        {
            get
            {
                lock (key)
                {
                    if (_instance == null)
                    {
                        _instance = new StorageBUS();
                    }
                }

                return _instance;
            }

            private set
            {
                _instance = value;
            }
        }
        public ReturnResult<Storage> GetAllStorage()
        {
            var result = storageDAL.GetAllStorage();
            return result;
        }
        //public ReturnResult<Storage> StorageSearch(string serachStr)
        //{
        //    var result = StorageDAL.S(serachStr);
        //    return result;
        //}
        public ReturnResult<Storage> GetStorageByID(int storageID)
        {
            var result = storageDAL.GetStorageByID(storageID);
            return result;
        }
        public ReturnResult<Storage> GetStorageByFontID(int fontID)
        {
            var result = storageDAL.GetStorageByFontID(fontID);
            return result;
        }
        public ReturnResult<Storage> GetStorageByRepoID(int frepoID)
        {
            var result = storageDAL.GetStorageByRepoID(frepoID);
            return result;
        }
        public ReturnResult<Storage> CreateStorage(Storage Storage)
        {
            var result = storageDAL.CreateStorage(Storage);
            return result;
        }
        public ReturnResult<Storage> DeleteStorage(int StorageId)
        {
            var result = storageDAL.DeleteStorage(StorageId);
            return result;
        }
        public ReturnResult<Storage> UpdateStorage(Storage Storage)
        {
            var result = storageDAL.UpdateStorage(Storage);
            return result;
        }
    }
}

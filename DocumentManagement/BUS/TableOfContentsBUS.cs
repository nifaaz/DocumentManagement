using DocumentManagement.Common;
using DocumentManagement.DAL;
using DocumentManagement.Model.Entity.TableOfContens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.BUS
{
    public class TableOfContentsBUS
    {
        private static TableOfContentsDAL tableOfContentsDAL = TableOfContentsDAL.GetTableOfContentsDALInstance;
        private TableOfContentsBUS() { }

        private static volatile TableOfContentsBUS _instance;

        static object key = new object();

        public static TableOfContentsBUS GetTableOfContentsBUSInstance
        {
            get
            {
                lock (key)
                {
                    if (_instance == null)
                    {
                        _instance = new TableOfContentsBUS();
                    }
                }

                return _instance;
            }

            private set
            {
                _instance = value;
            }
        }
        public ReturnResult<TableOfContents> GetAllTableOfContents()
        {
            var result = tableOfContentsDAL.GetAllTableOfContents();
            return result;
        }
        public ReturnResult<TableOfContents> GetTableOfContentsByID(int TableOfContentsID)
        {
            var rs = tableOfContentsDAL.GetTableOfContentsByID(TableOfContentsID);
            return rs;
        }
        public ReturnResult<TableOfContents> GetTableOfContentsByStorageID(int storageID)
        {
            var rs = tableOfContentsDAL.GetTableOfContentsByStorageID(storageID);
            return rs;
        }
        public ReturnResult<TableOfContents> GetTableOfContentsByFontID(int fontID)
        {
            var rs = tableOfContentsDAL.GetTableOfContentsByFontID(fontID);
            return rs;
        }
        public ReturnResult<TableOfContents> GetTableOfContentsByRepositoryID(int repositoryID)
        {
            var rs = tableOfContentsDAL.GetTableOfContentsByRepositoryID(repositoryID);
            return rs;
        }
        public ReturnResult<TableOfContents> TableOfContentsSearch(string searchStr)
        {
            var rs = tableOfContentsDAL.TableOfContentsSearch(searchStr);
            return rs;
        }
        public ReturnResult<TableOfContents> DeleteTableOfContents(int TableOfContentsID)
        {
            var rs = tableOfContentsDAL.DeleteTableOfContents(TableOfContentsID);
            return rs;
        }
        public ReturnResult<TableOfContents> UpdateTableOfContents(TableOfContents TableOfContents)
        {
            var rs = tableOfContentsDAL.UpdateTableOfContents(TableOfContents);
            return rs;
        }
        public ReturnResult<TableOfContents> InsertTableOfContents(TableOfContents TableOfContents)
        {
            var rs = tableOfContentsDAL.InsertTableOfContents(TableOfContents);
            return rs;
        }
    }
}

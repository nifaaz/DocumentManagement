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
        private TableOfContentsDAL _TableOfContentsDAL;
        private TableOfContentsDAL TableOfContentsDAL
        {
            get
            {
                _TableOfContentsDAL = new TableOfContentsDAL();
                return _TableOfContentsDAL;
            }
        }
        public ReturnResult<TableOfContents> GetAllTableOfContents()
        {
            var result = TableOfContentsDAL.GetAllTableOfContents();
            return result;
        }
        public ReturnResult<TableOfContents> GetTableOfContentsByID(int TableOfContentsID)
        {
            var rs = TableOfContentsDAL.GetTableOfContentsByID(TableOfContentsID);
            return rs;
        }
        public ReturnResult<TableOfContents> GetTableOfContentsByStorageID(int storageID)
        {
            var rs = TableOfContentsDAL.GetTableOfContentsByStorageID(storageID);
            return rs;
        }
        public ReturnResult<TableOfContents> GetTableOfContentsByFontID(int fontID)
        {
            var rs = TableOfContentsDAL.GetTableOfContentsByFontID(fontID);
            return rs;
        }
        public ReturnResult<TableOfContents> GetTableOfContentsByRepositoryID(int repositoryID)
        {
            var rs = TableOfContentsDAL.GetTableOfContentsByRepositoryID(repositoryID);
            return rs;
        }
        public ReturnResult<TableOfContents> TableOfContentsSearch(string searchStr)
        {
            var rs = TableOfContentsDAL.TableOfContentsSearch(searchStr);
            return rs;
        }
        public ReturnResult<TableOfContents> DeleteTableOfContents(int TableOfContentsID)
        {
            var rs = TableOfContentsDAL.DeleteTableOfContents(TableOfContentsID);
            return rs;
        }
        public ReturnResult<TableOfContents> UpdateTableOfContents(TableOfContents TableOfContents)
        {
            var rs = TableOfContentsDAL.UpdateTableOfContents(TableOfContents);
            return rs;
        }
        public ReturnResult<TableOfContents> InsertTableOfContents(TableOfContents TableOfContents)
        {
            var rs = TableOfContentsDAL.InsertTableOfContents(TableOfContents);
            return rs;
        }
    }
}

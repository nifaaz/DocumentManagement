using DocumentManagement.Common;
using DocumentManagement.Models.Entity.Language;
using LanguageManagement.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageManagement.BUS
{
    public class LanguageBUS
    {
        private LanguageDAL _languageDAL;
        private LanguageDAL LanguageDAL
        {
            get
            {
                _languageDAL = new LanguageDAL();
                return _languageDAL;
            }
        }
        public ReturnResult<Language> GetAllLanguage()
        {
            var result = LanguageDAL.GetAllLanguage();
            return result;
        }
    }
}

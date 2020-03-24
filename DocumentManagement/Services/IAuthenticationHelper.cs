using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Services
{
    public interface IAuthenticationHelper
    {
        string GetMd5Hash(string input);
        string RamdomString(int codeCount);
        string GenerateUniqueCode(string prefix = "");
    }
}

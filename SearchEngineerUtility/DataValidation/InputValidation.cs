using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SearchEngineerUtility.DataValidation
{
    public  static class InputValidation
    {
        public static bool KeywordValidation(string keyword)
        {
            var validation = new Regex(@"^[a-zA-Z]{1,15}$");

            return validation.IsMatch(keyword);
        }
    }
}

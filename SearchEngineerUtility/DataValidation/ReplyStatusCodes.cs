using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngineerUtility.DataValidation
{
    public enum ReplyStatusCodes
    {
        Invalid_Data = 422,
        Add_Failed = 423,
        Delete_Failed = 424,

        Internal_Serice_Error = 500,

        No_Content = 204,
        Ok = 200,
    }
}

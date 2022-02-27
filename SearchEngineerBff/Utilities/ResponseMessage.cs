using SearchEngineerUtility.DataValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngineerBackendForFrontend.Utilities
{
    public class ResponseMessage
    {
        private Dictionary<int, string> responseMessageList = new Dictionary<int, string>
        {
            {(int)ReplyStatusCodes.Ok,"Success" },
            {(int)ReplyStatusCodes.No_Content,"No resource" },
            {(int)ReplyStatusCodes.Invalid_Data,"Invalid data" },
            {(int)ReplyStatusCodes.Add_Failed,"Add failed" },
            {(int)ReplyStatusCodes.Delete_Failed,"Delete failed" },
            {(int)ReplyStatusCodes.Internal_Serice_Error,"Service error" },
        };

        public string GetResponseMessage(int replyStatusCode)
        {
            string output;
            responseMessageList.TryGetValue(replyStatusCode,out output);
            return output;
        }
    }
}

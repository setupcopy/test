using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngineerBackendForFrontend.Utilities
{
    public enum ReturnStatus
    {
        Fail = 0,
        Success = 1
    }

    public class ResultResponse
    {
        public ResultResponse()
        {
        }

        public int? Code { get; set; }
        public string Message { get; set; } = "";
        public object Result { get; set; }
        public ReturnStatus ReturnStatus { get; set; } = ReturnStatus.Success;

    }
}

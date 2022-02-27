using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SearchEngineerBackendForFrontend.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngineerBackendForFrontend.Filters
{
    public class ApiResultFilterAttribute: ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var resultResponse = new ResultResponse();
            resultResponse.Result = "";

            if (context.Result is StatusCodeResult)
            {
                resultResponse.Code = (context.Result as StatusCodeResult).StatusCode;
            }
            else
            {
                var objectResult = context.Result as ObjectResult;

                resultResponse.Code = objectResult.StatusCode;
                if (objectResult.Value != null)
                {
                    resultResponse.Result = objectResult.Value;
                }
                //failed
                if (objectResult.StatusCode >= 300)
                {
                    resultResponse.ReturnStatus = ReturnStatus.Fail;
                }
            }

            ResponseMessage responseMessage = new ResponseMessage();
            resultResponse.Message = responseMessage.GetResponseMessage((int)resultResponse.Code);

            context.Result = new ObjectResult(resultResponse);
        }
    }
}

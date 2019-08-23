using BLRMIS.Web.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLRMIS.Web.API.Extenstions
{
    public static class ResponseExtensions
    {
        #region Public Methods

        public static IActionResult ToHttpResponse(this ErrorResponseModel response)
        {
            var status = response.ToHttpStatusCode();

            return new ObjectResult(response) { StatusCode = (int)status };
        }

        public static string ToJsonResponse(this ErrorResponseModel response)
        {
            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        #endregion Public Methods
    }
}

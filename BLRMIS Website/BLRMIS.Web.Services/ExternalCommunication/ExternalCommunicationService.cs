using BLRMIS.Web.Common;
using BLRMIS.Web.Domain.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BLRMIS.Web.Services.ExternalCommunication
{
    public class ExternalCommunicationService
    {
        protected AppSettings _appSettings;

        public ExternalCommunicationService(IOptions<AppSettings> settings)
        {
            _appSettings = settings.Value;
        }

        public async Task<SingleResponseModel<EStampingModel>> GetTotalChallanAmount(Dictionary<string, string> parameters)
        {
            var response = new SingleResponseModel<EStampingModel>();

            var queryString = "";
            foreach (var item in parameters)
            {
                if (!string.IsNullOrEmpty(item.Value))
                {
                    queryString = queryString + $"{item.Key}={item.Value}&";
                }

            }
            var apiResponse = await CallApi(parameters,$"ChallanDetails/Amount?{queryString}");
            string TotalAmount = string.Empty;
            if (apiResponse.Item1.IsSuccessStatusCode)
            {
                dynamic jsonBody = JObject.Parse(apiResponse.Item2);

                response.Model = new EStampingModel()
                {
                    TotalAmount = jsonBody.TotalAmount
            };
            }
            else
            {
                dynamic jsonBody = JObject.Parse(apiResponse.Item2);

                response.IsError = true;
                response.Error = GetError(jsonBody, apiResponse.Item1.StatusCode);
            }


            return await Task.FromResult(response);
        }


        private async Task<Tuple<HttpResponseMessage, string>> CallApi(IReadOnlyDictionary<string, string> parameters,
         string url, Dictionary<string, string> inputParameters = null)
        {
            using (var client = new HttpClient())
            {

                var uri = new Uri(_appSettings.EStampingUrl);
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Host = uri.Host;
                client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add(_appSettings.AuthenticationHeaderName, _appSettings.AuthenticationHeaderValue);


                HttpResponseMessage message;
                string body;

                switch (GetParameter(ExternalCommunicationParameters.HttpMethod, parameters))
                {
                    case ExternalCommunicationParameters.Post:
                        {
                            var content = new StringContent(JsonConvert.SerializeObject(inputParameters), Encoding.UTF8,
                                "application/json");
                           // _logger.Log(LogLevel.Debug, $"External System Request ::External URL : {uri.OriginalString}{url} Content : {JsonConvert.SerializeObject(inputParameters)}");
                            message = client.PostAsync($"{uri.OriginalString}{url}", content).Result;
                            body = await message.Content.ReadAsStringAsync();
                           // Log4NetLogger.Log(LogLevel.Debug, $"External System Response :: Content : {body}");
                            break;
                        }

                    case ExternalCommunicationParameters.Put:
                        {
                            var content = new StringContent(JsonConvert.SerializeObject(inputParameters), Encoding.UTF8,
                                "application/json");          
                            var request = new HttpRequestMessage(new HttpMethod("PUT"), $"{uri.OriginalString}{url}") { Content = content };
                            message = await client.SendAsync(request);
                            body = await message.Content.ReadAsStringAsync();
                            break;
                        }
                    default:
                        {
                           // _logger.Log(LogLevel.Debug, $"External System Request GET ::External URL : {uri.OriginalString}{url}");
                            message = await client.GetAsync($"{uri.OriginalString}{url}",
                                HttpCompletionOption.ResponseContentRead);
                            body = await message.Content.ReadAsStringAsync();
                           // _logger.Log(LogLevel.Debug, $"External System Response :: Content : {body}");
                            break;

                        }
                }

                return new Tuple<HttpResponseMessage, string>(message, body);
            }
        }

        protected static string GetParameter(string key, IReadOnlyDictionary<string, string> parameters)
        {
            parameters.TryGetValue(key, out var value);

            return value;
        }

        protected static ErrorResponseModel GetError(dynamic jsonBody, HttpStatusCode statusCode)
        {
            var response = new ErrorResponseModel
            {
                InternalError = new ErrorModel
                {
                    HttpCode = statusCode,
                    Category = jsonBody.errorCategory,
                    Message = jsonBody.message,
                    Code = ((int)statusCode).ToString()
                }

                //do error parsing here
            };
            return response;
        }
    }
}

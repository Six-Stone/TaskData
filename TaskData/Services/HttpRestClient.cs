using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TackNo.Shared;

namespace TaskData.Services
{
    public class HttpRestClient
    {
        private readonly string apiUrl;
        protected readonly RestClient client;

        public HttpRestClient(string apiUrl)
        {
            this.apiUrl = apiUrl;
            client = new RestClient();
        }
        //获取
        public async Task<ApiResponse> ExecuteAsync(BaseRequest baseRequest)
        {
            var request = new RestRequest(baseRequest.Method);
            request.AddHeader("Content-Type", baseRequest.ContentType);

            if (baseRequest.Parameter != null)
                request.AddParameter("param", JsonConvert.SerializeObject(baseRequest.Parameter), ParameterType.RequestBody);
            client.BaseUrl = new Uri(apiUrl + baseRequest.Route);
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<ApiResponse>(response.Content);

            else
                return new ApiResponse()
                {
                    Status = false,
                    Result = null,
                    Message = response.ErrorMessage
                };
        }
        //泛型获取
        public async Task<ApiResponse<T>> ExecuteAsync<T>(BaseRequest baseRequest)
        {
            var request = new RestRequest(baseRequest.Method);
            request.AddHeader("Content-Type", baseRequest.ContentType);

            if (baseRequest.Parameter != null)
            {
                request.AddParameter("param", JsonConvert.SerializeObject(baseRequest.Parameter), ParameterType.RequestBody);
                //request.AddQueryParameter("subTaskNo", baseRequest.Parameter.ToString());
            }   
            client.BaseUrl = new Uri(apiUrl + baseRequest.Route);
            var response = await client.ExecuteAsync<ApiResponse<T>>(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return response.Data;
            else
                return new ApiResponse<T>()
                {

                };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TackNo.Shared;
using TackNo.Shared.Dtos;

namespace TaskData.Services
{
    public class LoginService : ILoginService
    {
        private readonly HttpRestClient client;
        private readonly string serviceName = "Author";
        public LoginService(HttpRestClient client)
        {
            this.client = client;
        }
        public async Task<ApiResponse> Login(UserDto user)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.POST;
            request.Route = $"api/{serviceName}/UserLogin";
            request.Parameter = user;
            return await client.ExecuteAsync(request);
        }
    }
}

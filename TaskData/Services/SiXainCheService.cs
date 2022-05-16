using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TackNo.Shared;
using TackNo.Shared.Dtos;

namespace TaskData.Services
{
    public class SiXainCheService : BaseService<Queue<SiXainCheDto>>, ISiXainCheDto
    {
        private readonly HttpRestClient client;
        public SiXainCheService(HttpRestClient client) : base(client, "SubTaskTest")
        {
            this.client = client;
        }

        public async Task<ApiResponse<SiXainCheDto>> ShuttleOnOffLine(SiXainCheDto entity)
        {
            BaseRequest request = new();
            request.Method = RestSharp.Method.POST;
            request.Route = $"api/SubTaskTest/ShuttleOnOffLine?shuttleNo={entity.ShuttleNo}&activeEnum={entity.IsActive}";

            return await client.ExecuteAsync<SiXainCheDto>(request);
        }
    }
}

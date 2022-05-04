using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TackNo.Shared.Dtos;

namespace TaskData.Services
{
   public class CarService : BaseService<Queue<CarTaskViewDto>>, ICarService
    {
        private readonly HttpRestClient client;
        public CarService(HttpRestClient client) : base(client, "ShuttleCommand")
        {
            this.client = client;
        }

        public async Task<TackNo.Shared.ApiResponse<CarTaskViewDto>> ActionCaerNO(CarTaskViewDto entity)
        {
            BaseRequest request = new BaseRequest();


            request.Method = RestSharp.Method.POST;
            request.Route = $"api/SubTaskTest/SendShuttleCommand?commandNo={entity.commandNo}&carNo={entity.shuttleNo}";
            return await client.ExecuteAsync<CarTaskViewDto>(request);
        }

        public async Task<List<T>> GetSearchSubTasksCaerNo<T>(string id)
        {
            BaseRequest request = new()
            {
                Method = RestSharp.Method.GET,
                Route = $"api/ShuttleCommand/SearchSubTasks?ShuttleNo={id}"
            };
            var a = await client.ExecuteAsync<QueryDto<T>>(request);
            return a.data.list;
        }
    }
}

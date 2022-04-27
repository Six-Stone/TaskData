using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TackNo.Shared.Dtos;

namespace TaskData.Services
{
    public class ToDoService : BaseService<Queue<TaskViewDto>>, IToDoService
    {
        private readonly HttpRestClient client;

        public ToDoService(HttpRestClient client) : base(client, "Task")
        {
            this.client = client;
        }
        public async Task<List<T>> GetAllFilterAsync<T>()
        {
            BaseRequest request = new()
            {
                Method = RestSharp.Method.GET,
                Route = $"api/Task/SearchTasks"
            };
            var a = await client.ExecuteAsync<QueryDto<T>>(request);
            return a.data.list;
        }

        public async Task<List<T>> GetStuTaskAsync<T>(string id)
        {
            BaseRequest request = new()
            {
                Method = RestSharp.Method.GET,
                Route = $"api/SubTask/SearchSubTasks?taskId={id}"
            };
            var a = await client.ExecuteAsync<QueryDto<T>>(request);
            return a.data.list;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TackNo.Shared.Dtos;

namespace TaskData.Services
{
    public class GetNodeTypeService : BaseService<Queue<NodeTypeViewDto>>, IGetNodeTypeService
    {
        private readonly HttpRestClient client;
        public GetNodeTypeService(HttpRestClient client) : base(client, "Node")
        {
            this.client = client;
        }
        public async Task<List<T>> GetNodeType<T>(string NodeType)
        {
            BaseRequest request = new()
            {
                Method = RestSharp.Method.GET,
                Route = $"api/Node/SearchTransportNode?nodeType={NodeType}"
            };
            var a = await client.ExecuteAsync<QueryDto<T>>(request);
            return a.data.list;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TackNo.Shared;
using TackNo.Shared.Dtos;

namespace TaskData.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        private readonly HttpRestClient client;
        private readonly string serviceName;
        public BaseService(HttpRestClient client, string serviceName)
        {
            this.client = client;
            this.serviceName = serviceName;
        }
        //强制完成子任务
        public async Task<ApiResponse<TEntity>> ActionStuTaskNO(string entity)
        {
            BaseRequest request = new();
            request.Method = RestSharp.Method.POST;
            request.Route = $"api/{serviceName}/ForceFinishSubTask?subTaskNo={entity}";
            request.Parameter = entity;
            return await client.ExecuteAsync<TEntity>(request);
        }
        //补发四向车任务
        public async Task<ApiResponse<TEntity>> ActionCaerNO(TEntity entity)
        {
            BaseRequest request = new();
            request.Method = RestSharp.Method.POST;
            request.Route = $"api/{serviceName}/ForceFinishSubTask";
            request.Parameter = entity;
            return await client.ExecuteAsync<TEntity>(request);
        }
        //查询主任务
        async Task<ApiResponse<QueryDto<TEntity>>> IBaseService<TEntity>.GetAllAsync()
        {
            BaseRequest request = new()
            {
                Method = RestSharp.Method.GET,
                Route = $"api/{serviceName}/SearchTasks"
            };
            return await client.ExecuteAsync<QueryDto<TEntity>>(request);
        }
        //查询子任务
        async Task<ApiResponse<QueryDto<TEntity>>> IBaseService<TEntity>.GetStuTaskAsync(string parameter)
        {
            BaseRequest request = new()
            {
                Method = RestSharp.Method.GET,
                Route = $"api/SubTask/SearchSubTasks?taskId={parameter}"
            };
            return await client.ExecuteAsync<QueryDto<TEntity>>(request);
        }
        //后管四项车子任务查询（根据车号来查询）
        async Task<ApiResponse<QueryDto<TEntity>>> IBaseService<TEntity>.GetSearchSubTasksCaerNo(string ShuttleNo)
        {
            BaseRequest request = new()
            {
                Method = RestSharp.Method.GET,
                Route = $"api/{serviceName}/SearchSubTasks?ShuttleNo={ShuttleNo}"
            };
            return await client.ExecuteAsync<QueryDto<TEntity>>(request);
        }
        //后管四向车子任务查询（根据StuTaskId来查询）
        async Task<ApiResponse<QueryDto<TEntity>>> IBaseService<TEntity>.GetSearchSubTasksId(string parameter)
        {
            BaseRequest request = new()
            {
                Method = RestSharp.Method.GET,
                Route = $"api/{serviceName}/SearchSubTasks?SubTaskId={parameter}"
            };
            return await client.ExecuteAsync<QueryDto<TEntity>>(request);
        }
    }
}

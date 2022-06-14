﻿using System;
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
        //下发任务
        //Task<ApiResponse<TEntity>> SendSubTask(string entity);
        public async Task<ApiResponse<TEntity>> SendSubTask(string entity)
        {
            BaseRequest request = new();
            request.Method = RestSharp.Method.POST;
            request.Route = $"api/SubTaskTest/SendSubTask?subTaskNo={entity}";
            return await client.ExecuteAsync<TEntity>(request);
        }
        //强制完成子任务
        public async Task<ApiResponse<TEntity>> ActionStuTaskNO(string entity)
        {
            BaseRequest request = new();
            request.Method = RestSharp.Method.POST;
            request.Route = $"api/SubTaskTest/ForceFinishSubTask?subTaskNo={entity}";
            return await client.ExecuteAsync<TEntity>(request);
        }
        /// <summary>
        /// 四向车上下线
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<ApiResponse<TEntity>> ShuttleOnOffLine(TEntity entity)
        {
            BaseRequest request = new();
            request.Method = RestSharp.Method.POST;
            request.Route = $"api/SubTaskTest/ShuttleOnOffLine?shuttleNo={entity}&activeEnum={entity}";

            return await client.ExecuteAsync<TEntity>(request);
        }
        //Task<ApiResponse<TEntity>> ConLifeTCHandle(string entity);
        //强制万层四向车，提升机任务
        public async Task<ApiResponse<TEntity>> ConLifeTCHandle(string entity)
        {
            BaseRequest request = new();
            request.Method = RestSharp.Method.POST;
            request.Route = $"api/SubTaskTest/ConLifeTCHandle?commandNo={entity}";
            return await client.ExecuteAsync<TEntity>(request);
        }
        //取消四向车充电任务
        public async Task<ApiResponse<TEntity>> CancelCharge(string entity)
        {
            BaseRequest request = new();
            request.Method = RestSharp.Method.POST;
            request.Route = $"api/SubTaskTest/CancelCharge?carNo={entity}";
            return await client.ExecuteAsync<TEntity>(request);
        }
        //补发四向车任务
        public async Task<ApiResponse<TEntity>> ActionCaerNO(TEntity entity)
        {
            BaseRequest request = new();
            request.Method = RestSharp.Method.POST;
            request.Route = $"api/SubTaskTest/SendShuttleCommand?commandNo={entity}&carNo={entity}";
            
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
        //根据站点类型来查询站点
        async Task<ApiResponse<QueryDto<TEntity>>> IBaseService<TEntity>.GetNodeType(string NodeType)
        {
            BaseRequest request = new()
            {
                Method = RestSharp.Method.GET,
                Route = $"api/nodeType/SearchTransportNode?nodeType={NodeType}"
            };
            return await client.ExecuteAsync<QueryDto<TEntity>>(request);
        }
        //呼叫大件库托盘
        async Task<ApiResponse<QueryDto<TEntity>>> IBaseService<TEntity>.ConvCallTray()
        {
            BaseRequest request = new()
            {
                Method = RestSharp.Method.POST,
                Route = $"api/SubTaskTest/ConvCallTray"
            };
            return await client.ExecuteAsync<QueryDto<TEntity>>(request);
        }
    }
}

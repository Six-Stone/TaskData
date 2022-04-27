using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TackNo.Shared;
using TackNo.Shared.Dtos;

namespace TaskData.Services
{
    /// <summary>
    /// 管理接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
   public interface IBaseService<TEntity> where TEntity : class
    {

        Task<ApiResponse<TEntity>> ActionStuTaskNO(string entity);
        Task<ApiResponse<TEntity>> ActionCaerNO(TEntity entity);

        //查询全部
        Task<ApiResponse<QueryDto<TEntity>>> GetAllAsync();
        // 条件查询
        Task<ApiResponse<QueryDto<TEntity>>> GetStuTaskAsync(string parameter);
        Task<ApiResponse<QueryDto<TEntity>>> GetSearchSubTasksCaerNo(string ShuttleNo);
        Task<ApiResponse<QueryDto<TEntity>>> GetSearchSubTasksId(string SubTaskId);
    }
}

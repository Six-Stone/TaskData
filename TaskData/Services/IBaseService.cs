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
        //强制完成子任务
        Task<ApiResponse<TEntity>> ActionStuTaskNO(string entity);
        //四向车上下线
        Task<ApiResponse<TEntity>> ActionCaerNO(TEntity entity);
        //下发任务
        Task<ApiResponse<TEntity>> SendSubTask(string entity);

        //强制完成四向车，提升机任务
        Task<ApiResponse<TEntity>> ConLifeTCHandle(string entity);
        //取消四向车充电任务
        Task<ApiResponse<TEntity>> CancelCharge(string entity);
        //四向车上下线
        Task<ApiResponse<TEntity>> ShuttleOnOffLine(TEntity entity);
        //查询全部
        Task<ApiResponse<QueryDto<TEntity>>> GetAllAsync();
        // 条件查询
        Task<ApiResponse<QueryDto<TEntity>>> GetStuTaskAsync(string parameter);
        //后管四项车子任务查询（根据车号来查询）
        Task<ApiResponse<QueryDto<TEntity>>> GetSearchSubTasksCaerNo(string ShuttleNo);
        //后管四向车子任务查询（根据StuTaskId来查询）
        Task<ApiResponse<QueryDto<TEntity>>> GetSearchSubTasksId(string SubTaskId);
        //根据站点类型来查询站点
        Task<ApiResponse<QueryDto<TEntity>>> GetNodeType(string NodeType);
        //呼叫大件库托盘
        Task<ApiResponse<QueryDto<TEntity>>> ConvCallTray();
    }
}

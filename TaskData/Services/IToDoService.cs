using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TackNo.Shared.Dtos;

namespace TaskData.Services
{
   public interface IToDoService :IBaseService<Queue<TaskViewDto>>
    {
        Task<List<T>> GetAllFilterAsync<T>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TackNo.Shared.Dtos;

namespace TaskData.Services
{
    public interface ICarService : IBaseService<Queue<CarTaskViewDto>>
    {
        Task<List<T>> GetSearchSubTasksCaerNo<T>(string id);
    }
}

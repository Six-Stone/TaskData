using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TackNo.Shared;
using TackNo.Shared.Dtos;

namespace TaskData.Services
{
    public interface ISiXainCheDto : IBaseService<Queue<SiXainCheDto>>
    {
        Task<ApiResponse<SiXainCheDto>> ShuttleOnOffLine(SiXainCheDto entity);
    }
}

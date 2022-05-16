using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TackNo.Shared.Dtos;

namespace TaskData.Services
{
    public interface IGetNodeTypeService : IBaseService<Queue<NodeTypeViewDto>>
    {
        Task<List<T>> GetNodeType<T>(string NodeType);
    }
}

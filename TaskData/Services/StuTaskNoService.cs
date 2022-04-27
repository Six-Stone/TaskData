using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TackNo.Shared.Dtos;

namespace TaskData.Services
{
    public class StuTaskNoService : BaseService<Queue<SubTaskViewDto>>, IStuTaskNoService
    {
        public StuTaskNoService(HttpRestClient client) : base(client, "SubTask")
        {

        }
    }
}

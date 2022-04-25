using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TackNo.Shared.Dtos
{
    /// <summary>
    /// 子任务显示模型
    /// </summary>
    public class SubTaskViewDto : BaseDto
    {
        public string subTaskNo { get; set; }
        public string equipmentNo { get; set; }
        public string source { get; set; }
        public string destination { get; set; }
        public string carNo { get; set; }
        public int status { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string id { get; set; }
        public string operateUser { get; set; }


    }
}

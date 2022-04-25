using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TackNo.Shared.Dtos
{
    /// <summary>
    /// 主任务查询显示模型
    /// </summary>
    public class TaskViewDto
    {
        /// <summary>
        /// 任务号
        /// </summary>
        public string taskNo { get; set; }
        public string containerId { get; set; }
        public string sourceArea { get; set; }
        public string source { get; set; }
        public string areaNo { get; set; }
        public string destination { get; set; }
        public int taskType { get; set; }
        public string uniqueNo { get; set; }
        public string status { get; set; }
        public DateTime createDate { get; set; }
        public DateTime updateDate { get; set; }
        public string id { get; set; }
        public string operateUser { get; set; }

    }
}

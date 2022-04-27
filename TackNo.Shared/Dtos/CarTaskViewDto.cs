using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TackNo.Shared.Dtos
{
    public class CarTaskViewDto
    {
        //"commandNo": "SC813401",
        //"sourceNode": "N331813",
        //"taskStep": 1,
        //"destNode": "N332013",
        //"routes": null,
        //"subTaskId": "4e6d4f8d-7f2b-42f1-a0e4-1d1f550ed002",
        //"shuttleId": null,
        //"shuttleNo": "04",
        //"status": "进行中",
        //"sendDate": "2022-04-19T13:28:07",
        //"responseDate": "2022-04-19T13:28:07",
        //"endDate": null,
        //"shuttleCommandType": "y方向移动",
        //"wareHouse": "中小件",
        //"id": "ffae7cab-4978-4b41-9a75-3a0b7035f552",
        //"operateUser": null
        public string commandNo { get; set; }
        public string sourceNode { get; set; }
        public string taskStep { get; set; }
        public string destNode { get; set; }
        public string routes { get; set; }
        public string subTaskId { get; set; }
        public string shuttleId { get; set; }
        public string shuttleNo { get; set; }
        public string status { get; set; }
        public DateTime sendDate { get; set; }
        public DateTime endDate { get; set; }
        public string shuttleCommandType { get; set; }
        public string wareHouse { get; set; }
        public string id { get; set; }
        public string operateUser { get; set; }

    }
}

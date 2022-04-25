using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TackNo.Shared.Dtos
{
    public class QueryDto<T>
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int totalSize { get; set; }

        public List<T> list { get; set; }
    }
}

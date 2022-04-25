using System;

namespace TackNo.Shared
{
    public class ApiResponse
    {
        public string Message { get; set; }

        public bool Status { get; set; }

        public object Result { get; set; }
    }
    public class ApiResponse<T>
    { /// <summary>
      /// 成功:200
      /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 请求返回描述信息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 请求返回的数据模型
        /// </summary>
        public T data { get; set; }
        //[JsonIgnore]
        public bool IsSuccess
        {
            get => code.Equals("200");
        }
    }
}

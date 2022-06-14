using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.NLog.NLog
{
    /// <summary>
    /// 报错日志
    /// </summary>
    public class NLogError : NLogBase
    {
        private static NLogError _NLogError;

        public static NLogError GetInstance()
        {
            return _NLogError ?? (_NLogError = new NLogError());
        }

        private NLogError()
        {
            Logger = LogManager.GetLogger("Error");
        }
    }
}

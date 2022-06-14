using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.NLog.NLog
{
    /// <summary>
    /// 后管操作日志
    /// </summary>
    public class NLogAdminMessage : NLogBase
    {
        private static NLogAdminMessage _NLogMessage;

        public static NLogAdminMessage GetInstance()
        {
            return _NLogMessage ?? (_NLogMessage = new NLogAdminMessage());
        }

        private NLogAdminMessage()
        {
            Logger = LogManager.GetLogger("AdminMessage");
        }
    }
}

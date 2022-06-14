using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.NLog.NLog
{
    public class NLogFourWayShuttleMessage : NLogBase
    {
        /// <summary>
        /// 四向车相关日志
        /// </summary>
        private static NLogFourWayShuttleMessage _NLogMessage;

        public static NLogFourWayShuttleMessage GetInstance()
        {
            return _NLogMessage ?? (_NLogMessage = new NLogFourWayShuttleMessage());
        }

        private NLogFourWayShuttleMessage()
        {
            Logger = LogManager.GetLogger("FourWayShuttleMessage");
        }
    }
}

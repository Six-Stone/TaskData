using Newtonsoft.Json;
using NLog;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.NLog.NLog
{
    public class NLogBase
    {
        public ILogger Logger = (ILogger)NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
        public void Info(string str)
        {
            Logger.Info(str);
        }
        public void Info(object obj)
        {
            Info(JsonConvert.SerializeObject(obj));
        }
    }
}

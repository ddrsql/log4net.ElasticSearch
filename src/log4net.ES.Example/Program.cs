using log4net;
using log4net.ElasticSearch;
using MessageTemplates;
using MessageTemplates.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log4net.ES.Example
{
    class Program
    {
        // Create a new logging instance
        private static readonly ILog _log = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            // Most basic logging example.
            string test = "测试";
            var id = Guid.NewGuid();
            int num = (new Random()).Next(1, 100);
            //using (log4net.ThreadContext.Stacks["123"].Push("456"))
            //{
            //    log4net.ThreadContext.Properties["aa"] = "bb";
            //}
            ////var temp = string.Format("{0}{test}", "aaa", test);
            _log.DebugFormat("Debug 格式化输出 {0}{1}", "aaa", test);
            _log.InfoFormat("Info 格式化输出 {0}{1}", "aaa", test);
            _log.WarnFormat("Warn 格式化输出 {0}{1}", "aaa", test);
            _log.FatalFormat("Fatal 格式化输出 {0}{1}", "aaa", test);
            _log.ErrorFormat("Error格式化输出 {0}{1}", "Divide by zero error.", test);
            _log.DebugFormat("Debug 格式化输出 {0}{test}{id} {num}", "Divide by zero error.", test, id, num);

            // Log a message with an exeption object
            _log.Error("异常", new Exception("Something terrible happened."));


            // Log an exception object with custom fields in the Data property
            Exception newException = new Exception("There was a system error");
            newException.Data.Add("Name", "Value");
            newException.Data.Add("SystemUserID", "123");
            _log.ErrorFormat("异常带Data", newException);

            #region MessageTemplate
            var logStr = MessageTemplate.Format("Error格式化输出 {0}{test}{id} {num}", "Divide by zero error.", test, id, num);
            var templatePropertys = MessageTemplate.Capture("Error格式化输出 {0}{test}{id} {num}", "Divide by zero error.", test, id, num);

            IMyLog myLog = MyLogManager.GetLogger(typeof(Program));
            myLog.DebugFormat("Debug 格式化输出 {0}{test}{id} {num}", "Divide by zero error.", test, id, num);
            myLog.DebugFormat(new Exception("测试"), "Debug 格式化输出 {0}{test}{id} {num}", "Divide by zero error.", test, id, num);
            #endregion
        }
    }
}

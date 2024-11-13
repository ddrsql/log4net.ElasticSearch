using log4net.Core;
using log4net.Repository;
using log4net.Repository.Hierarchy;
using log4net.Util;
using MessageTemplates;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace log4net.ElasticSearch
{
    public class MyLogImpl : LogImpl, IMyLog
    {
        ///<summary>
        /// The fully qualified name of this declaring type not the type of any subclass.
        ///</summary>
        private readonly static Type ThisDeclaringType = typeof(MyLogImpl);

        public MyLogImpl(ILogger logger) : base(logger)
        {
        }

        public override void DebugFormat(string format, params object[] args)
        {
            if (IsDebugEnabled)
            {
                //Logger.Log(ThisDeclaringType, m_levelDebug, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), null);
                LoggingEvent loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository,
                    Logger.Name, Level.Info, MessageTemplate.Format(CultureInfo.InvariantCulture, format, args), null);
                var properties = MessageTemplate.Capture(format, args);
                loggingEvent.Properties["messageTemplate"] = format;
                if (properties.Count > 0)
                {
                    foreach (var item in properties)
                    {
                        loggingEvent.Properties[item.Name] = item.Value;
                    }
                }
                Logger.Log(loggingEvent);
            }
        }
        public void DebugFormat(Exception exception, string format, params object[] args)
        {
            if (IsDebugEnabled)
            {
                //Logger.Log(ThisDeclaringType, m_levelDebug, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), null);
                LoggingEvent loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository,
                    Logger.Name, Level.Info, MessageTemplate.Format(CultureInfo.InvariantCulture, format, args), exception);
                var properties = MessageTemplate.Capture(format, args);
                loggingEvent.Properties["messageTemplate"] = format;
                if (properties.Count > 0)
                {
                    foreach (var item in properties)
                    {
                        loggingEvent.Properties[item.Name] = item.Value;
                    }
                }
                Logger.Log(loggingEvent);
            }
        }
    }
}

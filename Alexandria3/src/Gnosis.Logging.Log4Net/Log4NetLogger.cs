﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using log4net;

namespace Gnosis.Logging.Log4Net
{
    public class Log4NetLogger
        : ILogger
    {
        private Log4NetLogger(ILog log)
        {
            this.log = log;
        }

        private readonly ILog log;

        public void Debug(string message)
        {
            log.Debug(message);
        }

        public void Info(string message)
        {
            log.Info(message);
        }

        public void Error(string message)
        {
            log.Error(message);
        }

        public void Error(string message, Exception ex)
        {
            log.Error(message, ex);
        }

        public void Warn(string message)
        {
            log.Warn(message);
        }

        public static ILogger GetDefaultLogger(Type type)
        {
            log4net.Config.XmlConfigurator.Configure();
            return new Log4NetLogger(LogManager.GetLogger(type));
        }
    }
}

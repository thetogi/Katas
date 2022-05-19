﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public interface ILogger
    {
        void Log(string message);
        void LogError(string message, Exception exception);
        void LogException(Exception exception);
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace KDSCommon.Interfaces.Logs
{
    public interface ILogger
    {
        void LogMessage(string message, EventLogEntryType entryType);

        void LogError(Exception exc);
        
        void LogError(string message);
       
        void LogMessage(string message, EventLogEntryType entryType, bool DisplayFunctionName);

        void LogMessage(string message, EventLogEntryType entryType, int iEventId);
       
    }

}

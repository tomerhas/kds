using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using KDSCommon.Interfaces.Logs;

namespace KDSBLLogic.Logs
{
    public class Logger : ILogger
    {

        private const string KdsEventLogSource = "Kds";
        public  void LogError(Exception exc)
        {
            LogError(exc.ToString());
        }
        public  void LogError(string message)
        {
            LogMessage(message, EventLogEntryType.Error);
        }
        public  void LogMessage(string message, EventLogEntryType entryType, bool DisplayFunctionName)
        {
            string StrError = message;
            if (DisplayFunctionName)
            {
                StackTrace st = new StackTrace();
                StackFrame sf = st.GetFrame(1);
                StrError = sf.GetMethod().Name + " :: " + StrError;
            }
            LogMessage(StrError, entryType);
        }
        public  void LogMessage(string message, EventLogEntryType entryType)
        {
            try
            {
                if (!EventLog.SourceExists(KdsEventLogSource))
                {
                    EventLog.CreateEventSource(KdsEventLogSource, KdsEventLogSource);
                }
                // Create an EventLog instance and assign its source.
                EventLog kdsLog = new EventLog();
                kdsLog.Source = KdsEventLogSource;

                // Write an informational entry to the event log.    
                kdsLog.WriteEntry(message, entryType);
            }
            catch (Exception) { }
        }
        public  void LogMessage(string message, EventLogEntryType entryType, int iEventId)
        {
            try
            {
                if (!EventLog.SourceExists(KdsEventLogSource))
                {
                    EventLog.CreateEventSource(KdsEventLogSource, KdsEventLogSource);
                }
                // Create an EventLog instance and assign its source.
                EventLog kdsLog = new EventLog();
                kdsLog.Source = KdsEventLogSource;

                // Write an informational entry to the event log.    
                kdsLog.WriteEntry(message, entryType, iEventId);
            }
            catch (Exception) { }
        }
    }
}
